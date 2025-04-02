IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Todos] (
    [Id] uniqueidentifier NOT NULL,
    [Title] varchar(100) NOT NULL,
    [Description] varchar(100) NOT NULL,
    [Status] int NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [CompletionDate] datetime2 NULL,
    [Assignee] varchar(100) NOT NULL,
    [Priority] int NOT NULL,
    CONSTRAINT [PK_Todos] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250402014205_InitialCreate', N'9.0.3');

COMMIT;
GO

