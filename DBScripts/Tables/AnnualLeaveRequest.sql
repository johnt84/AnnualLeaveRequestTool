USE [AnnualLeave]
GO

/****** Object:  Table [dbo].[AnnualLeaveRequest]    Script Date: 18/04/2020 01:27:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AnnualLeaveRequest](
	[AnnualLeaveRequestID] [int] IDENTITY(1,1) NOT NULL,
	[PaidLeaveType] [nvarchar](6) NOT NULL,
	[LeaveType] [nvarchar](25) NOT NULL,
	[StartDate] [date] NOT NULL,
	[ReturnDate] [date] NOT NULL,
	[NumberOfDays] [decimal](18, 2) NOT NULL,
	[NumberOfAnnualLeaveDays] [decimal](18, 2) NOT NULL,
	[NumberOfPublicLeaveDays] [decimal](18, 2) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Notes] [nvarchar](150) NULL,
	[Year] [int] NOT NULL,
	[AssociatedAnnualLeaveRequestID] [int] NULL,
 CONSTRAINT [PK_AnnualLeaveRequest] PRIMARY KEY CLUSTERED 
(
	[AnnualLeaveRequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


