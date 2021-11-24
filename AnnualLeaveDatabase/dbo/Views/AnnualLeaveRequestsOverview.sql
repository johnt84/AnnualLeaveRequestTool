
CREATE VIEW [dbo].[AnnualLeaveRequestsOverview]
AS

    select 
		ar.AnnualLeaveRequestID as AnnualLeaveRequestID
		,ay.Year as Year
		,ar.PaidLeaveType as PaidLeaveType
		,ar.LeaveType as LeaveType
		,ar.StartDate as StartDate
		,ar.ReturnDate as ReturnDate
		,ar.NumberOfDays as NumberOfDaysRequested
		,ar.NumberOfAnnualLeaveDays as NumberOfAnnualLeaveDaysRequested
		,ar.NumberOfPublicLeaveDays as NumberOfPublicLeaveDaysRequested
		,ar.DateCreated as DateCreated
		,ar.Notes as Notes
		,ay.NumberOfDays as NumberOfDays
		,ay.NumberOfAnnualLeaveDays as NumberOfAnnualLeaveDays
		,ay.NumberOfPublicLeaveDays as NumberOfPublicLeaveDays
		,ay.NumberOfDaysLeft as NumberOfDaysLeftForYear
		,ay.NumberOfAnnualLeaveDaysLeft as NumberOfAnnualLeaveDaysLeftForYear
		,ay.NumberOfPublicLeaveDaysLeft as NumberOfPublicLeaveDaysLeftForYear
    from 
		AnnualLeaveYear ay 
    left join AnnualLeaveRequest ar on ar.Year = ay.Year

