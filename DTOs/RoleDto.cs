using pr9ASP.Models;

namespace pr9ASP.DTOs
{
    public class RoleDto
    {
        public int Role_Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public static RoleDto ToDto(Role role) => new RoleDto
        {
            Role_Id = role.Role_Id,
            RoleName = role.RoleName,
            CreatedAt = role.CreatedAt
        };
    }
}