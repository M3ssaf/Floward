using FlowardDB.DTOs;

namespace FlowardDB.RepoContract
{
    public interface IUsersRepo
    {
        Task<short> RegisterUser(ApplicationUser user, string password, long roleId);
    }
}
