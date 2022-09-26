using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowardDB.DTOs
{
    public class UserSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Jti { get; set; }
        public long ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LoginTimeStamp { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool isActive { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? TerminationTimestamp { get; set; }
    }
}
