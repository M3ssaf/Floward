using FlowardDB.DTOs;
using FlowardDB.RepoContract;
using Microsoft.AspNetCore.Identity;

namespace FlowardDB.Repo
{
    public class UsersRepo : IUsersRepo
    {
        #region Declarations
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        #endregion

        #region Constructor
        public UsersRepo(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #endregion

        #region Implementation
        public async Task<short> RegisterUser(ApplicationUser user, string password, long roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null) { return 1; }
                var result =await _userManager.CreateAsync(user, password);
                if (!result.Succeeded) {
                    if (result.Errors.FirstOrDefault().Code.Contains("DuplicateUserName")) {
                        return 3;
                    }
                    return 2;
                }
                var userRole = await _userManager.AddToRoleAsync(user, role.Name);
                if (!userRole.Succeeded) {
                    _userManager.DeleteAsync(user);
                    return 2;
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        } 
        #endregion
    }
}
