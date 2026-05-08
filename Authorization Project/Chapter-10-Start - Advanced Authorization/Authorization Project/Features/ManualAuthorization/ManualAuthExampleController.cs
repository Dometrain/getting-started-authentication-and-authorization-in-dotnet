using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthorization.Features.ManualAuthorization;

public class ManualAuthExampleController : Controller
{
    public IActionResult Example1()
    {
        if (User?.Identity?.IsAuthenticated == true &&
            User.IsInRole("developer"))
        {
            return View();
        }
        else
        {
            return Redirect("/users/AccessDenied");
        }
    }

    public IActionResult Example2(int orderId)
    {
        var roleClaim = User.Claims.FirstOrDefault(c => c.Type == "role");
        var departmentClaim = User.Claims.FirstOrDefault(c => c.Type == "department");
        var clearanceClaim = User.Claims.FirstOrDefault(c => c.Type == "clearance");

        if (User.Identity != null &&
            User.Identity.IsAuthenticated &&
            roleClaim != null && roleClaim.Value == "developer" &&
            departmentClaim != null && departmentClaim.Value.ToLower() == "engineering" &&
            clearanceClaim != null && (clearanceClaim.Value == "high" ||
                                       clearanceClaim.Value == "very-high"))
        {
            var order = LoadOrderFromDatabase(orderId);

            return View(order);
        }

        return Forbid();
    }

    private static string? LoadOrderFromDatabase(int orderId)
    {
        // Todo......
        return orderId.ToString();
    }
}

