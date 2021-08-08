USE [SparEnergiDb]
GO

/****** Object: Table [dbo].[Users] Script Date: 08-08-2021 20:36:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [Id]           INT        IDENTITY (1, 1) NOT NULL,
    [USERNAME]     NCHAR (50) NOT NULL,
    [PASSWORD]     NCHAR (50) NOT NULL,
    [EMAILADDRESS] NCHAR (50) NOT NULL,
    [FIRSTNAME]    NCHAR (50) NULL,
    [LASTNAME]     NCHAR (50) NULL,
    [STREETNAME]   NCHAR (50) NULL,
    [POSTCODE]     INT        NULL
);


