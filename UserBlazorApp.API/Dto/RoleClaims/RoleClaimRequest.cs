namespace UserBlazorApp.API.DTO.RoleClaims;

public class RoleClaimRequest
{
    public required string ClaimType { get; set; }
    public required string ClaimValue { get; set; }
}