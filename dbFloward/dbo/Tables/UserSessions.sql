CREATE TABLE [dbo].[UserSessions] (
    [Id]                   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Jti]                  NVARCHAR (255) NOT NULL,
    [ApplicationUserId]    BIGINT         NOT NULL,
    [LoginTimeStamp]       DATETIME2 (7)  NOT NULL,
    [isActive]             BIT            NOT NULL,
    [TerminationTimestamp] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_UserSessions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserSessions_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserSessions_ApplicationUserId]
    ON [dbo].[UserSessions]([ApplicationUserId] ASC);

