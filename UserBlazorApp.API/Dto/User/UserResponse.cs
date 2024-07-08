using UserBlazorApp.API.DTO.Role;
using UserBlazorApp.API.DTO.RoleClaims;

namespace UserBlazorApp.API.DTO.User;

public class UserResponse
{
    public required int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public ICollection<RoleResponse> Role { get; set; } = new List<RoleResponse>();
}