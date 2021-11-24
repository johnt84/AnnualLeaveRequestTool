
-- =============================================

-- Author:		John Tomlinson
-- Create date: November 2019
-- Description: Updates an annual leave request
-- and updates the number of remaining days left
-- for the year passed in
-- =============================================

CREATE PROCEDURE [dbo].[procDeleteAnnualLeaveRequest]
    @AnnualLeaveRequestID int
AS
BEGIN

	declare @Msg varchar(1024) = ''

	if not exists(
		select *
		from AnnualLeaveRequest t
		where t.AnnualLeaveRequestID = @AnnualLeaveRequestID
	)
	begin
		set @Msg += 'No annual leave request exists with annual leave request id ' + convert(varchar(10), @AnnualLeaveRequestID) 

		RAISERROR(@Msg, 16, 1)

		return -1
	end

	declare @annualLeaveRequestIDsToDelete table (annualLeaveRequestID int primary key, processed bit)

	insert into @annualLeaveRequestIDsToDelete (annualLeaveRequestID, processed)
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
		insert into @annualLeaveRequestIDsToDelete (annualLeaveRequestID, processed)
		values(@associatedAnnualLeaveRequestID, 0)
	end

	declare @rowsToProcess int = 0
	declare @noOfRowsProcessed int = 0
	declare @currentAnnualLeaveRequestID int = 0

	select 
		@rowsToProcess = count(*) 
	from 
		@annualLeaveRequestIDsToDelete

	while @noOfRowsProcessed < @rowsToProcess
	begin
		select 
			@currentAnnualLeaveRequestID = min(t.annualLeaveRequestID)
		from 
			@annualLeaveRequestIDsToDelete t
		where 
			t.processed = 0

		declare @Year int = 0
		declare @NumberOfDaysRequested decimal(18,2) = 0
		declare @NumberOfAnnualLeaveDaysRequested decimal(18,2) = 0
		declare @NumberOfPublicLeaveDaysRequested decimal(18,2) = 0

		select 
			@Year = t.Year
			,@NumberOfDaysRequested = t.NumberOfDays
			,@NumberOfAnnualLeaveDaysRequested = t.NumberOfAnnualLeaveDays
			,@NumberOfPublicLeaveDaysRequested = t.NumberOfPublicLeaveDays
		from 
			AnnualLeaveRequest t
		where 
			t.AnnualLeaveRequestID = @currentAnnualLeaveRequestID

		update
			t
		set
			t.NumberOfDaysLeft = t.NumberOfDaysLeft + @NumberOfDaysRequested
			,t.NumberOfAnnualLeaveDaysLeft = t.NumberOfAnnualLeaveDaysLeft + @NumberOfAnnualLeaveDaysRequested
			,t.NumberOfPublicLeaveDaysLeft = t.NumberOfPublicLeaveDaysLeft + @NumberOfPublicLeaveDaysRequested
		from
			AnnualLeaveYear t
		where
			t.Year = @Year

		delete
			t
		from 
			AnnualLeaveRequest t
		where 
			t.AnnualLeaveRequestID = @currentAnnualLeaveRequestID

		update 
			t
		set 
			t.processed = 1
		from 
			@annualLeaveRequestIDsToDelete t
		where 
			t.annualLeaveRequestID = @currentAnnualLeaveRequestID

		set @noOfRowsProcessed = @noOfRowsProcessed + 1
	end

END
