
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/28/2019 21:20:34
-- Generated from EDMX file: D:\Learn Social\SideTrade\sideTrade.myProfilo\sideTrade.Dal\sideTradeDbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SideTrade_Dev];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProfileUpload]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FileManager] DROP CONSTRAINT [FK_ProfileUpload];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileLogin]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Login] DROP CONSTRAINT [FK_ProfileLogin];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileProfileMapping]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProfileMapping] DROP CONSTRAINT [FK_ProfileProfileMapping];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileTypeProfileMapping]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProfileMapping] DROP CONSTRAINT [FK_ProfileTypeProfileMapping];
GO
IF OBJECT_ID(N'[dbo].[FK_NotificationTypeNotification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notification] DROP CONSTRAINT [FK_NotificationTypeNotification];
GO
IF OBJECT_ID(N'[dbo].[FK_LogTypeLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Log] DROP CONSTRAINT [FK_LogTypeLog];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FileManager]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileManager];
GO
IF OBJECT_ID(N'[dbo].[Notification]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notification];
GO
IF OBJECT_ID(N'[dbo].[Login]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Login];
GO
IF OBJECT_ID(N'[dbo].[Profile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Profile];
GO
IF OBJECT_ID(N'[dbo].[ProfileMapping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProfileMapping];
GO
IF OBJECT_ID(N'[dbo].[ProfileType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProfileType];
GO
IF OBJECT_ID(N'[dbo].[Settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Settings];
GO
IF OBJECT_ID(N'[dbo].[NotificationType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NotificationType];
GO
IF OBJECT_ID(N'[dbo].[Log]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Log];
GO
IF OBJECT_ID(N'[dbo].[LogType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogType];
GO
IF OBJECT_ID(N'[dbo].[FileManagerType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileManagerType];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'FileManager'
CREATE TABLE [dbo].[FileManager] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProfileId] int  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [Path] nvarchar(max)  NULL,
    [Mode] nvarchar(200)  NOT NULL,
    [FileName] nvarchar(max)  NULL,
    [Status] nvarchar(200)  NOT NULL,
    [Comment] nvarchar(max)  NULL,
    [FileManagerTypeId] int  NOT NULL
);
GO

-- Creating table 'Notification'
CREATE TABLE [dbo].[Notification] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SenderProfileId] int  NOT NULL,
    [RecipientProfileId] int  NULL,
    [FromEmail] nvarchar(200)  NOT NULL,
    [ToEmail] nvarchar(200)  NOT NULL,
    [Subject] nvarchar(200)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Link] nvarchar(max)  NOT NULL,
    [ReadOn] datetime  NULL,
    [NotificationTypeId] int  NOT NULL,
    [IsHTML] bit  NULL,
    [CreatedOn] datetime  NOT NULL,
    [UpdatedOn] datetime  NULL
);
GO

-- Creating table 'Login'
CREATE TABLE [dbo].[Login] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL,
    [IsLock] bit  NULL,
    [IsActive] bit  NOT NULL,
    [Profile_Id] int  NOT NULL
);
GO

-- Creating table 'Profile'
CREATE TABLE [dbo].[Profile] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nchar(100)  NULL,
    [LastName] nvarchar(100)  NULL,
    [EmailAddress] nvarchar(250)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsInvited] bit  NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL
);
GO

-- Creating table 'ProfileMapping'
CREATE TABLE [dbo].[ProfileMapping] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProfileId] int  NOT NULL,
    [ProfileTypeId] int  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'ProfileType'
CREATE TABLE [dbo].[ProfileType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'NotificationType'
CREATE TABLE [dbo].[NotificationType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [TemplateName] nvarchar(200)  NOT NULL
);
GO

-- Creating table 'Log'
CREATE TABLE [dbo].[Log] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LogTypeId] int  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ProfileId] int  NULL
);
GO

-- Creating table 'LogType'
CREATE TABLE [dbo].[LogType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL
);
GO

-- Creating table 'FileManagerType'
CREATE TABLE [dbo].[FileManagerType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileType] nvarchar(200)  NOT NULL,
    [MaxMBSize] nvarchar(200)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [UpdatedOn] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'FileManager'
ALTER TABLE [dbo].[FileManager]
ADD CONSTRAINT [PK_FileManager]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notification'
ALTER TABLE [dbo].[Notification]
ADD CONSTRAINT [PK_Notification]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Login'
ALTER TABLE [dbo].[Login]
ADD CONSTRAINT [PK_Login]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Profile'
ALTER TABLE [dbo].[Profile]
ADD CONSTRAINT [PK_Profile]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProfileMapping'
ALTER TABLE [dbo].[ProfileMapping]
ADD CONSTRAINT [PK_ProfileMapping]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProfileType'
ALTER TABLE [dbo].[ProfileType]
ADD CONSTRAINT [PK_ProfileType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [PK_Settings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NotificationType'
ALTER TABLE [dbo].[NotificationType]
ADD CONSTRAINT [PK_NotificationType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Log'
ALTER TABLE [dbo].[Log]
ADD CONSTRAINT [PK_Log]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LogType'
ALTER TABLE [dbo].[LogType]
ADD CONSTRAINT [PK_LogType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileManagerType'
ALTER TABLE [dbo].[FileManagerType]
ADD CONSTRAINT [PK_FileManagerType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProfileId] in table 'FileManager'
ALTER TABLE [dbo].[FileManager]
ADD CONSTRAINT [FK_ProfileUpload]
    FOREIGN KEY ([ProfileId])
    REFERENCES [dbo].[Profile]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileUpload'
CREATE INDEX [IX_FK_ProfileUpload]
ON [dbo].[FileManager]
    ([ProfileId]);
GO

-- Creating foreign key on [Profile_Id] in table 'Login'
ALTER TABLE [dbo].[Login]
ADD CONSTRAINT [FK_ProfileLogin]
    FOREIGN KEY ([Profile_Id])
    REFERENCES [dbo].[Profile]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileLogin'
CREATE INDEX [IX_FK_ProfileLogin]
ON [dbo].[Login]
    ([Profile_Id]);
GO

-- Creating foreign key on [ProfileId] in table 'ProfileMapping'
ALTER TABLE [dbo].[ProfileMapping]
ADD CONSTRAINT [FK_ProfileProfileMapping]
    FOREIGN KEY ([ProfileId])
    REFERENCES [dbo].[Profile]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileProfileMapping'
CREATE INDEX [IX_FK_ProfileProfileMapping]
ON [dbo].[ProfileMapping]
    ([ProfileId]);
GO

-- Creating foreign key on [ProfileTypeId] in table 'ProfileMapping'
ALTER TABLE [dbo].[ProfileMapping]
ADD CONSTRAINT [FK_ProfileTypeProfileMapping]
    FOREIGN KEY ([ProfileTypeId])
    REFERENCES [dbo].[ProfileType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileTypeProfileMapping'
CREATE INDEX [IX_FK_ProfileTypeProfileMapping]
ON [dbo].[ProfileMapping]
    ([ProfileTypeId]);
GO

-- Creating foreign key on [NotificationTypeId] in table 'Notification'
ALTER TABLE [dbo].[Notification]
ADD CONSTRAINT [FK_NotificationTypeNotification]
    FOREIGN KEY ([NotificationTypeId])
    REFERENCES [dbo].[NotificationType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NotificationTypeNotification'
CREATE INDEX [IX_FK_NotificationTypeNotification]
ON [dbo].[Notification]
    ([NotificationTypeId]);
GO

-- Creating foreign key on [LogTypeId] in table 'Log'
ALTER TABLE [dbo].[Log]
ADD CONSTRAINT [FK_LogTypeLog]
    FOREIGN KEY ([LogTypeId])
    REFERENCES [dbo].[LogType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LogTypeLog'
CREATE INDEX [IX_FK_LogTypeLog]
ON [dbo].[Log]
    ([LogTypeId]);
GO

-- Creating foreign key on [FileManagerTypeId] in table 'FileManager'
ALTER TABLE [dbo].[FileManager]
ADD CONSTRAINT [FK_FileManagerTypeFileManager]
    FOREIGN KEY ([FileManagerTypeId])
    REFERENCES [dbo].[FileManagerType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileManagerTypeFileManager'
CREATE INDEX [IX_FK_FileManagerTypeFileManager]
ON [dbo].[FileManager]
    ([FileManagerTypeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------