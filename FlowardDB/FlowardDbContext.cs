using FlowardDB.DTOs;
using FlowardDB.NotMapped;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlowardDB
{
    public class FlowardDbContext:IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {
        #region Constructor
        public FlowardDbContext(DbContextOptions<FlowardDbContext> options)
            : base(options)
        {
        }
        //public FlowardDbContext()
        //{

        //}
        #endregion

        #region Datasets
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<ApplicationRolePermissions> ApplicationRolePermissions { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }
        #endregion

        #region Overrides
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=LAPTOP-MQA4LEOG\\MSSQLSERVER01;Database=dbFloward;user id=sa;password=m_01004392068;Trusted_Connection=True;");
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().Property(u => u.FullName).HasComputedColumnSql("TRIM([FirstName])+' '+TRIM([LastName])");
            builder.Entity<UserPermissions>(entity => entity.HasNoKey());
            base.OnModelCreating(builder);
        }
        #endregion
    }
}
