using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowardBusiness.Models
{
    public class UserCreationRequest
    {
        [Required, MaxLength(250)]
        [EmailAddress]
        public string? emailAddress { get; set; }
        public string? phoneNumber { get; set; }
        [Required, MaxLength(50)]
        public string? FirstName { get; set; }
        [Required, MaxLength(50)]
        public string? LastName { get; set; }
        [Required]
        public long RoleId { get; set; }
        [Required]
        public long CreatedBy { get; set; }
        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required, DataType(DataType.Password)]
        public string? confirmedPassword { get; set; }
    }
}
