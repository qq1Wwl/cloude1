namespace pr9ASP.DTOs
{
    public class UpdateOrderItemDto
    {
        public int Order_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}