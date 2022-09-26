using FlowardDB.DTOs;
using FlowardDB.RepoContract;
using Microsoft.EntityFrameworkCore;

namespace FlowardDB.Repo
{
    public class RolesRepo : IRolesRepo
    {
        #region Declarations
        private readonly FlowardDbContext _dbContext;
        private readonly string sp_ValidateUserPermission = "sp_ValidateUserPermission";
        #endregion

        #region Constructor
        public RolesRepo(FlowardDbContext db)
        {
            _dbContext = db;
        }
        #endregion

        #region Implementation
        public async Task<bool> ValidatePermission(long userId, string TargetPath){
            try{
                var PermissionDetails = await _dbContext.UserPermissions.FromSqlInterpolated($"{sp_ValidateUserPermission} {userId}, {TargetPath}").ToListAsync();
                if (PermissionDetails.Count() <= 0){
                    return false;
                }
                return true;
            }
            catch (Exception){
                throw;
            }
        } 
        #endregion
    }
}
