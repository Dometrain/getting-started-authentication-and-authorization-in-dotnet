using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthentication.Features.AuthTest;


/// <summary>
/// Authentication test controller for demonstrating authentication operations
/// </summary>
public class AuthTestController : Controller
{
    /// <summary>
    /// Display the test page with authentication status
    /// </summary>
    public IActionResult Index()
    {
        ViewBag.IsAuthenticated = User.Identity?.IsAuthenticated ?? false;
        ViewBag.Username = User.Identity?.Name ?? "Not authenticated";

        return View();
    }

    /// <summary>
    /// Trigger authentication check
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AuthenticateUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.AuthenticateAsync()");

        // TODO: 

        // TempData["Message"] = $"Authentication result: {(result.Succeeded ? "Success" : "Failed")}";

        return RedirectToAction("Index");
    }

    /// <summary>
    /// Trigger challenge (redirects to login if not authenticated)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> ChallengeUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.ChallengeAsync()");

        // TODO: 

        return Empty;
    }

    /// <summary>
    /// Trigger sign in with test claims
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SignInUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.SignInAsync(principal)");

        // TODO: 

        return Empty;
    }

    /// <summary>
    /// Trigger sign out
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SignOutUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.SignOutAsync();");

        // TODO: 

        return Empty;
    }

    /// <summary>
    /// Trigger forbid (returns access denied)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Forbidden()
    {
        Console.WriteLine("\r\nCalling HttpContext.ForbidAsync();");

        // TODO:

        return Empty;
    }
}
