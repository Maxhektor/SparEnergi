USE [SparEnergiDb]
GO

/****** Object: Table [dbo].[Meetings] Script Date: 08-08-2021 20:34:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Meetings] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Date]             DATE           NOT NULL,
    [MeetingRequester] NCHAR (40)     NOT NULL,
    [PhoneNumber]      NCHAR (8)      NOT NULL,
    [RequestContent]   NVARCHAR (MAX) NOT NULL
);


