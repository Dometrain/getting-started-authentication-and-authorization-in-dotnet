using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthentication.Features.Api
{
    // URL: /Api/Index
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            if (User?.Identity?.IsAuthenticated == false)
            {
                return Challenge();
            }
            else
            {
                return Ok("API Response");
            }
        }
    }
}