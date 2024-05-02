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
GO

CREATE TABLE [SearchHistories] (
    [Id] int NOT NULL IDENTITY,
    [Keyword] nvarchar(255) NULL,
    [Url] nvarchar(255) NULL,
    [Rank] int NOT NULL,
    [SearchDate] datetime2 NOT NULL DEFAULT (getutcdate()),
    CONSTRAINT [PK_SearchHistories] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_SearchHistories_Url_Keyword_SearchDate] ON [SearchHistories] ([Url], [Keyword], [SearchDate]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240429005831_InitSearchHistoryTable', N'8.0.4');
GO

COMMIT;
GO