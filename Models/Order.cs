namespace pr9ASP.Models
{
    public class Order
    {
        public int Order_Id { get; set; }
        public int User_Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}   