using System.Security.Claims;

namespace StartcodeAuthorization.Features.UserDebug;

public class UserInfoModel
{
    public bool? IsAuthenticated { get; set; }

    public string? AuthenticationType { get; set; }

    public List<Claim>? Claims { get; set; }

    public string? Name { get; set; }

    public bool IsDeveloper { get; set; }

    public bool IsAdmin { get; set; }

    public string? DefaultNameClaimType { get; set; }

    public string? DefaultRoleClaimType { get; set; }
}