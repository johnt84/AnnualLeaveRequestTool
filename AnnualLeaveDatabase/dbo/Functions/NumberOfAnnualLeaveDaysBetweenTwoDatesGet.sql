
CREATE FUNCTION [dbo].[NumberOfAnnualLeaveDaysBetweenTwoDatesGet] 
(	
	@StartDate date --= '2020-01-01'
	,@ReturnDate date --= '2020-01-02'
)
RETURNS @numberOfAnnualLeaveDays TABLE
(
	year int,
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
AS
BEGIN

	declare @datesBetweenBothDates table
	(
		theDate datetime primary key
		,year int
		,isWeekend bit
		,isPublicHoliday bit
		,PublicHolidayMoved bit
	)

	if @ReturnDate >= @StartDate
	begin
		insert into @datesBetweenBothDates(theDate)
		select  
			top(datediff(day, @StartDate, @ReturnDate) + 1)
				Date = DATEADD(DAY, ROW_NUMBER() OVER(ORDER BY a.object_id) - 1, @StartDate)
		from    
			sys.all_objects a
		cross join sys.all_objects b
	end

	update 
		t
	set 
		t.year = year(t.theDate)
		,t.IsWeekend = iif(DATENAME(dw, t.theDate) in('Saturday', 'Sunday'), 1, 0)
		,t.PublicHolidayMoved = 0
	from 
		@datesBetweenBothDates t

	declare @years table (year int Primary Key, processed bit)

	insert into @years (year, processed)
	select Year(@StartDate), 0
	union
	select Year(@ReturnDate), 0

	declare @rowsToProcess int = 0
	declare @noOfRowsProcessed int = 0
	declare @currentYear int = 0

	select 
		@rowsToProcess = count(*) 
	from 
		@years

	declare @publicHolidays table
	(
		publicHoliday datetime primary key
	)

	while @noOfRowsProcessed < @rowsToProcess
	begin
		select 
			@currentYear = min(t.year)
		from 
			@years t
		where 
			t.processed = 0

		insert into @publicHolidays(publicHoliday)
		values
			(convert(datetime, convert(nvarchar, @currentYear) + '-01-01'))
			,(convert(datetime, convert(nvarchar, @currentYear) + '-12-25'))
			,(convert(datetime, convert(nvarchar, @currentYear) + '-12-26'))

		update 
			t
		set 
			t.processed = 1
		from @years t
		where 
			t.year = @currentYear

		set @noOfRowsProcessed = @noOfRowsProcessed + 1
	end

	update 
		t
	set 
		t.isPublicHoliday = iif(publicHoliday is not null, 1, 0)
	from 
		@datesBetweenBothDates t
	left join @publicHolidays publicHoliday on t.theDate = publicHoliday.publicHoliday

	declare @publicHolidayDaysOnWeekend table (theDate datetime primary key)

	insert into @publicHolidayDaysOnWeekend (theDate)
	select 
		t.theDate
	from 
		@datesBetweenBothDates t
	where 
		t.isPublicHoliday = 1
		and t.isWeekend = 1

	declare @rowsToProcessForPublicHolidays int = 0

	select 
		@rowsToProcessForPublicHolidays = count(*) 
	from 
		@publicHolidayDaysOnWeekend

	declare @lastPublicHolidayDaysOnWeekendDate datetime

	select 
		@lastPublicHolidayDaysOnWeekendDate = max(t.theDate)
	from 
		@publicHolidayDaysOnWeekend t

	declare @nextDateToProcess datetime

	declare @noOfRowsProcessedForPublicHolidays int = 0

	while @noOfRowsProcessedForPublicHolidays < @rowsToProcessForPublicHolidays
	begin
		select 
			@nextDateToProcess = min(t.theDate)
		from 
			@datesBetweenBothDates t
		where 
			t.isPublicHoliday = 0
			and t.isWeekend = 0
			and t.theDate > @lastPublicHolidayDaysOnWeekendDate
			
		update 
			t
		set 
			t.isPublicHoliday = 1
			,t.PublicHolidayMoved = 1
		from 
			@datesBetweenBothDates t
		where 
			t.theDate = @nextDateToProcess

		set @noOfRowsProcessedForPublicHolidays = @noOfRowsProcessedForPublicHolidays + 1
	end

	declare @noOfAnnualLeaveDays decimal(18, 2) = 0
	declare @noOfPublicLeaveDays decimal(18, 2) = 0
	declare @noOfLeaveDays decimal(18, 2) = 0

	insert into @numberOfAnnualLeaveDays 
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
		t.year
		,0 as numberOfDays
		,case
			when count(*) > 1 and min(t.year) = year(@ReturnDate) then
				count(*) - 1
			when count(*) > 1 and min(t.year) <> year(@ReturnDate) then
				count(*) 
			when count(*) = 1 and @StartDate <> @ReturnDate then
				count(*) 
			when count(*) = 1 and @StartDate = @ReturnDate then
				0.5
			else
				0
			end as numberOfAnnualLeaveDays
		,0 as numberOfPublicLeaveDays
		,0 as numberOfDaysLeft
		,0 as numberOfAnnualLeaveDaysLeft
		,0 as numberOfPublicLeaveDaysLeft
		,0 as numberOfDaysLeftAfterRequest
		,0 as numberOfAnnualLeaveDaysLeftAfterRequest
		,0 as numberOfPublicLeaveDaysLeftAfterRequest
		,0 as alreadyExists
		,0 as annualLeaveRequestID
	from 
		@datesBetweenBothDates t
	where 
		t.isWeekend = 0
		and t.isPublicHoliday = 0
	group by
		t.year

	update
		t
	set
		t.numberOfPublicLeaveDays = publicLeaveDaysPerYear.numberOfPublicLeaveDays
	from 
		@numberOfAnnualLeaveDays t
	inner join
	(
		select 
			t.year
			,case
				when count(*) > 1 then
					count(*)
				when count(*) = 1 and @StartDate <> @ReturnDate then
					count(*) 
				when count(*) = 1 and @StartDate = @ReturnDate then
					0.5
				else
					0
				end as numberOfPublicLeaveDays
		from 
			@datesBetweenBothDates t
		where 
			t.isPublicHoliday = 1
			and t.isWeekend = 0 
		group by
			t.year
	)publicLeaveDaysPerYear on t.year = publicLeaveDaysPerYear.year

	update
		t
	set
		t.numberOfDays = t.numberOfAnnualLeaveDays + t.numberOfPublicLeaveDays
		,t.numberOfDaysLeft = AnnualLeaveYear.NumberOfDaysLeft
		,t.numberOfAnnualLeaveDaysLeft = AnnualLeaveYear.NumberOfAnnualLeaveDaysLeft
		,t.numberOfPublicLeaveDaysLeft = AnnualLeaveYear.NumberOfPublicLeaveDaysLeft
		,t.numberOfDaysLeftAfterRequest = AnnualLeaveYear.NumberOfDaysLeft - (t.numberOfAnnualLeaveDays + t.numberOfPublicLeaveDays)
		,t.numberOfAnnualLeaveDaysLeftAfterRequest = AnnualLeaveYear.NumberOfAnnualLeaveDaysLeft - t.numberOfAnnualLeaveDays
		,t.numberOfPublicLeaveDaysLeftAfterRequest = AnnualLeaveYear.NumberOfPublicLeaveDaysLeft - t.numberOfPublicLeaveDays
	from 
		@numberOfAnnualLeaveDays t
	inner join AnnualLeaveYear on t.year = AnnualLeaveYear.Year

	declare @ExistingAnnualLeaveRequests table
	(
		AnnualLeaveRequestID int Primary Key
		,StartDate datetime
		,ReturnDate datetime
	)

	update
		t
	set
		t.alreadyExists = 1,
		t.annualLeaveRequestID = annualLeaveRequest.AnnualLeaveRequestID
	from 
		@numberOfAnnualLeaveDays t
	inner join annualLeaveRequest on t.year = annualLeaveRequest.Year
	where 
		(@StartDate between annualLeaveRequest.StartDate and annualLeaveRequest.ReturnDate
		or @ReturnDate between annualLeaveRequest.StartDate and annualLeaveRequest.ReturnDate)

	Return
End
