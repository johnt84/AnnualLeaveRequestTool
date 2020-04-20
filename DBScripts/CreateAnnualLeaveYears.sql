declare @startYear int = 2019
,@numberOfDays decimal(18, 2) = 28
,@numberOfAnnualLeaveDays decimal(18, 2) = 25
,@numberOfPublicLeaveDays decimal(18, 2) = 3

;with yearlist as 
(
    select @startYear as year
    union all
    select yl.year + 1 as year
    from yearlist yl
    where yl.year + 1 <= YEAR(GetDate()) + 1
)

insert into AnnualLeaveYear
(
	year,
	NumberOfDays,
	NumberOfAnnualLeaveDays,
	NumberOfPublicLeaveDays,
	NumberOfDaysLeft,
	NumberOfAnnualLeaveDaysLeft,
	NumberOfPublicLeaveDaysLeft
)
select 
	t.year 
	,@numberOfDays
	,@numberOfAnnualLeaveDays
	,@numberOfPublicLeaveDays
	,@numberOfDays
	,@numberOfAnnualLeaveDays
	,@numberOfPublicLeaveDays
from 
	yearlist t