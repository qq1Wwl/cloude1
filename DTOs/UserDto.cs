using pr9ASP.Models;

namespace pr9ASP.DTOs
{
    public class UserDto
    {
        public int User_Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public int Role_Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public static UserDto ToDto(User user) => new UserDto
        {
            User_Id = user.User_Id,
            Login = user.Login,
            Role_Id = user.Role_Id,
            CreatedAt = user.CreatedAt
        };
    }
}