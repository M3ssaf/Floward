CREATE TABLE [dbo].[ApplicationRolePermission] (
    [ApplicationRolesId] BIGINT NOT NULL,
    [PermissionsId]      BIGINT NOT NULL,
    CONSTRAINT [PK_ApplicationRolePermission] PRIMARY KEY CLUSTERED ([ApplicationRolesId] ASC, [PermissionsId] ASC),
    CONSTRAINT [FK_ApplicationRolePermission_AspNetRoles_ApplicationRolesId] FOREIGN KEY ([ApplicationRolesId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ApplicationRolePermission_Permissions_PermissionsId] FOREIGN KEY ([PermissionsId]) REFERENCES [dbo].[Permissions] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationRolePermission_PermissionsId]
    ON [dbo].[ApplicationRolePermission]([PermissionsId] ASC);

