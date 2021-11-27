-- =============================================

-- Author:		John Tomlinson
-- Create date: November 2019
-- Description: Updates an annual leave request
-- and updates the number of remaining days left
-- for the year passed in
-- =============================================

CREATE PROCEDURE [dbo].[procUpdateAnnualLeaveRequest]
    @AnnualLeaveRequestID int
	,@PaidLeaveType nvarchar(15)
    ,@LeaveType nvarchar(20)
    ,@StartDate datetime
    ,@ReturnDate datetime
    ,@Notes nvarchar(150)
AS
BEGIN

	begin transaction

	declare @Msg varchar(1024) = ''

	if not exists(
		select *
		from annualLeaveRequest t
		where t.AnnualLeaveRequestID = @AnnualLeaveRequestID
	)
	begin
		set @Msg += 'No annual leave request exists with annual leave request id ' + convert(varchar(10), @AnnualLeaveRequestID) 

		RAISERROR(@Msg, 16, 1)

		rollback

		return -1
	end

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
			set @Msg += '\nThe ' + @PaidLeaveType + ' paid leave type is not yet supported'
		end

		if @LeaveType <> 'Annual Leave'
		begin
			set @Msg += '\nThe ' + @LeaveType + ' leave type is not yet supported'
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
		annualLeaveRequestID int
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
		annualLeaveRequestID
	)
	select 
		t.*
	from 
		NumberOfAnnualLeaveDaysBetweenTwoDatesGet(@StartDate, @ReturnDate) t

	if not exists(
		select *
		from @annualLeaveRequest
	)
	begin
		set @Msg += 'No valid annual leave days to be updated between ' + convert(varchar, @StartDate, 106) + ' and ' + convert(varchar, @ReturnDate, 106)

		RAISERROR(@Msg, 16, 1)

		rollback

		return -1
	end

	declare @annualLeaveRequestIDsToUpdate table (annualLeaveRequestID int primary key, processed bit)

	insert into @annualLeaveRequestIDsToUpdate (annualLeaveRequestID, processed)
	values(@AnnualLeaveRequestID, 0)

	declare @associatedAnnualLeaveRequestID int = 0

	select 
		@associatedAnnualLeaveRequestID = t.AssociatedAnnualLeaveRequestID
	from 
		AnnualLeaveRequest t
	where 
		t.AnnualLeaveRequestID = @AnnualLeaveRequestID

	if @associatedAnnualLeaveRequestID > 0
	begin
		insert into @annualLeaveRequestIDsToUpdate (annualLeaveRequestID, processed)
		values(@associatedAnnualLeaveRequestID, 0)
	end

	declare @rowsToProcess int = 0
	declare @noOfRowsProcessed int = 0
	declare @currentAnnualLeaveRequestID int = 0
	declare @currentAssociatedAnnualLeaveRequestID int = 0

	select 
		@rowsToProcess = count(*) 
	from 
		@annualLeaveRequestIDsToUpdate

	while @noOfRowsProcessed < @rowsToProcess
	begin
		select 
			@currentAnnualLeaveRequestID = min(t.annualLeaveRequestID)
		from 
			@annualLeaveRequestIDsToUpdate t
		where 
			t.processed = 0

		declare @year int = 0
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
		declare @NumberOfDaysLeftAfterChange decimal(18, 2) = 0
		declare @NumberOfAnnualLeaveDaysLeftAfterChange decimal(18, 2) = 0
		declare @NumberOfPublicLeaveDaysLeftAfterChange decimal(18, 2) = 0

		select 
			@year = t.year
			,@NumberOfDaysRequested = t.numberOfDays
			,@numberOfDaysLeft = t.numberOfDaysLeft
			,@numberOfDaysLeftAfterRequest = t.numberOfDaysLeftAfterRequest
			,@numberOfAnnualLeaveDaysRequested = t.numberOfAnnualLeaveDays
			,@numberOfAnnualLeaveDaysLeft = t.numberOfAnnualLeaveDaysLeft
			,@numberOfAnnualLeaveDaysLeftAfterRequest = t.numberOfAnnualLeaveDaysLeftAfterRequest
			,@numberOfPublicLeaveDaysRequested = t.numberOfPublicLeaveDays
			,@numberOfPublicLeaveDaysLeft = t.numberOfPublicLeaveDaysLeft
			,@numberOfPublicLeaveDaysLeftAfterRequest = t.numberOfPublicLeaveDaysLeftAfterRequest
			,@alreadyExists = t.alreadyExists
			,@NumberOfDaysLeftAfterChange = t.NumberOfDaysLeft - (@NumberOfDaysRequested - annualLeaveRequest.NumberOfDays)
			,@NumberOfAnnualLeaveDaysLeftAfterChange = t.NumberOfAnnualLeaveDaysLeft - (@NumberOfAnnualLeaveDaysRequested - annualLeaveRequest.NumberOfAnnualLeaveDays)
			,@NumberOfPublicLeaveDaysLeftAfterChange = t.NumberOfPublicLeaveDaysLeft - (@NumberOfPublicLeaveDaysRequested - annualLeaveRequest.NumberOfPublicLeaveDays)
			,@currentAssociatedAnnualLeaveRequestID = annualLeaveRequest.AssociatedAnnualLeaveRequestID
		from 
			@annualLeaveRequest t
		inner join annualLeaveRequest on t.annualLeaveRequestID = annualLeaveRequest.AnnualLeaveRequestID
		where 
			t.annualLeaveRequestID = @currentAnnualLeaveRequestID

		if @NumberOfDaysLeftAfterChange < 0 or
			@NumberOfAnnualLeaveDaysLeftAfterChange < 0 or
			@NumberOfPublicLeaveDaysLeftAfterChange < 0
		begin
			if @NumberOfDaysLeftAfterChange < 0
			begin
				set @Msg += 'Number of days requested of ' + convert(nvarchar(15), @NumberOfDaysRequested) + ' is more than the number of days left of ' + convert(nvarchar(15), @NumberOfDaysLeft)
			end

			if @NumberOfAnnualLeaveDaysLeftAfterChange < 0 
			begin
				set @Msg += '\nNumber of annual leave days requested of ' + convert(nvarchar(15), @NumberOfAnnualLeaveDaysRequested) + ' is more than the number of annual leave days left of ' + convert(nvarchar(15), @NumberOfAnnualLeaveDaysLeft)
			end

			if @NumberOfPublicLeaveDaysLeftAfterChange < 0
			begin
				select 'here', @NumberOfPublicLeaveDaysRequested, @NumberOfPublicLeaveDaysLeft
				set @Msg += '\nNumber of public days requested of ' + convert(nvarchar(15), @NumberOfPublicLeaveDaysRequested) + ' is more than the number of public days left of ' + convert(nvarchar(15), @NumberOfPublicLeaveDaysLeft)
			end

			rollback

			break
		end

		declare @ExistingAnnualLeaveRequests table
		(
			AnnualLeaveRequestID int Primary Key
			,StartDate datetime
			,ReturnDate datetime
		)

		insert into @ExistingAnnualLeaveRequests
		(
			AnnualLeaveRequestID
			,StartDate
			,ReturnDate
		)
		select 
			t.AnnualLeaveRequestID
			,t.StartDate
			,t.ReturnDate
		from 
			annualLeaveRequest t
		where
			t.AnnualLeaveRequestID <> @currentAnnualLeaveRequestID
			and t.AnnualLeaveRequestID <> isnull(@currentAssociatedAnnualLeaveRequestID, 0)
			and (@StartDate between t.StartDate and t.ReturnDate
			or @ReturnDate between t.StartDate and t.ReturnDate)

		if exists 
		(
			select 
				*
			from 
				@ExistingAnnualLeaveRequests
		)
		begin
			select 
				@msg = 'There is already an existing annual leave request between ' + convert(varchar, t.StartDate, 106) + ' and ' + convert(varchar, t.ReturnDate, 106)
			from 
				@ExistingAnnualLeaveRequests t

			rollback

			break
		end

		update 
			t
		set 
			t.PaidLeaveType = @PaidLeaveType
			,t.LeaveType = @LeaveType
			,t.StartDate = @StartDate
			,t.ReturnDate = @ReturnDate
			,t.NumberOfDays = @NumberOfDaysRequested
			,t.NumberOfAnnualLeaveDays = @NumberOfAnnualLeaveDaysRequested
			,t.NumberOfPublicLeaveDays = @NumberOfPublicLeaveDaysRequested
			,t.Notes = @Notes
		from 
			AnnualLeaveRequest t
		where 
			t.AnnualLeaveRequestID = @currentAnnualLeaveRequestID

		update
			t
		set
			t.NumberOfDaysLeft = @NumberOfDaysLeftAfterChange
			,t.NumberOfAnnualLeaveDaysLeft = @NumberOfAnnualLeaveDaysLeftAfterChange
			,t.NumberOfPublicLeaveDaysLeft = @NumberOfPublicLeaveDaysLeftAfterChange
		from
			AnnualLeaveYear t
		where
			t.Year = @Year
		
		update 
			t
		set 
			t.processed = 1
		from 
			@annualLeaveRequestIDsToUpdate t
		where 
			t.annualLeaveRequestID = @currentAnnualLeaveRequestID

		set @noOfRowsProcessed = @noOfRowsProcessed + 1
	end

	if exists(
		select *
		from @annualLeaveRequestIDsToUpdate t
		where t.processed = 0
	)
	begin
		RAISERROR(@Msg, 16, 1)

		return -1
	end

	commit

END
