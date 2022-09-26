using Microsoft.AspNetCore.Identity;

namespace FlowardDB.DTOs
{
    public class ApplicationRole:IdentityRole<long>
    {
        public bool isActive { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
