namespace Common.Models.Responses
{
    public class ProductResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string Base64Image { get; set; }
    }
}
