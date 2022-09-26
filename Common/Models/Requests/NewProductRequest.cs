using System.ComponentModel.DataAnnotations;

namespace Common.Models.Requests
{
    public class NewProductRequest
    {
        [Required, MaxLength(128)]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public string Base64Image { get; set; }
    }
}
