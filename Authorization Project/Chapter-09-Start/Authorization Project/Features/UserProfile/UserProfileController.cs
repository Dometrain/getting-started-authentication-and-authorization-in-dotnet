using Microsoft.AspNetCore.Mvc;
using StartcodeAuthorization.Features.Invoices;
using StartcodeAuthorization.Features.Users;
using System.Security.Claims;

namespace StartcodeAuthorization.Features.UserProfile;

public class UserProfileController : Controller
{
    private readonly IInvoiceDatabase invoiceDatabase;
    private readonly IUserDatabase userDatabase;

    public UserProfileController(IInvoiceDatabase invoiceDatabase, IUserDatabase userDatabase)
    {
        this.invoiceDatabase = invoiceDatabase;
        this.userDatabase = userDatabase;
    }

    public IActionResult Index()
    {
        // Get current user's ID from claims
        var subject = User.FindFirst("sub")?.Value;

        if (subject == null || !int.TryParse(subject, out int currentUserId))
        {
            return Unauthorized("Invalid user session");
        }

        var userProfile = userDatabase.GetUserProfileById(currentUserId);
        var userInvoices = invoiceDatabase.GetInvoicesByUserId(currentUserId);

        var viewModel = new UserProfileViewModel
        {
            UserProfile = userProfile,
            UserInvoices = userInvoices
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult UpdateUser()
    {
        // Get current user's ID from claims
        var userIdClaim = User.FindFirst("sub")?.Value;

        if (!int.TryParse(userIdClaim, out var userId))
        {
            return BadRequest("Invalid user ID.");
        }

        // Fetch the user's current profile to pre-populate the form
        var userProfile = userDatabase.GetUserProfileById(userId);

        if (userProfile == null)
        {
            return NotFound("User not found.");
        }


        var model = new UpdateUserModel
        {
            Name = userProfile.GetNameClaim(),
            Roles = userProfile.Claims?
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList()
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult UpdateUser(UpdateUserModel updateUser)
    {
        var userIdClaim = User.FindFirst("sub")?.Value;

        if (!int.TryParse(userIdClaim, out var userId))
        {
            return BadRequest("Invalid user ID.");
        }

        userDatabase.UpdateUser(userId, updateUser);
        return RedirectToAction("Index");
    }

}
