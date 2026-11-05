namespace pr9ASP.DTOs
{
    public class UpdateUserDto
    {
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int Role_Id { get; set; }
    }
}