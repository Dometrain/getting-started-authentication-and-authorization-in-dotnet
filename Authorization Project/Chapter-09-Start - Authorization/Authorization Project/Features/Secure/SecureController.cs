using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthorization.Features.Secure;

public class SecureController : Controller
{
    public SecureController()
    {
    }

    public IActionResult Finance()
    {
        return View();
    }

    public IActionResult Sales()
    {
        return View();
    }

    public IActionResult Admin()
    {
        return View();
    }

    public IActionResult Support()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }
}

