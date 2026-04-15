using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace StartcodeAuthorization.Features.Users;

public class UsersController : Controller
{
    private readonly IUserDatabase userDatabase;

    public UsersController(IUserDatabase userDatabase)
    {
        this.userDatabase = userDatabase;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(int id)
    {
        var userProfile = userDatabase.GetUserProfileById(id);

        if (userProfile != null)
        {
            var identity = new ClaimsIdentity(
                            claims: userProfile.Claims,
                            authenticationType: "TestAuth",
                            nameType: "name");

            var principal = new ClaimsPrincipal(identity);


            var properties = new AuthenticationProperties()
            {
                RedirectUri = "/"
            };

            await HttpContext.SignInAsync(principal, properties);
        }

        return Empty;
    }

    [HttpPost]
    public async Task Logout()
    {

        var properties = new AuthenticationProperties()
        {
            RedirectUri = "/Users/LoggedOut"
        };

        await HttpContext.SignOutAsync(properties);
    }

    public IActionResult LoggedOut()
    {
        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
