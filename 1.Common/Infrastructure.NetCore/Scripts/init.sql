IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Order] (
    [Id] int NOT NULL IDENTITY,
    [CreateTime] datetime2 NOT NULL DEFAULT (getdate()),
    [OrderNo] varchar(20) NOT NULL,
    [TotalAmount] decimal(18, 2) NOT NULL,
    [UserName] nvarchar(max) NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [OrderItem] (
    [Id] int NOT NULL IDENTITY,
    [CreateTime] datetime2 NOT NULL,
    [ProductName] nvarchar(max) NULL,
    [Qty] int NOT NULL,
    [Price] decimal(18, 2) NOT NULL,
    [OrderId] int NOT NULL,
    CONSTRAINT [PK_OrderItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItem_Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Order] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_OrderItem_OrderId] ON [OrderItem] ([OrderId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180929114540_Initial', N'2.1.3-rtm-32065');

GO

