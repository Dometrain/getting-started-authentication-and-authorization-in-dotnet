using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthentication.Features.Secure;

// URL: /Secure/Index
public class SecureController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
