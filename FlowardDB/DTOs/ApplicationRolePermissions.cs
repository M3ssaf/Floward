using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowardDB.DTOs
{
    public class ApplicationRolePermissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ApplicationRoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
        public long PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
