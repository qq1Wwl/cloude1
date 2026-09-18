namespace pr9ASP.DTOs
{
    public class CreateProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Category_Id { get; set; }
    }
}