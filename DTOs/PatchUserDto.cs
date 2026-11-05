namespace pr9ASP.DTOs
{
    public class PatchUserDto
    {
        public string? Login { get; set; }
        public string? PasswordHash { get; set; }
        public int? Role_Id { get; set; }
    }
}