using System.ComponentModel.DataAnnotations;

namespace Common.Models.Requests
{
    public class EditProductRequest:NewProductRequest{
        [Required]
        public long Id { get; set; }
    }
}
