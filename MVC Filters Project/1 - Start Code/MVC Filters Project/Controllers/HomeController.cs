using Microsoft.AspNetCore.Mvc;

namespace MVC_Filter_Demo___Startcode.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            Console.WriteLine("     ¤¤¤ Home Controller - Constructor");
        }

        public IActionResult Index()
        {
            Console.WriteLine("     ¤¤¤ Home Controller - Index method");
            return View();
        }

        public IActionResult Time()
        {
            Console.WriteLine("     ¤¤¤ Home Controller - Time method");
            return Ok(DateTime.Now.ToString());
        }

        public IActionResult ForbidUser()
        {
            Console.WriteLine("     ¤¤¤ Home Controller - Forbid method (returns Forbid)");
            return Forbid();
        }

        public IActionResult ChallengeUser()
        {
            Console.WriteLine("     ¤¤¤ Home Controller - Challenge method (returns challenge)");
            return Challenge();
        }

        public IActionResult Exception()
        {
            Console.WriteLine("     ¤¤¤ Home Controller - Exception method (throws exception");
            throw new InvalidOperationException("Test exception for filter demo");
        }
    }
}
