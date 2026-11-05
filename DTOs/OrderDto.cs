using pr9ASP.Models;

namespace pr9ASP.DTOs
{
    public class OrderDto
    {
        public int Order_Id { get; set; }
        public int User_Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public static OrderDto ToDto(Order order) => new OrderDto
        {
            Order_Id = order.Order_Id,
            User_Id = order.User_Id,
            OrderDate = order.OrderDate,
            CreatedAt = order.CreatedAt
        };
    }
}