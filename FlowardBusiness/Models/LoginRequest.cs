using System.ComponentModel.DataAnnotations;

namespace FlowardBusiness.Models
{
    public class LoginRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
