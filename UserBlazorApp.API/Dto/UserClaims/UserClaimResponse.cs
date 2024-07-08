namespace UserBlazorApp.API.DTO.UserClaims;

public class UserClaimResponse
{
    public required int Id {  get; set; }
    public string? ClaimType { get; set; }
    public string? ClaimValue { get; set; }
}