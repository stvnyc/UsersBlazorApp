using UserBlazorApp.API.DTO.RoleClaims;

namespace UserBlazorApp.API.DTO.Role;

public class RoleRequest
{
    public required string Name { get; set; }
    public ICollection<RoleClaimRequest> roleClaimRequests { get; set; } = new List<RoleClaimRequest>();
}