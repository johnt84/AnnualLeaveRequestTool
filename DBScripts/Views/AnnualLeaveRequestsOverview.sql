USE [AnnualLeave]
GO

/****** Object:  View [dbo].[AnnualLeaveRequestsOverview]    Script Date: 18/04/2020 01:31:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





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


GO


