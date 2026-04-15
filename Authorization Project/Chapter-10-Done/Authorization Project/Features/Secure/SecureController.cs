using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StartcodeAuthorization.Infrastructure.Attributes;
using StartcodeAuthorization.Infrastructure.Filters;

namespace StartcodeAuthorization.Features.Secure;


public class SecureController : Controller
{
    public SecureController()
    {
    }

    [RequirePolicy(Policies.Finance)]
    public IActionResult Finance()
    {
        return View();
    }

    [RequirePolicy(Policies.Sales)]
    public IActionResult Sales()
    {
        return View();
    }

    [RequirePolicy(Policies.Admin)]
    public IActionResult Admin()
    {
        return View();
    }

    [RequirePolicy(Policies.Customer)]
    public IActionResult Support()
    {
        return View();
    }

    [Authorize]
    public IActionResult Contact()
    {
        return View();
    }
}
