using UserBlazorApp.API.DTO.Role;
using UserBlazorApp.API.DTO.RoleClaims;

namespace UserBlazorApp.API.DTO.User;

public class UserRequest
{
    public required string UserName { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string PhoneNumber { get; set; }
    public required DateTimeOffset LockoutEnd { get; set; }
    public ICollection<RoleRequest> Role { get; set; } = new List<RoleRequest>();
}