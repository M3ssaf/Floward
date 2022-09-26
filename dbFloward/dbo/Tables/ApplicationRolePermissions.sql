CREATE TABLE [dbo].[ApplicationRolePermissions] (
    [Id]                BIGINT IDENTITY (1, 1) NOT NULL,
    [ApplicationRoleId] BIGINT NOT NULL,
    [PermissionId]      BIGINT NOT NULL,
    CONSTRAINT [PK_ApplicationRolePermissions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationRolePermissions_AspNetRoles_ApplicationRoleId] FOREIGN KEY ([ApplicationRoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ApplicationRolePermissions_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permissions] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationRolePermissions_PermissionId]
    ON [dbo].[ApplicationRolePermissions]([PermissionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationRolePermissions_ApplicationRoleId]
    ON [dbo].[ApplicationRolePermissions]([ApplicationRoleId] ASC);

