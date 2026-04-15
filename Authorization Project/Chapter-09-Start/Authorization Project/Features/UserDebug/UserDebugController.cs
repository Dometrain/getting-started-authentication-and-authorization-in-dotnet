using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace StartcodeAuthorization.Features.UserDebug;

/// <summary>
/// Provides debugging information about the currently authenticated user, including claims, roles, and authentication
/// details.
/// </summary>
/// <remarks>This controller is intended for debugging purposes and retrieves information about the current user
/// from the HTTP context. It gathers details such as authentication status, claims, roles, and identity properties, and
/// passes them to the view.
/// </remarks>
public class UserDebugController : Controller
{
    public IActionResult UserInfo()
    {
        // 1. Get the user (ClaimsPrincipal) from the HttpContext
        ClaimsPrincipal user = User;

        // 2. Get the Primary Identity of the user (there might be more than one)
        var identity = User.Identity as ClaimsIdentity;

        // 3. Get IsAuthenticated
        var isAuthenticated = identity?.IsAuthenticated;

        // 4. Get AuthenticationType
        var authenticationType = identity?.AuthenticationType;

        // 5. Get the claims from the user

        List<Claim> claims = user?.Claims?.ToList() ?? [];

        // 6. Get the Name claim
        var name = identity?.Name;

        // 7. Check if user is a developer and admin
        var isDeveloper = user?.IsInRole("developer") ?? false;
        var isAdmin = user?.IsInRole("admin") ?? false;

        var model = new UserInfoModel()
        {
            IsAuthenticated = isAuthenticated,
            AuthenticationType = authenticationType,
            Claims = claims,
            Name = name,
            IsDeveloper = isDeveloper,
            IsAdmin = isAdmin,
            DefaultNameClaimType = identity?.NameClaimType,
            DefaultRoleClaimType = identity?.RoleClaimType
        };

        return View(model);
    }
}

