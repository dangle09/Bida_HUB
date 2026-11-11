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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [ActivityLogs] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Action] nvarchar(50) NOT NULL,
        [TableName] nvarchar(50) NULL,
        [RecordId] int NULL,
        [Description] nvarchar(255) NULL,
        [IpAddress] nvarchar(45) NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_ActivityLogs] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [AiMessages] (
        [Id] int NOT NULL IDENTITY,
        [AiSessionId] int NOT NULL,
        [Role] nvarchar(20) NOT NULL,
        [Content] nvarchar(4000) NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_AiMessages] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [AiSessions] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NULL,
        [CustomerId] int NULL,
        [SessionKey] nvarchar(50) NOT NULL,
        [Title] nvarchar(100) NOT NULL,
        [StartedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
        [LastActiveAt] datetime2 NOT NULL DEFAULT (GETDATE()),
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_AiSessions] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [AiStates] (
        [Id] int NOT NULL IDENTITY,
        [AiSessionId] int NULL,
        [StateKey] nvarchar(100) NOT NULL,
        [StateValue] nvarchar(4000) NOT NULL,
        [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_AiStates] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [BilliardTables] (
        [Id] int NOT NULL IDENTITY,
        [TableAreaId] int NOT NULL,
        [TableTypeId] int NOT NULL,
        [Code] nvarchar(20) NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [Status] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_BilliardTables] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(20) NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [CustomerCues] (
        [Id] int NOT NULL IDENTITY,
        [CustomerId] int NOT NULL,
        [Code] nvarchar(20) NOT NULL,
        [CueBrand] nvarchar(100) NULL,
        [ShaftType] nvarchar(100) NULL,
        [ReceivedDate] datetime2 NOT NULL,
        [Status] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_CustomerCues] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Customers] (
        [Id] int NOT NULL IDENTITY,
        [MembershipTierId] int NOT NULL,
        [Code] nvarchar(20) NOT NULL,
        [FullName] nvarchar(100) NOT NULL,
        [PhoneNumber] nvarchar(15) NOT NULL,
        [AccumulatedPoints] int NOT NULL,
        [AccountBalance] decimal(18,2) NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [ExpenseTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [Description] nvarchar(255) NULL,
        CONSTRAINT [PK_ExpenseTypes] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [GoodsReceiptDetails] (
        [Id] int NOT NULL IDENTITY,
        [GoodsReceiptId] int NOT NULL,
        [ServiceItemId] int NOT NULL,
        [Quantity] int NOT NULL,
        [ImportPrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_GoodsReceiptDetails] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [GoodsReceipts] (
        [Id] int NOT NULL IDENTITY,
        [SupplierId] int NOT NULL,
        [UserId] int NOT NULL,
        [Code] nvarchar(20) NOT NULL,
        [ImportDate] datetime2 NOT NULL,
        [TotalImportAmount] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_GoodsReceipts] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Inventories] (
        [Id] int NOT NULL IDENTITY,
        [ServiceItemId] int NOT NULL,
        [StockQuantity] int NOT NULL,
        [ReorderLevel] int NOT NULL,
        CONSTRAINT [PK_Inventories] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [InventoryAudits] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Code] nvarchar(20) NOT NULL,
        [AuditDate] datetime2 NOT NULL,
        [Notes] nvarchar(255) NULL,
        CONSTRAINT [PK_InventoryAudits] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [InvoiceDetails] (
        [Id] int NOT NULL IDENTITY,
        [InvoiceId] int NOT NULL,
        [ServiceItemId] int NOT NULL,
        [Quantity] int NOT NULL,
        [UnitPrice] decimal(18,2) NOT NULL,
        [TotalAmount] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_InvoiceDetails] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [InvoicePayments] (
        [Id] int NOT NULL IDENTITY,
        [InvoiceId] int NOT NULL,
        [PaymentMethodId] int NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [PaymentTime] datetime2 NOT NULL,
        CONSTRAINT [PK_InvoicePayments] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Invoices] (
        [Id] int NOT NULL IDENTITY,
        [BilliardTableId] int NOT NULL,
        [UserId] int NOT NULL,
        [CustomerId] int NOT NULL,
        [PromotionId] int NULL,
        [InvoiceCode] nvarchar(20) NOT NULL,
        [StartTime] datetime2 NOT NULL,
        [EndTime] datetime2 NULL,
        [TableAmount] decimal(18,2) NOT NULL,
        [ServiceAmount] decimal(18,2) NOT NULL,
        [DiscountAmount] decimal(18,2) NOT NULL,
        [TotalPayment] decimal(18,2) NOT NULL,
        [Status] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_Invoices] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [LockerRentals] (
        [Id] int NOT NULL IDENTITY,
        [LockerId] int NOT NULL,
        [CustomerId] int NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [MonthlyRent] decimal(18,2) NOT NULL,
        [Deposit] decimal(18,2) NOT NULL,
        [Status] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_LockerRentals] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Lockers] (
        [Id] int NOT NULL IDENTITY,
        [LockerNumber] nvarchar(20) NOT NULL,
        [Area] nvarchar(50) NULL,
        [Status] nvarchar(20) NOT NULL,
        [MonthlyRent] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Lockers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Matches] (
        [Id] int NOT NULL IDENTITY,
        [TournamentId] int NOT NULL,
        [BilliardTableId] int NOT NULL,
        [RoundName] nvarchar(50) NOT NULL,
        [Player1Id] int NOT NULL,
        [Player2Id] int NOT NULL,
        [ScorePlayer1] int NULL,
        [ScorePlayer2] int NULL,
        [WinnerId] int NULL,
        [MatchTime] datetime2 NOT NULL,
        [Status] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_Matches] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [MembershipTiers] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [MinPoints] int NOT NULL,
        [DiscountRate] decimal(5,2) NOT NULL,
        [Description] nvarchar(255) NULL,
        CONSTRAINT [PK_MembershipTiers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [PaymentMethods] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_PaymentMethods] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [PaymentVouchers] (
        [Id] int NOT NULL IDENTITY,
        [ExpenseTypeId] int NOT NULL,
        [UserId] int NOT NULL,
        [Code] nvarchar(20) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
        [ExpensePurpose] nvarchar(255) NOT NULL,
        [Notes] nvarchar(255) NULL,
        CONSTRAINT [PK_PaymentVouchers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Payrolls] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [SalaryMonth] int NOT NULL,
        [SalaryYear] int NOT NULL,
        [TotalHours] decimal(6,2) NOT NULL,
        [BaseSalary] decimal(18,2) NOT NULL,
        [Allowance] decimal(18,2) NOT NULL,
        [Bonus] decimal(18,2) NOT NULL,
        [Penalty] decimal(18,2) NOT NULL,
        [NetSalary] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Payrolls] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Permissions] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(50) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Module] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [PointTransactions] (
        [Id] int NOT NULL IDENTITY,
        [CustomerId] int NOT NULL,
        [TransactionType] nvarchar(20) NOT NULL,
        [Points] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
        [Notes] nvarchar(255) NULL,
        CONSTRAINT [PK_PointTransactions] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Promotions] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(20) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [DiscountPercent] decimal(5,2) NULL,
        [DiscountAmount] decimal(18,2) NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Promotions] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Reservations] (
        [Id] int NOT NULL IDENTITY,
        [CustomerId] int NOT NULL,
        [BilliardTableId] int NOT NULL,
        [ArrivalTime] datetime2 NOT NULL,
        [GuestCount] int NOT NULL,
        [Deposit] decimal(18,2) NOT NULL,
        [Status] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_Reservations] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [RolePermissions] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] int NOT NULL,
        [PermissionId] int NOT NULL,
        CONSTRAINT [PK_RolePermissions] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Roles] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [Description] nvarchar(255) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [ServiceItems] (
        [Id] int NOT NULL IDENTITY,
        [CategoryId] int NOT NULL,
        [Code] nvarchar(20) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Unit] nvarchar(20) NOT NULL,
        [UnitPrice] decimal(18,2) NOT NULL,
        [Image] nvarchar(255) NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_ServiceItems] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Shifts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [StartTime] time NOT NULL,
        [EndTime] time NOT NULL,
        [HourlyRate] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Shifts] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Suppliers] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [PhoneNumber] nvarchar(15) NOT NULL,
        [Address] nvarchar(200) NULL,
        CONSTRAINT [PK_Suppliers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [TableAreas] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(20) NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_TableAreas] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [TableEquipments] (
        [Id] int NOT NULL IDENTITY,
        [BilliardTableId] int NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Quantity] int NOT NULL,
        [Status] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_TableEquipments] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [TableMaintenances] (
        [Id] int NOT NULL IDENTITY,
        [BilliardTableId] int NOT NULL,
        [MaintenanceType] nvarchar(100) NOT NULL,
        [Cost] decimal(18,2) NOT NULL,
        [MaintenanceDate] datetime2 NOT NULL,
        [PerformedBy] nvarchar(100) NULL,
        [Notes] nvarchar(255) NULL,
        CONSTRAINT [PK_TableMaintenances] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [TableTransfers] (
        [Id] int NOT NULL IDENTITY,
        [InvoiceId] int NOT NULL,
        [FromTableId] int NOT NULL,
        [ToTableId] int NOT NULL,
        [TransferTime] datetime2 NOT NULL,
        [Notes] nvarchar(255) NULL,
        CONSTRAINT [PK_TableTransfers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [TableTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [BasePrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_TableTypes] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Timekeepings] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [WorkDate] datetime2 NOT NULL,
        [CheckInTime] datetime2 NOT NULL,
        [CheckOutTime] datetime2 NULL,
        [WorkHours] decimal(4,2) NULL,
        [LateMinutes] int NULL,
        [EarlyLeaveMinutes] int NULL,
        CONSTRAINT [PK_Timekeepings] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [TimeSlotPrices] (
        [Id] int NOT NULL IDENTITY,
        [TableTypeId] int NOT NULL,
        [SlotStartTime] time NOT NULL,
        [SlotEndTime] time NOT NULL,
        [SlotPrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_TimeSlotPrices] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [TournamentParticipants] (
        [Id] int NOT NULL IDENTITY,
        [TournamentId] int NOT NULL,
        [CustomerId] int NOT NULL,
        [SeedNumber] int NULL,
        [RegisteredDate] datetime2 NOT NULL,
        [FinalRank] int NULL,
        CONSTRAINT [PK_TournamentParticipants] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Tournaments] (
        [Id] int NOT NULL IDENTITY,
        [TournamentName] nvarchar(100) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [EntryFee] decimal(18,2) NOT NULL,
        [TotalPrize] decimal(18,2) NOT NULL,
        [Status] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_Tournaments] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [UserRoles] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [RoleId] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [Username] nvarchar(50) NOT NULL,
        [PasswordHash] nvarchar(255) NOT NULL,
        [FullName] nvarchar(100) NOT NULL,
        [Email] nvarchar(100) NULL,
        [PhoneNumber] nvarchar(15) NULL,
        [Avatar] nvarchar(255) NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    CREATE TABLE [WorkSchedules] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ShiftId] int NOT NULL,
        [WorkDate] datetime2 NOT NULL,
        [Notes] nvarchar(255) NULL,
        CONSTRAINT [PK_WorkSchedules] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111155220_InitialCreateWithAiSessionsMessagesStates'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261111155220_InitialCreateWithAiSessionsMessagesStates', N'10.0.0');
END;

COMMIT;
GO

