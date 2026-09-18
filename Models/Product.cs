namespace pr9ASP.Models
{
    public class Product
    {
        public int Product_Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Category_Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}