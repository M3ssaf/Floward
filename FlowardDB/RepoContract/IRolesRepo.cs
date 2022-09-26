namespace FlowardDB.RepoContract
{
    public interface IRolesRepo
    {
        Task<bool> ValidatePermission(long userId, string TargetPath);
    }
}
