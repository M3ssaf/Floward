using FlowardDB.DTOs;

namespace FlowardDB.RepoContract
{
    public interface ISessionRepo
    {
        Task<bool> CreateUserSession(DTOs.UserSession session);
        Task<UserSession> GetCurrentUserSession(long userId);
        Task<bool> TerminateSession(long userId);
    }
}
