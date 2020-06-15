--Create Database
CREATE DATABASE [WordCount]




--Create table
USE [WordCount]
GO

/****** Object:  Table [dbo].[WordCount] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WordCount](
	[Id] [nvarchar](50) NOT NULL,
	[Word] [nvarchar](max) NULL,
	[Count] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

