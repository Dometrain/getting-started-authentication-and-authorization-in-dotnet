using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthorization.Features.Secure;

[Authorize]
public class SecureController : Controller
{
    public SecureController()
    {
    }

    [Authorize("finance")]
    public IActionResult Finance()
    {
        return View();
    }

    [Authorize("sales")]
    public IActionResult Sales()
    {
        return View();
    }

    [Authorize(Roles = "admin")]
    public IActionResult Admin()
    {
        return View();
    }

    public IActionResult Support()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult Contact()
    {
        return View();
    }
}

