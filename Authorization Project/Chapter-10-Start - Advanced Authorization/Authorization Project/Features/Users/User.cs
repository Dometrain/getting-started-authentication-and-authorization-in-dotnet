using System.Security.Claims;

namespace StartcodeAuthorization.Features.Users;

public class User
{
    public int UserID { get; set; }
    public string? Website { get; set; }
    public string? Country { get; set; }
    public List<Claim>? Claims { get; set; }

    public string? GetNameClaim()
    {
        return Claims?.FirstOrDefault(c => c.Type == "name")?.Value;
    }
}