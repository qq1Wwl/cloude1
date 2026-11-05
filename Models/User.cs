namespace pr9ASP.Models
{
    public class User
    {
        public int User_Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int Role_Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}   