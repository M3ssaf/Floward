CREATE TABLE [dbo].[Products] (
    [Id]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (128)  NOT NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [Cost]        DECIMAL (18, 2) NOT NULL,
    [Base64Image] VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC)
);

