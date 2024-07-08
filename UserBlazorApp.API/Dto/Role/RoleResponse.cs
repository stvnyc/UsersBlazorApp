using UserBlazorApp.API.DTO.RoleClaims;

namespace UserBlazorApp.API.DTO.Role;

public class RoleResponse
{
    public required int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<RoleClaimResponse> RoleClaims { get; set; } = new List<RoleClaimResponse>();
}