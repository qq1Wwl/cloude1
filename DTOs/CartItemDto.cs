using pr9ASP.Models;

namespace pr9ASP.DTOs
{
    public class CartItemDto
    {
        public int CartItem_Id { get; set; }
        public int Cart_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }

        public static CartItemDto ToDto(CartItem item) => new CartItemDto
        {
            CartItem_Id = item.CartItem_Id,
            Cart_Id = item.Cart_Id,
            Product_Id = item.Product_Id,
            Quantity = item.Quantity,
            CreatedAt = item.CreatedAt
        };
    }
}