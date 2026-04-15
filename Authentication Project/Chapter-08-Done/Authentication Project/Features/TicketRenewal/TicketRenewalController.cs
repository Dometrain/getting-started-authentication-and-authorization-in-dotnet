using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthentication.Features.TicketRenewal
{
    public class TicketRenewalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [Authorize()]
        [HttpGet("/TicketRenewal/getdata")]
        public async Task<IActionResult> GetData()
        {
            // This runs the cookie handler Authenticate step
            var result = await HttpContext.AuthenticateAsync();

            if (!result.Succeeded || result.Properties?.ExpiresUtc == null)
            {
                return Ok(new
                {
                    message = "Not authenticated or no ExpiresUtc found",
                    remainingSeconds = 0
                });
            }

            var expiresUtc = result.Properties.ExpiresUtc.Value;
            var nowUtc = DateTimeOffset.UtcNow;

            var remaining = expiresUtc - nowUtc;
            var remainingSeconds = (int)Math.Floor(remaining.TotalSeconds);

            if (remainingSeconds < 0)
                remainingSeconds = 0;

            var msg = $"Authentication Ticket expires in {remainingSeconds} seconds";

            return Ok(new
            {
                message = msg,
                remainingSeconds,
                issuedUtc = result.Properties.IssuedUtc,
                expiresUtc = expiresUtc
            });
        }
    }
}
