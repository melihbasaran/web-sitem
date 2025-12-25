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
CREATE TABLE [Recipes] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [PrepTime] int NOT NULL,
    [Servings] int NOT NULL,
    [Ingredients] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Recipes] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251216093751_IlkKurulum', N'9.0.0');

CREATE TABLE [ShoppingItems] (
    [Id] int NOT NULL IDENTITY,
    [ItemName] nvarchar(max) NOT NULL,
    [IsChecked] bit NOT NULL,
    CONSTRAINT [PK_ShoppingItems] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251216101041_AlisverisListesiEklendi', N'9.0.0');

COMMIT;
GO

