CREATE PROCEDURE sp_ValidateUserPermission
@UserId BIGINT,
@TargetPath NVARCHAR(250)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT UserRole.UserId, P.PathUrl
	FROM   ApplicationRolePermissions Permission INNER JOIN
	       AspNetUserRoles UserRole ON Permission.ApplicationRoleId=UserRole.RoleId INNER JOIN
		   [Permissions] P ON Permission.PermissionId=P.Id
	WHERE  UserRole.UserId=@UserId AND LOWER(P.PathUrl)=LOWER(@TargetPath)
	SET NOCOUNT ON;
END