namespace pr9ASP.Models
{
    public class Category
    {
        public int Category_Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}