
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/29/2015 15:13:08
-- Generated from EDMX file: D:\Visual Studio 2015\Projects\EpamProjects\Model\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Project4];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProductSaleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleInfoSet] DROP CONSTRAINT [FK_ProductSaleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ManagerFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FileInformationSet] DROP CONSTRAINT [FK_ManagerFile];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientSaleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleInfoSet] DROP CONSTRAINT [FK_ClientSaleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_SaleInfoFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleInfoSet] DROP CONSTRAINT [FK_SaleInfoFile];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ManagerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ManagerSet];
GO
IF OBJECT_ID(N'[dbo].[ClientSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientSet];
GO
IF OBJECT_ID(N'[dbo].[FileInformationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileInformationSet];
GO
IF OBJECT_ID(N'[dbo].[ProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSet];
GO
IF OBJECT_ID(N'[dbo].[SaleInfoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SaleInfoSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ManagerSet'
CREATE TABLE [dbo].[ManagerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SecondName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ClientSet'
CREATE TABLE [dbo].[ClientSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [SecondName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FileInformationSet'
CREATE TABLE [dbo].[FileInformationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [ManagerId] int  NOT NULL
);
GO

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SaleInfoSet'
CREATE TABLE [dbo].[SaleInfoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [ProductId] int  NOT NULL,
    [ClientId] int  NOT NULL,
    [Cost] decimal(18,0)  NOT NULL,
    [FileId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ManagerSet'
ALTER TABLE [dbo].[ManagerSet]
ADD CONSTRAINT [PK_ManagerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientSet'
ALTER TABLE [dbo].[ClientSet]
ADD CONSTRAINT [PK_ClientSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileInformationSet'
ALTER TABLE [dbo].[FileInformationSet]
ADD CONSTRAINT [PK_FileInformationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SaleInfoSet'
ALTER TABLE [dbo].[SaleInfoSet]
ADD CONSTRAINT [PK_SaleInfoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProductId] in table 'SaleInfoSet'
ALTER TABLE [dbo].[SaleInfoSet]
ADD CONSTRAINT [FK_ProductSaleInfo]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductSaleInfo'
CREATE INDEX [IX_FK_ProductSaleInfo]
ON [dbo].[SaleInfoSet]
    ([ProductId]);
GO

-- Creating foreign key on [ManagerId] in table 'FileInformationSet'
ALTER TABLE [dbo].[FileInformationSet]
ADD CONSTRAINT [FK_ManagerFile]
    FOREIGN KEY ([ManagerId])
    REFERENCES [dbo].[ManagerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerFile'
CREATE INDEX [IX_FK_ManagerFile]
ON [dbo].[FileInformationSet]
    ([ManagerId]);
GO

-- Creating foreign key on [ClientId] in table 'SaleInfoSet'
ALTER TABLE [dbo].[SaleInfoSet]
ADD CONSTRAINT [FK_ClientSaleInfo]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[ClientSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientSaleInfo'
CREATE INDEX [IX_FK_ClientSaleInfo]
ON [dbo].[SaleInfoSet]
    ([ClientId]);
GO

-- Creating foreign key on [FileId] in table 'SaleInfoSet'
ALTER TABLE [dbo].[SaleInfoSet]
ADD CONSTRAINT [FK_SaleInfoFile]
    FOREIGN KEY ([FileId])
    REFERENCES [dbo].[FileInformationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SaleInfoFile'
CREATE INDEX [IX_FK_SaleInfoFile]
ON [dbo].[SaleInfoSet]
    ([FileId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------