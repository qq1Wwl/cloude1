namespace pr9ASP.Models
{
    public class CartItem
    {
        public int CartItem_Id { get; set; }
        public int Cart_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}