CREATE TABLE [dbo].[Activities] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50)  NOT NULL,
    [UserID]            NVARCHAR (128) NOT NULL,
    [Description]       VARCHAR (MAX)  NULL,
    [Type]              INT            NOT NULL,
    [Verified]          BIT            DEFAULT ((0)) NOT NULL,
    [RecognitionReason] INT            NULL,
    [Created]           DATETIME        NULL,
    [Updated]           DATETIME        NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    FOREIGN KEY ([Type]) REFERENCES [dbo].[ActivityTypes] ([Id]),
    FOREIGN KEY ([RecognitionReason]) REFERENCES [dbo].[RecognitionReasons] ([Id])
);

