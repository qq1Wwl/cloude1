using pr9ASP.Models;

namespace pr9ASP.DTOs
{
    public class OrderItemDto
    {
        public int OrderItem_Id { get; set; }
        public int Order_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }

        public static OrderItemDto ToDto(OrderItem item) => new OrderItemDto
        {
            OrderItem_Id = item.OrderItem_Id,
            Order_Id = item.Order_Id,
            Product_Id = item.Product_Id,
            Quantity = item.Quantity,
            Price = item.Price,
            CreatedAt = item.CreatedAt
        };
    }
}