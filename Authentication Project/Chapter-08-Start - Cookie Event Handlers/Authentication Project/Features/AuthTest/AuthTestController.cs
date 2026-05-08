using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

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

        AuthenticateResult result = await HttpContext.AuthenticateAsync();

        TempData["Message"] = $"Authentication result: {(result.Succeeded ? "Success" : "Failed")}";

        return RedirectToAction("Index");
    }

    /// <summary>
    /// Trigger challenge (redirects to login if not authenticated)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> ChallengeUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.ChallengeAsync()");

        var properties = new AuthenticationProperties()
        {
            RedirectUri = "/",
        };

        return Challenge(properties);
    }

    /// <summary>
    /// Trigger sign in with test claims
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SignInUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.SignInAsync(principal)");

        //1. Load the claims for this user 
        var myClaims = new List<Claim>()
               {
                   new("sub","1234"),
                   new("name","Bob"),
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

        var properties = new AuthenticationProperties(items)
        {
            //RedirectUri = "/AuthTest",
        };

        foreach (var item in properties.Items)
            Console.WriteLine(item);


        await HttpContext.SignInAsync(myPrincipal, properties);

        //return LocalRedirect(Request.Query["ReturnUrl"]);
        return Redirect("/");

    }

    /// <summary>
    /// Trigger sign out
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SignOutUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.SignOutAsync();");

        await HttpContext.SignOutAsync();

        // You control the redirect
        return RedirectToAction("Index");
    }

    /// <summary>
    /// Trigger forbid (returns access denied)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Forbidden()
    {
        Console.WriteLine("\r\nCalling HttpContext.ForbidAsync();");

        await HttpContext.ForbidAsync();

        return Empty;
    }
}
