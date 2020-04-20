USE [AnnualLeave]
GO

/****** Object:  Table [dbo].[AnnualLeaveYear]    Script Date: 18/04/2020 01:29:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AnnualLeaveYear](
	[Year] [int] NOT NULL,
	[NumberOfDays] [decimal](18, 2) NOT NULL,
	[NumberOfAnnualLeaveDays] [decimal](18, 2) NOT NULL,
	[NumberOfPublicLeaveDays] [decimal](18, 2) NOT NULL,
	[NumberOfDaysLeft] [decimal](18, 2) NOT NULL,
	[NumberOfAnnualLeaveDaysLeft] [decimal](18, 2) NOT NULL,
	[NumberOfPublicLeaveDaysLeft] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_AnnualLeaveYear] PRIMARY KEY CLUSTERED 
(
	[Year] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


