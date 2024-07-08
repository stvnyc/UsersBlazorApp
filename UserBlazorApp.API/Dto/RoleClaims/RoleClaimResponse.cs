namespace UserBlazorApp.API.DTO.RoleClaims;

public class RoleClaimResponse
{
    public required int Id { get; set; }
    public required int RoleId { get; set; }
    public string? ClaimType { get; set; }
    public string? ClaimValue { get; set; }
}