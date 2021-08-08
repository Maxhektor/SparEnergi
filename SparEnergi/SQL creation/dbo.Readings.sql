USE [SparEnergiDb]
GO

/****** Object: Table [dbo].[Readings] Script Date: 08-08-2021 20:35:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Readings] (
    [Id]         INT       IDENTITY (1, 1) NOT NULL,
    [Date]       DATETIME  NOT NULL,
    [UserId]     INT       NOT NULL,
    [EnergyUsed] INT       NOT NULL,
    [EnergyUnit] NCHAR (3) NOT NULL,
    [WaterUsed]  INT       NOT NULL,
    [WaterUnit]  NCHAR (2) NOT NULL
);


