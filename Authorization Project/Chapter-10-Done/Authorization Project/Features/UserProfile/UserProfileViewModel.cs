using StartcodeAuthorization.Features.Invoices;
using StartcodeAuthorization.Features.Users;

namespace StartcodeAuthorization.Features.UserProfile;

public class UserProfileViewModel
{
    public User? UserProfile { get; set; }
    public List<Invoice>? UserInvoices { get; set; }
}