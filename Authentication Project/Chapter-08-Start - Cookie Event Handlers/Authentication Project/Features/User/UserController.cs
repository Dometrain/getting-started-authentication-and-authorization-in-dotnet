using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace StartcodeAuthentication.Features.User;

public class UserController : Controller
{
    private readonly ILogger<UserController> logger;

    public UserController(ILogger<UserController> logger)
    {
        this.logger = logger;
    }


    [HttpGet]
    public IActionResult Login(string ReturnUrl)
    {
        return View(new LoginModel() { ReturnUrl = ReturnUrl });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginCredentials, string returnUrl = null)
    {
        var userName = loginCredentials.UserName;

        //Validation


        //1. Load the claims for this user 
        var myClaims = new List<Claim>()
               {
                   new("sub","1234"),
                   new("name",userName),
                   new("email","bob@tn-data.se"),
                   new("role","developer"),
                   new("role","sales"),
                   new("role","admin")
               };

        var myIdentity = new ClaimsIdentity(claims: myClaims,
                                            authenticationType: "custom",
                                            nameType: "name",
                                            roleType: "role");

        var myPrincipal = new ClaimsPrincipal(myIdentity);

        var items = new Dictionary<string, string>()
        {
            { "Item1", "Value1" },
            { "Item2", "Value2" },
            { "Item3", "Value3" },
        };

        var parameters = new Dictionary<string, object>()
        {
            { "Param1", "Value1" },
            { "Param2", "Value2" },
            { "Param3", "Value3" },
        };

        var properties = new AuthenticationProperties(items, parameters)
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(15),
            AllowRefresh = true,
        };

        await HttpContext.SignInAsync(myPrincipal, properties);


        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            return LocalRedirect("/");
        }
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("cookie");

        return LocalRedirect("/user/LoggedOut");
    }


    public IActionResult LoggedOut()
    {
        return View();
    }


    public IActionResult AccessDenied(string returnUrl = null)
    {
        // Log it
        logger.LogWarning("Access denied for user {User} attempting to access {Resource}",
        User?.Identity?.Name, returnUrl);

        ViewData["returnUrl"] = returnUrl;
        return View();
    }


    public IActionResult Info()
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

        // 7. Check if user has developer or admin role
        var isDeveloper = user.IsInRole("developer");
        var isAdmin = user.IsInRole("admin");


        var model = new UserInfoModel()
        {
            IsAuthenticated = isAuthenticated,
            AuthenticationType = authenticationType,
            Claims = claims,
            Name = name,
            IsDeveloper = isDeveloper,
            IsAdmin = isAdmin,
            DefaultNameClaimType = identity.NameClaimType,
            DefaultRoleClaimType = identity.RoleClaimType
        };

        return View(model);
    }
}

