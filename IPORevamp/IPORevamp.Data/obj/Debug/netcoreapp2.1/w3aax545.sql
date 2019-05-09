IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [IsDefault] bit NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [MiddleName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Gender] int NOT NULL,
    [DoB] datetime2 NOT NULL,
    [MobileNumber] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [DateCreated] datetime2 NOT NULL,
    [RowVersion] rowversion NULL,
    [LastUpdateDate] datetime2 NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [EmailLog] (
    [Id] int NOT NULL IDENTITY,
    [Sender] nvarchar(max) NULL,
    [Receiver] nvarchar(max) NULL,
    [CC] nvarchar(max) NULL,
    [BCC] nvarchar(max) NULL,
    [Subject] nvarchar(max) NULL,
    [MailBody] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [DateSent] datetime2 NOT NULL,
    [DateToSend] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [LastUpdateDate] datetime2 NULL,
    [RowVersion] varbinary(max) NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_EmailLog] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [EmailTemplates] (
    [Id] int NOT NULL IDENTITY,
    [EmailName] nvarchar(max) NULL,
    [EmailBody] nvarchar(max) NULL,
    [EmailSubject] nvarchar(max) NULL,
    [EmailTemplateType] int NULL,
    [EmailSender] nvarchar(max) NULL,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [LastUpdateDate] datetime2 NULL,
    [RowVersion] varbinary(max) NULL,
    CONSTRAINT [PK_EmailTemplates] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Menu] (
    [Id] int NOT NULL IDENTITY,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [LastUpdateDate] datetime2 NULL,
    [RowVersion] varbinary(max) NULL,
    [Path] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Icon] nvarchar(max) NULL,
    [Badge] nvarchar(max) NULL,
    [BadgeClass] nvarchar(max) NULL,
    [IsExternalLink] bit NOT NULL,
    [Roles] nvarchar(max) NULL,
    [MenuId] int NULL,
    CONSTRAINT [PK_Menu] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Menu_Menu_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [Menu] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] int NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [RoleMenu] (
    [Id] int NOT NULL IDENTITY,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [LastUpdateDate] datetime2 NULL,
    [RowVersion] varbinary(max) NULL,
    [MenuId] int NULL,
    [RoleId] int NULL,
    CONSTRAINT [PK_RoleMenu] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleMenu_Menu_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [Menu] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RoleMenu_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE INDEX [IX_Menu_MenuId] ON [Menu] ([MenuId]);

GO

CREATE INDEX [IX_RoleMenu_MenuId] ON [RoleMenu] ([MenuId]);

GO

CREATE INDEX [IX_RoleMenu_RoleId] ON [RoleMenu] ([RoleId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190216161735_InitialMigration', N'2.1.4-rtm-31024');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EmailLog]') AND [c].[name] = N'DateSent');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [EmailLog] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [EmailLog] ALTER COLUMN [DateSent] datetime2 NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190219100247_EmailLogDateToSendNullable', N'2.1.4-rtm-31024');

GO

CREATE TABLE [AuditTrails] (
    [UserId] int NOT NULL,
    [UserName] nvarchar(max) NULL,
    [ActionTaken] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Entity] nvarchar(max) NULL,
    [Id] int NOT NULL IDENTITY,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [LastUpdateDate] datetime2 NULL,
    [RowVersion] varbinary(max) NULL,
    CONSTRAINT [PK_AuditTrails] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190219134249_AuditTrail', N'2.1.4-rtm-31024');

GO

CREATE TABLE [Quotes] (
    [Id] int NOT NULL IDENTITY,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [LastUpdateDate] datetime2 NULL,
    [RowVersion] varbinary(max) NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [EventName] nvarchar(max) NULL,
    [CountryCode] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    [State] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [NumberofAttendees] int NOT NULL,
    [Duration] int NOT NULL,
    [Referer] nvarchar(max) NULL,
    [RequestdById] int NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Quotes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Quotes_AspNetUsers_RequestdById] FOREIGN KEY ([RequestdById]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [EventFeature] (
    [Id] int NOT NULL IDENTITY,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [LastUpdateDate] datetime2 NULL,
    [RowVersion] varbinary(max) NULL,
    [Name] nvarchar(max) NULL,
    [PriceQuoteId] int NULL,
    CONSTRAINT [PK_EventFeature] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EventFeature_Quotes_PriceQuoteId] FOREIGN KEY ([PriceQuoteId]) REFERENCES [Quotes] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_EventFeature_PriceQuoteId] ON [EventFeature] ([PriceQuoteId]);

GO

CREATE INDEX [IX_Quotes_RequestdById] ON [Quotes] ([RequestdById]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190221151307_FeaturesAndQUotes', N'2.1.4-rtm-31024');

GO

ALTER TABLE [EventFeature] DROP CONSTRAINT [FK_EventFeature_Quotes_PriceQuoteId];

GO

ALTER TABLE [EventFeature] DROP CONSTRAINT [PK_EventFeature];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Quotes]') AND [c].[name] = N'UserId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Quotes] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Quotes] DROP COLUMN [UserId];

GO

EXEC sp_rename N'[EventFeature]', N'EventFeatures';

GO

EXEC sp_rename N'[EventFeatures].[IX_EventFeature_PriceQuoteId]', N'IX_EventFeatures_PriceQuoteId', N'INDEX';

GO

ALTER TABLE [EventFeatures] ADD CONSTRAINT [PK_EventFeatures] PRIMARY KEY ([Id]);

GO

ALTER TABLE [EventFeatures] ADD CONSTRAINT [FK_EventFeatures_Quotes_PriceQuoteId] FOREIGN KEY ([PriceQuoteId]) REFERENCES [Quotes] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190221163743_FeaturesAndQUotesII', N'2.1.4-rtm-31024');

GO

ALTER TABLE [AspNetUsers] ADD [Bio] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [CountryCode] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [EmployerName] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [FaceBook] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [GooglePlus] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [Instagram] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [Interests] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [Nationality] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [Occupation] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [ProfilePicLoc] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [ResidentialAddress] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [Title] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [AspNetUsers] ADD [Twitter] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190225145646_event', N'2.1.4-rtm-31024');

GO

CREATE TABLE [Events] (
    [Id] int NOT NULL IDENTITY,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [LastUpdateDate] datetime2 NULL,
    [RowVersion] varbinary(max) NULL,
    [EventName] nvarchar(max) NULL,
    [EventLogo] nvarchar(max) NULL,
    [EventBanner] nvarchar(max) NULL,
    [EventDescription] nvarchar(max) NULL,
    [OrganizerDescription] nvarchar(max) NULL,
    [EventSocialMediaHandle] nvarchar(max) NULL,
    [BrandColor] nvarchar(max) NULL,
    [IsRegistrationOutside] bit NOT NULL,
    [RegistrationURL] nvarchar(max) NULL,
    [EventRegistrationLink] nvarchar(max) NULL,
    [IsPaid] bit NOT NULL,
    [RegistrationFee] decimal(18, 2) NOT NULL,
    [OrganizerId] int NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Events_AspNetUsers_OrganizerId] FOREIGN KEY ([OrganizerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AttendeeTypes] (
    [Id] int NOT NULL IDENTITY,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [LastUpdateDate] datetime2 NULL,
    [RowVersion] varbinary(max) NULL,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [EventId] int NOT NULL,
    CONSTRAINT [PK_AttendeeTypes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AttendeeTypes_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Attendees] (
    [Id] int NOT NULL IDENTITY,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [LastUpdateDate] datetime2 NULL,
    [RowVersion] varbinary(max) NULL,
    [InvitationCode] nvarchar(max) NULL,
    [EventId] int NOT NULL,
    [AttendeeTypeId] int NULL,
    CONSTRAINT [PK_Attendees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Attendees_AttendeeTypes_AttendeeTypeId] FOREIGN KEY ([AttendeeTypeId]) REFERENCES [AttendeeTypes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Attendees_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Attendees_AttendeeTypeId] ON [Attendees] ([AttendeeTypeId]);

GO

CREATE INDEX [IX_Attendees_EventId] ON [Attendees] ([EventId]);

GO

CREATE INDEX [IX_AttendeeTypes_EventId] ON [AttendeeTypes] ([EventId]);

GO

CREATE INDEX [IX_Events_OrganizerId] ON [Events] ([OrganizerId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190225150448_eventI', N'2.1.4-rtm-31024');

GO

ALTER TABLE [AttendeeTypes] ADD [RegistrationFee] decimal(18, 2) NOT NULL DEFAULT 0.0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190225150724_eventII', N'2.1.4-rtm-31024');

GO

ALTER TABLE [Attendees] ADD [UserId] int NOT NULL DEFAULT 0;

GO

CREATE INDEX [IX_Attendees_UserId] ON [Attendees] ([UserId]);

GO

ALTER TABLE [Attendees] ADD CONSTRAINT [FK_Attendees_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190225160217_eventIII', N'2.1.4-rtm-31024');

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Events]') AND [c].[name] = N'RegistrationFee');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Events] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Events] DROP COLUMN [RegistrationFee];

GO

ALTER TABLE [Events] ADD [Location] nvarchar(max) NULL;

GO

ALTER TABLE [Events] ADD [Tags] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190226102043_EventIV', N'2.1.4-rtm-31024');

GO

ALTER TABLE [Events] ADD [NoOfAttendees] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190226123757_EventV', N'2.1.4-rtm-31024');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190226143042_EventVI', N'2.1.4-rtm-31024');

GO

ALTER TABLE [EventFeatures] ADD [Description] nvarchar(max) NULL;

GO

ALTER TABLE [EventFeatures] ADD [EventInfoId] int NULL;

GO

ALTER TABLE [EventFeatures] ADD [Price] decimal(18, 2) NOT NULL DEFAULT 0.0;

GO

CREATE INDEX [IX_EventFeatures_EventInfoId] ON [EventFeatures] ([EventInfoId]);

GO

ALTER TABLE [EventFeatures] ADD CONSTRAINT [FK_EventFeatures_Events_EventInfoId] FOREIGN KEY ([EventInfoId]) REFERENCES [Events] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190228165037_EventFeatures', N'2.1.4-rtm-31024');

GO

ALTER TABLE [EventFeatures] DROP CONSTRAINT [FK_EventFeatures_Events_EventInfoId];

GO

DROP INDEX [IX_EventFeatures_EventInfoId] ON [EventFeatures];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EventFeatures]') AND [c].[name] = N'EventInfoId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [EventFeatures] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [EventFeatures] DROP COLUMN [EventInfoId];

GO

CREATE TABLE [EventEventFeature] (
    [EventId] int NOT NULL,
    [FeatureId] int NOT NULL,
    CONSTRAINT [PK_EventEventFeature] PRIMARY KEY ([EventId], [FeatureId]),
    CONSTRAINT [FK_EventEventFeature_EventFeatures_EventId] FOREIGN KEY ([EventId]) REFERENCES [EventFeatures] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EventEventFeature_Events_FeatureId] FOREIGN KEY ([FeatureId]) REFERENCES [Events] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_EventEventFeature_FeatureId] ON [EventEventFeature] ([FeatureId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190228172028_EventFeaturesI', N'2.1.4-rtm-31024');

GO

ALTER TABLE [Attendees] ADD [Bio] nvarchar(max) NULL;

GO

ALTER TABLE [Attendees] ADD [CountryCode] nvarchar(max) NULL;

GO

ALTER TABLE [Attendees] ADD [DoB] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [Attendees] ADD [EmployerName] nvarchar(max) NULL;

GO

ALTER TABLE [Attendees] ADD [FirstName] nvarchar(max) NULL;

GO

ALTER TABLE [Attendees] ADD [Gender] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Attendees] ADD [LastName] nvarchar(max) NULL;

GO

ALTER TABLE [Attendees] ADD [MiddleName] nvarchar(max) NULL;

GO

ALTER TABLE [Attendees] ADD [MobileNumber] nvarchar(max) NULL;

GO

ALTER TABLE [Attendees] ADD [Nationality] nvarchar(max) NULL;

GO

ALTER TABLE [Attendees] ADD [Occupation] nvarchar(max) NULL;

GO

ALTER TABLE [Attendees] ADD [PhoneNumber] nvarchar(max) NULL;

GO

ALTER TABLE [Attendees] ADD [ResidentialAddress] nvarchar(max) NULL;

GO

ALTER TABLE [Attendees] ADD [Title] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190305151718_Pikup', N'2.1.4-rtm-31024');

GO

CREATE TABLE [BillLogs] (
    [Id] int NOT NULL IDENTITY,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [LastUpdateDate] datetime2 NULL,
    [RowVersion] varbinary(max) NULL,
    [UserId] int NOT NULL,
    [Amount] decimal(18, 2) NOT NULL,
    [BillStatus] int NOT NULL,
    [BillRefenceNo] nvarchar(max) NULL,
    [DatePaid] datetime2 NULL,
    [BillType] int NOT NULL,
    [Year] int NOT NULL,
    [PaymentMethod] int NOT NULL,
    [EntityId] int NULL,
    CONSTRAINT [PK_BillLogs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BillLogs_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [PaymentLogs] (
    [Id] int NOT NULL IDENTITY,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [LastUpdateDate] datetime2 NULL,
    [RowVersion] varbinary(max) NULL,
    [MembershipNo] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    [PaymenType] int NOT NULL,
    [BillId] int NOT NULL,
    [PaymentReferenceNo] nvarchar(max) NULL,
    [Amount] decimal(18, 2) NOT NULL,
    [FinalAmount] decimal(18, 2) NOT NULL,
    [PaymentDate] datetime2 NULL,
    [PaymentCharges] decimal(18, 2) NOT NULL,
    [ResponseCode] nvarchar(max) NULL,
    [ResponseDescription] nvarchar(max) NULL,
    [GateWayAmount] decimal(18, 2) NOT NULL,
    [TechFee] decimal(18, 2) NOT NULL,
    [PaymentStatus] int NOT NULL,
    [ResponsePayload] nvarchar(max) NULL,
    [CardNumber] nvarchar(max) NULL,
    [EntityId] int NULL,
    [BillLogId] int NULL,
    CONSTRAINT [PK_PaymentLogs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PaymentLogs_BillLogs_BillLogId] FOREIGN KEY ([BillLogId]) REFERENCES [BillLogs] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PaymentLogs_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_BillLogs_UserId] ON [BillLogs] ([UserId]);

GO

CREATE INDEX [IX_PaymentLogs_BillLogId] ON [PaymentLogs] ([BillLogId]);

GO

CREATE INDEX [IX_PaymentLogs_UserId] ON [PaymentLogs] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190306085822_Paymnt', N'2.1.4-rtm-31024');

GO

ALTER TABLE [EmailTemplates] ADD [EventId] int NULL;

GO

ALTER TABLE [Attendees] ADD [EmailAddress] nvarchar(max) NULL;

GO

ALTER TABLE [Attendees] ADD [IsPaid] bit NOT NULL DEFAULT 0;

GO

ALTER TABLE [Attendees] ADD [RegistrationBillId] int NOT NULL DEFAULT 0;

GO

CREATE INDEX [IX_EmailTemplates_EventId] ON [EmailTemplates] ([EventId]);

GO

CREATE INDEX [IX_Attendees_RegistrationBillId] ON [Attendees] ([RegistrationBillId]);

GO

ALTER TABLE [Attendees] ADD CONSTRAINT [FK_Attendees_BillLogs_RegistrationBillId] FOREIGN KEY ([RegistrationBillId]) REFERENCES [BillLogs] ([Id]);

GO

ALTER TABLE [EmailTemplates] ADD CONSTRAINT [FK_EmailTemplates_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190306152318_EventReg', N'2.1.4-rtm-31024');

GO

ALTER TABLE [Attendees] DROP CONSTRAINT [FK_Attendees_BillLogs_RegistrationBillId];

GO

ALTER TABLE [Events] ADD [RegistrationFee] decimal(18, 2) NOT NULL DEFAULT 0.0;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Attendees]') AND [c].[name] = N'RegistrationBillId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Attendees] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Attendees] ALTER COLUMN [RegistrationBillId] int NULL;

GO

ALTER TABLE [Attendees] ADD CONSTRAINT [FK_Attendees_BillLogs_RegistrationBillId] FOREIGN KEY ([RegistrationBillId]) REFERENCES [BillLogs] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190307101018_AttendeeRegII', N'2.1.4-rtm-31024');

GO

ALTER TABLE [EmailLog] ADD [AttachmentLoc] nvarchar(max) NULL;

GO

ALTER TABLE [EmailLog] ADD [HasAttachements] bit NOT NULL DEFAULT 0;

GO

ALTER TABLE [EmailLog] ADD [SendImmediately] bit NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190308152613_EmailCampagin', N'2.1.4-rtm-31024');

GO

