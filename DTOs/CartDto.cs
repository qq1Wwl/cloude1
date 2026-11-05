using pr9ASP.Models;

namespace pr9ASP.DTOs
{
    public class CartDto
    {
        public int Cart_Id { get; set; }
        public int User_Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public static CartDto ToDto(Cart cart) => new CartDto
        {
            Cart_Id = cart.Cart_Id,
            User_Id = cart.User_Id,
            CreatedAt = cart.CreatedAt
        };
    }
}