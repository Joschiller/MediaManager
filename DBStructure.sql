CREATE TABLE [dbo].[Catalogues] (
    [Id]                           INT             IDENTITY (1, 1) NOT NULL,
    [Title]                        NVARCHAR (128)  NOT NULL,
    [Description]                  NVARCHAR (2048) NOT NULL,
    [DeletionConfirmationMedium]   BIT             DEFAULT ((1)) NOT NULL,
    [DeletionConfirmationPart]     BIT             DEFAULT ((1)) NOT NULL,
    [DeletionConfirmationTag]      BIT             DEFAULT ((1)) NOT NULL,
    [DeletionConfirmationPlaylist] BIT             DEFAULT ((1)) NOT NULL,
    [ShowTitleOfTheDayAsMedium]    BIT             DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Media] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CatalogueId] INT            NOT NULL,
    [Title]       NVARCHAR (128) NOT NULL,
    [Description] NVARCHAR (512) NOT NULL,
    [Location]    NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Media_ToCatalogues] FOREIGN KEY ([CatalogueId]) REFERENCES [dbo].[Catalogues] ([Id])
);

CREATE TABLE [dbo].[Parts] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [MediumId]         INT            NOT NULL,
    [Title]            NVARCHAR (128) NOT NULL,
    [Description]      NVARCHAR (512) NOT NULL,
    [Favourite]        BIT            NOT NULL,
    [Length]           INT            NOT NULL,
    [Publication Year] INT            NOT NULL,
    [Image]            IMAGE          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Parts_ToMedia] FOREIGN KEY ([MediumId]) REFERENCES [dbo].[Media] ([Id])
);

CREATE TABLE [dbo].[Tags] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CatalogueId] INT            NOT NULL,
    [Title]       NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tags_ToCatalogues] FOREIGN KEY ([CatalogueId]) REFERENCES [dbo].[Catalogues] ([Id])
);

CREATE TABLE [dbo].[MT_Relation] (
    [MediaId] INT NOT NULL,
    [TagId]   INT NOT NULL,
    [Value]   BIT NOT NULL,
    PRIMARY KEY CLUSTERED ([MediaId] ASC, [TagId] ASC),
    CONSTRAINT [FK_MT_Relation_ToMedia] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([Id]),
    CONSTRAINT [FK_MT_Relation_ToTags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id])
);

CREATE TABLE [dbo].[PT_Relation] (
    [PartId] INT NOT NULL,
    [TagId]  INT NOT NULL,
    [Value]  BIT NOT NULL,
    PRIMARY KEY CLUSTERED ([PartId] ASC, [TagId] ASC),
    CONSTRAINT [FK_PT_Relation_ToParts] FOREIGN KEY ([PartId]) REFERENCES [dbo].[Parts] ([Id]),
    CONSTRAINT [FK_PT_Relation_ToTags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id])
);

CREATE TABLE [dbo].[Settings] (
    [Key]   NVARCHAR (512) NOT NULL,
    [Value] NVARCHAR (512) NOT NULL,
    PRIMARY KEY CLUSTERED ([Key] ASC)
);

INSERT INTO [dbo].[Settings] VALUES ('SettingsVersion', '1.0.0');
