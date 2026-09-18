using pr9ASP.Models;

namespace pr9ASP.DTOs
{
    public class ProductDto
    {
        public int Product_Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Category_Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public static ProductDto ToDto(Product product)
        {
            return new ProductDto
            {
                Product_Id = product.Product_Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Category_Id = product.Category_Id,
                CreatedAt = product.CreatedAt
            };
        }
    }
}