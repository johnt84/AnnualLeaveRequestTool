
-- =============================================

-- Author:			John Tomlinson
-- Create date:		November 2019
-- Description: Creates an annual leave request
-- and updates the number of remaining days left
-- for the year passed in
-- =============================================

CREATE PROCEDURE [dbo].[procCreateAnnualLeaveRequest]
    @PaidLeaveType nvarchar(15)
    ,@LeaveType nvarchar(20)
    ,@StartDate datetime
    ,@ReturnDate datetime
    ,@DateCreated datetime
    ,@Notes nvarchar(150)
	,@firstNewALRequestID int = 0 OUTPUT
AS
BEGIN

	begin transaction

	declare @Msg varchar(1024) = ''

	if @ReturnDate < @StartDate or
		YEAR(@StartDate) < Year(getutcdate()) or
		YEAR(@ReturnDate) < Year(getutcdate()) or
		@PaidLeaveType <> 'Paid' or
		@LeaveType <> 'Annual Leave'
	begin
		if @ReturnDate < @StartDate 
		begin
			set @Msg += '\nReturn date ' + convert(varchar, @StartDate, 106) + ' is earlier than the start date ' + convert(varchar, @ReturnDate, 106)
		end
		
		if YEAR(@StartDate) < Year(getutcdate())
		begin
			set @Msg += '\nStart date ' + convert(varchar, @StartDate, 106) + ' is in the past'
		end

		if YEAR(@ReturnDate) < Year(getutcdate())
		begin
			set @Msg += '\nReturn date ' + convert(varchar, @ReturnDate, 106) + ' is in the past'
		end

		if @PaidLeaveType <> 'Paid' 
		begin
			set @Msg += '\n' + @PaidLeaveType + ' paid leave type is not yet supported'
		end

		if @LeaveType <> 'Annual Leave'
		begin
			set @Msg += '\n' + @LeaveType + ' leave type is not yet supported'
		end

		RAISERROR(@Msg, 16, 1)

		rollback

		return -1
	end

	declare @annualLeaveRequest table
	(
		year int Primary Key,
		numberOfDays decimal(18, 2),
		numberOfAnnualLeaveDays decimal(18, 2),
		numberOfPublicLeaveDays decimal(18, 2),
		numberOfDaysLeft decimal(18, 2),
		numberOfAnnualLeaveDaysLeft decimal(18, 2),
		numberOfPublicLeaveDaysLeft decimal(18, 2),
		numberOfDaysLeftAfterRequest decimal(18, 2),
		numberOfAnnualLeaveDaysLeftAfterRequest decimal(18, 2),
		numberOfPublicLeaveDaysLeftAfterRequest decimal(18, 2),
		alreadyExists bit,
		annualLeaveRequestID int,
		processed bit
	)

	insert into @annualLeaveRequest
	(
		year,
		numberOfDays,
		numberOfAnnualLeaveDays,
		numberOfPublicLeaveDays,
		numberOfDaysLeft,
		numberOfAnnualLeaveDaysLeft,
		numberOfPublicLeaveDaysLeft,
		numberOfDaysLeftAfterRequest,
		numberOfAnnualLeaveDaysLeftAfterRequest,
		numberOfPublicLeaveDaysLeftAfterRequest,
		alreadyExists,
		annualLeaveRequestID,
		processed
	)
	select 
		t.*
		,0 as processed
	from 
		NumberOfAnnualLeaveDaysBetweenTwoDatesGet(@StartDate, @ReturnDate) t

	if not exists(
		select *
		from @annualLeaveRequest
	)
	begin
		set @Msg += 'No valid annual leave days to be created between ' + convert(varchar, @StartDate, 106) + ' and ' + convert(varchar, @ReturnDate, 106)

		RAISERROR(@Msg, 16, 1)

		rollback

		return -1
	end

	declare @newAnnualLeaveRequestIDs table (newAnnualLeaveRequestID int Primary Key)

	declare @rowsToProcess int = 0
	declare @noOfRowsProcessed int = 0
	declare @currentYear int = 0

	select 
		@rowsToProcess = count(*) 
	from 
		@annualLeaveRequest

	while @noOfRowsProcessed < @rowsToProcess
	begin
		select 
			@currentYear = min(t.year)
		from 
			@annualLeaveRequest t
		where 
			t.processed = 0

		declare @numberOfDaysRequested decimal(18, 2) = 0
		declare @numberOfDaysLeft decimal(18, 2) = 0
		declare @numberOfDaysLeftAfterRequest decimal(18, 2) = 0
		declare @numberOfAnnualLeaveDaysRequested decimal(18, 2) = 0
		declare @numberOfAnnualLeaveDaysLeft decimal(18, 2) = 0
		declare @numberOfAnnualLeaveDaysLeftAfterRequest decimal(18, 2) = 0
		declare @numberOfPublicLeaveDaysRequested decimal(18, 2) = 0
		declare @numberOfPublicLeaveDaysLeft decimal(18, 2) = 0
		declare @numberOfPublicLeaveDaysLeftAfterRequest decimal(18, 2) = 0
		declare @alreadyExists bit = 0

		select 
			@numberOfDaysRequested = t.numberOfDays
			,@numberOfDaysLeft = t.numberOfDaysLeft
			,@numberOfDaysLeftAfterRequest = t.numberOfDaysLeftAfterRequest
			,@numberOfAnnualLeaveDaysRequested = t.numberOfAnnualLeaveDays
			,@numberOfAnnualLeaveDaysLeft = t.numberOfAnnualLeaveDaysLeft
			,@numberOfAnnualLeaveDaysLeftAfterRequest = t.numberOfAnnualLeaveDaysLeftAfterRequest
			,@numberOfPublicLeaveDaysRequested = t.numberOfPublicLeaveDays
			,@numberOfPublicLeaveDaysLeft = t.numberOfPublicLeaveDaysLeft
			,@numberOfPublicLeaveDaysLeftAfterRequest = t.numberOfPublicLeaveDaysLeftAfterRequest
			,@alreadyExists = t.alreadyExists
		from 
			@annualLeaveRequest t
		where 
			t.year = @currentYear

		if @numberOfDaysLeftAfterRequest < 0 or
			@numberOfAnnualLeaveDaysLeftAfterRequest < 0 or
			@numberOfPublicLeaveDaysLeftAfterRequest < 0 or
			@alreadyExists = 1
		begin
			if @numberOfDaysLeftAfterRequest < 0
			begin
				set @Msg += 'Number of days requested of ' + convert(nvarchar(15), @NumberOfDaysRequested) + ' is more than the number of days left of ' + convert(nvarchar(15), @NumberOfDaysLeft)
			end

			if @numberOfAnnualLeaveDaysLeftAfterRequest < 0
			begin
				set @Msg += '\nNumber of annual leave days requested of ' + convert(nvarchar(15), @NumberOfAnnualLeaveDaysRequested) + ' is more than the number of annual leave days left of ' + convert(nvarchar(15), @NumberOfAnnualLeaveDaysLeft)
			end

			if @numberOfPublicLeaveDaysLeftAfterRequest < 0
			begin
				set @Msg += '\nNumber of public days requested of ' + convert(nvarchar(15), @NumberOfPublicLeaveDaysRequested) + ' is more than the number of public days left of ' + convert(nvarchar(15), @NumberOfPublicLeaveDaysLeft)
			end

			if @alreadyExists = 1 
			begin
				set @Msg += '\nThere is already an existing annual leave request between ' + convert(varchar, @StartDate, 106) + ' and ' + convert(varchar, @ReturnDate, 106)
			end

			rollback

			break
		end

		declare @newALRequestID int = 0

		insert into AnnualLeaveRequest
		(
			Year,
			PaidLeaveType,
			LeaveType,
			StartDate,
			ReturnDate,
			NumberOfDays,
			NumberOfAnnualLeaveDays,
			NumberOfPublicLeaveDays,
			DateCreated,
			Notes
		)
		values
		(
			@currentYear
			,@PaidLeaveType
			,@LeaveType
			,@StartDate
			,@ReturnDate
			,@numberOfDaysRequested
			,@numberOfAnnualLeaveDaysRequested
			,@numberOfPublicLeaveDaysRequested
			,@dateCreated
			,@notes
		)

		SELECT @newALRequestID = SCOPE_IDENTITY()

		update
			t
		set
			t.NumberOfDaysLeft = @numberOfDaysLeftAfterRequest
			,t.NumberOfAnnualLeaveDaysLeft = @NumberOfAnnualLeaveDaysLeftAfterRequest
			,t.NumberOfPublicLeaveDaysLeft = @NumberOfPublicLeaveDaysLeftAfterRequest
		from
			AnnualLeaveYear t
		where
			t.Year = @currentYear

		insert into @newAnnualLeaveRequestIDs (newAnnualLeaveRequestID)
		select @NewALRequestID

		update 
			t
		set 
			t.processed = 1
		from 
			@annualLeaveRequest t
		where 
			t.year = @currentYear

		set @noOfRowsProcessed = @noOfRowsProcessed + 1
	end

	if exists(
		select *
		from @annualLeaveRequest t
		where t.processed = 0
	)
	begin
		RAISERROR(@Msg, 16, 1)

		return -1
	end

	declare @numberOfNewAnnualLeaveRequestIDs int = 0

	select @numberOfNewAnnualLeaveRequestIDs = count(*)
	from @newAnnualLeaveRequestIDs

	if @numberOfNewAnnualLeaveRequestIDs > 1
	begin
		update t
		set t.AssociatedAnnualLeaveRequestID = (select * from @newAnnualLeaveRequestIDs x where x.newAnnualLeaveRequestID <> t.AnnualLeaveRequestID)
		from AnnualLeaveRequest t
		inner join @newAnnualLeaveRequestIDs x on t.AnnualLeaveRequestID = x.newAnnualLeaveRequestID
	end
    
    select @firstNewALRequestID = min(t.newAnnualLeaveRequestID)
	from @newAnnualLeaveRequestIDs t

	select @firstNewALRequestID

	commit

END
