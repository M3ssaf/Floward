namespace Common.Models
{
    public class mqProduct
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public byte[] Base64Image { get; set; }
    }
}
