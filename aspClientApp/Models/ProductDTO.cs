using System.Text.Json.Serialization;

namespace aspClientApp.Models
{
    public class ProductDTO
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [JsonPropertyName("productName")]
        public string ProductName { get; set; } = null!;
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}