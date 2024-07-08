namespace UserBlazorApp.API.DTO.UserClaims;

public class UserClaimRequest
{
    public required string ClaimType { get; set; }
    public required string ClaimValue { get; set; }
}