CREATE TABLE [dbo].[AnnualLeaveYear] (
    [Year]                        INT             NOT NULL,
    [NumberOfDays]                DECIMAL (18, 2) NOT NULL,
    [NumberOfAnnualLeaveDays]     DECIMAL (18, 2) NOT NULL,
    [NumberOfPublicLeaveDays]     DECIMAL (18, 2) NOT NULL,
    [NumberOfDaysLeft]            DECIMAL (18, 2) NOT NULL,
    [NumberOfAnnualLeaveDaysLeft] DECIMAL (18, 2) NOT NULL,
    [NumberOfPublicLeaveDaysLeft] DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_AnnualLeaveYear] PRIMARY KEY CLUSTERED ([Year] ASC)
);

