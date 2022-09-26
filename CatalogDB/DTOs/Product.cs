using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogDB.DTOs
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required, MaxLength(128)]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public byte[] Base64Image { get; set; }
    }
}
