using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthentication.Features.User;

public class UserController : Controller
{
    [HttpGet]
    public IActionResult Login(string ReturnUrl)
    {
        return View(new LoginModel() { ReturnUrl = ReturnUrl });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginCredentials)
    {
        return Redirect(loginCredentials.ReturnUrl ?? "/");
    }


    [HttpPost]
    public async Task Logout()
    {
    }


    public IActionResult LoggedOut()
    {
        return View();
    }


    public IActionResult AccessDenied()
    {
        return View();
    }


    public IActionResult Info()
    {
        // 1. Get the user (ClaimsPrincipal) from the HttpContext

        // 2. Get the Primary Identity of the user (there might be more than one)

        // 3. Get IsAuthenticated

        // 4. Get AuthenticationType

        // 5. Get the claims from the user

        // 6. Get the Name claim

        // 7. Check if user has developer or admin role


        var model = new UserInfoModel()
        {
        };

        return View(model);
    }
}

