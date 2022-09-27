/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE [dbFloward]
GO
SET IDENTITY_INSERT [dbo].[AspNetRoles] ON 
GO
INSERT [dbo].[AspNetRoles] ([Id], [isActive], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (1, 1, N'Admin', N'ADMIN', N'91E7A1E9-94C4-4971-8F47-8C65EE4ADEA2')
GO
SET IDENTITY_INSERT [dbo].[AspNetRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[AspNetUsers] ON 
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [isActive], [CreatedBy], [CreatedAt], [UpdateBy], [UpdatedAt], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (1, N'Mahmoud', N'Assaf', 1, 0, CAST(N'2022-09-27T00:35:49.7621683' AS DateTime2), NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'mahmoud.a.assaf@gmail.com', N'MAHMOUD.A.ASSAF@GMAIL.COM', N'mahmoud.a.assaf@gmail.com', N'MAHMOUD.A.ASSAF@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEG0SpkquWL4LxV+lAJBrVY81OGPoepNIW0WlFxWLWggGZNtAoYeMfpa11swwI/qPOA==', N'LPLJOS75A6L3RKEZE3S524XS4WMYKTIX', N'e23a03c8-282f-4ccf-bce7-20bf44fa6756', N'0508826813', 1, 0, NULL, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[AspNetUsers] OFF
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (1, 1)
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON 
GO
INSERT [dbo].[Permissions] ([Id], [Name], [PathUrl]) VALUES (1, N'Create Product', N'api/Catalog/addProduct')
GO
INSERT [dbo].[Permissions] ([Id], [Name], [PathUrl]) VALUES (2, N'Get Product', N'api/Catalog/getProduct')
GO
INSERT [dbo].[Permissions] ([Id], [Name], [PathUrl]) VALUES (3, N'Get All Products', N'api/Catalog/getAllProducts')
GO
INSERT [dbo].[Permissions] ([Id], [Name], [PathUrl]) VALUES (4, N'Delete Product', N'api/Catalog/deleteProduct')
GO
INSERT [dbo].[Permissions] ([Id], [Name], [PathUrl]) VALUES (5, N'Update Product', N'api/Catalog/editProduct')
GO
SET IDENTITY_INSERT [dbo].[Permissions] OFF
GO
SET IDENTITY_INSERT [dbo].[ApplicationRolePermissions] ON 
GO
INSERT [dbo].[ApplicationRolePermissions] ([Id], [ApplicationRoleId], [PermissionId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[ApplicationRolePermissions] ([Id], [ApplicationRoleId], [PermissionId]) VALUES (2, 1, 2)
GO
INSERT [dbo].[ApplicationRolePermissions] ([Id], [ApplicationRoleId], [PermissionId]) VALUES (3, 1, 3)
GO
INSERT [dbo].[ApplicationRolePermissions] ([Id], [ApplicationRoleId], [PermissionId]) VALUES (5, 1, 5)
GO
INSERT [dbo].[ApplicationRolePermissions] ([Id], [ApplicationRoleId], [PermissionId]) VALUES (6, 1, 4)
GO
SET IDENTITY_INSERT [dbo].[ApplicationRolePermissions] OFF
GO
SET IDENTITY_INSERT [dbo].[UserSessions] ON 
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (1, N'6ccce0ed-34bc-4e25-8204-d52717200a69', 1, CAST(N'2022-09-27T00:37:17.3028623' AS DateTime2), 0, CAST(N'2022-09-27T00:50:01.4539738' AS DateTime2))
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (2, N'621adb6a-165c-4685-9c14-5c6c206b70db', 1, CAST(N'2022-09-27T00:38:50.5975936' AS DateTime2), 0, CAST(N'2022-09-27T00:50:01.4540200' AS DateTime2))
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (3, N'd6998c74-4073-4987-950f-760ce8330f6f', 1, CAST(N'2022-09-27T00:41:45.1548585' AS DateTime2), 0, CAST(N'2022-09-27T00:50:01.4540202' AS DateTime2))
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (4, N'b1f249eb-4413-4f56-b2a6-891e522ce973', 1, CAST(N'2022-09-27T00:42:57.0025725' AS DateTime2), 0, CAST(N'2022-09-27T00:50:01.4540203' AS DateTime2))
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (5, N'2c286954-3a8e-47b4-861b-3ac4569ddab9', 1, CAST(N'2022-09-27T00:47:55.9493993' AS DateTime2), 0, CAST(N'2022-09-27T00:50:01.4540203' AS DateTime2))
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (6, N'7da1a36f-55ee-4d31-8f72-72c733bc3ae3', 1, CAST(N'2022-09-27T00:48:16.6479047' AS DateTime2), 0, CAST(N'2022-09-27T00:50:01.4540204' AS DateTime2))
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (7, N'c8c470d4-bec5-4980-8a36-0687d8ea03ef', 1, CAST(N'2022-09-27T00:50:01.4073097' AS DateTime2), 0, CAST(N'2022-09-27T01:08:06.5891250' AS DateTime2))
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (8, N'77dde030-c6b4-41e3-9ae8-eec271894c39', 1, CAST(N'2022-09-27T01:08:06.5365724' AS DateTime2), 0, CAST(N'2022-09-27T01:11:30.2974698' AS DateTime2))
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (9, N'b09ee278-7a01-44cf-ae7b-0ac8631247f2', 1, CAST(N'2022-09-27T01:11:30.2512013' AS DateTime2), 0, CAST(N'2022-09-27T01:14:39.1423287' AS DateTime2))
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (10, N'89f3ecc9-96e6-4f7e-83a4-53629926ad2c', 1, CAST(N'2022-09-27T01:14:39.0919483' AS DateTime2), 0, CAST(N'2022-09-27T01:22:37.3123644' AS DateTime2))
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (11, N'8c009d7e-268f-483d-b100-64407c02f009', 1, CAST(N'2022-09-27T01:22:37.2536273' AS DateTime2), 0, CAST(N'2022-09-27T01:26:02.7369376' AS DateTime2))
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (12, N'1189cf1a-b013-4d1a-8b98-5d5d325efddf', 1, CAST(N'2022-09-27T01:26:02.6827589' AS DateTime2), 0, CAST(N'2022-09-27T01:29:24.7937067' AS DateTime2))
GO
INSERT [dbo].[UserSessions] ([Id], [Jti], [ApplicationUserId], [LoginTimeStamp], [isActive], [TerminationTimestamp]) VALUES (13, N'b77bf2cd-03ad-4a64-b850-085f52d496ce', 1, CAST(N'2022-09-27T01:29:24.7429057' AS DateTime2), 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[UserSessions] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220925173210_initialCreation', N'6.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220925175725_initialCreation2', N'6.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220925175959_initialCreation3', N'6.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220925183302_initialCreation4', N'6.0.9')
GO
