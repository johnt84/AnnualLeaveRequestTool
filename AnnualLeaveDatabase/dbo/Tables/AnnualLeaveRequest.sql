CREATE TABLE [dbo].[AnnualLeaveRequest] (
    [AnnualLeaveRequestID]           INT             IDENTITY (1, 1) NOT NULL,
    [PaidLeaveType]                  NVARCHAR (6)    NOT NULL,
    [LeaveType]                      NVARCHAR (25)   NOT NULL,
    [StartDate]                      DATE            NOT NULL,
    [ReturnDate]                     DATE            NOT NULL,
    [NumberOfDays]                   DECIMAL (18, 2) NOT NULL,
    [NumberOfAnnualLeaveDays]        DECIMAL (18, 2) NOT NULL,
    [NumberOfPublicLeaveDays]        DECIMAL (18, 2) NOT NULL,
    [DateCreated]                    DATETIME        NOT NULL,
    [Notes]                          NVARCHAR (150)  NULL,
    [Year]                           INT             NOT NULL,
    [AssociatedAnnualLeaveRequestID] INT             NULL,
    CONSTRAINT [PK_AnnualLeaveRequest] PRIMARY KEY CLUSTERED ([AnnualLeaveRequestID] ASC)
);

