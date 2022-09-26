using FlowardDB.DTOs;
using FlowardDB.RepoContract;
using Microsoft.EntityFrameworkCore;

namespace FlowardDB.Repo
{
    public class SessionRepo : ISessionRepo
    {
        #region Declarations
        private readonly FlowardDbContext _dbContext;
        #endregion

        #region Constructor
        public SessionRepo(FlowardDbContext db)
        {
            _dbContext = db;
        }
        #endregion

        #region Implementation
        public async Task<bool> CreateUserSession(UserSession session)
        {
            try
            {
                var OldSessions = _dbContext.UserSessions.Where(s => s.ApplicationUserId == session.ApplicationUser.Id && s.isActive).ToList();
                OldSessions.ForEach(s =>
                {
                    s.isActive = false;
                    s.TerminationTimestamp = DateTime.Now;
                });
                _dbContext.UserSessions.Add(session);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserSession> GetCurrentUserSession(long userId)
        {
            try
            {
                return await _dbContext.UserSessions.Where(s => s.ApplicationUserId == userId && s.isActive).OrderByDescending(s => s.Id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TerminateSession(long userId)
        {
            try
            {
                var activeSessions = _dbContext.UserSessions.Where(s => s.ApplicationUserId == userId && s.isActive).ToList();
                if (activeSessions == null) { return false; }
                activeSessions.ForEach(a =>
                {
                    a.isActive = false;
                    a.TerminationTimestamp = DateTime.Now;
                });
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}