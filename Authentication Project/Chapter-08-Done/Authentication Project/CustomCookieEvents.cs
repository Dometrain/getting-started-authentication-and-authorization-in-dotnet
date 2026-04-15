using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace StartcodeAuthentication;


/// <summary>
/// Custom cookie authentication events handler.
/// Override methods to add custom behavior before or after the default logic.
/// </summary>
public class CustomCookieEvents : CookieAuthenticationEvents
{
    private readonly ILogger<CustomCookieEvents> logger;

    public CustomCookieEvents(ILogger<CustomCookieEvents> logger)
    {
        this.logger = logger;
    }

    public override Task SigningIn(CookieSigningInContext context)
    {
        // Add your custom logic here

        // Call the base method to execute the default behavior
        // Remove this line to replace the default behavior entirely
        return base.SigningIn(context);
    }

    public override Task SignedIn(CookieSignedInContext context)
    {
        return base.SignedIn(context);
    }

    public override Task SigningOut(CookieSigningOutContext context)
    {
        return base.SigningOut(context);
    }

    public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        return base.ValidatePrincipal(context);
    }

    public override Task CheckSlidingExpiration(CookieSlidingExpirationContext context)
    {
        return base.CheckSlidingExpiration(context);
    }

    public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
    {
        return base.RedirectToLogin(context);
    }

    public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
    {
        logger.LogInformation("Access denied for user {User}", context.HttpContext.User.Identity?.Name);

        context.Response.Redirect("/Home/AccessDenied");

        return Task.CompletedTask;
    }

    public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
    {
        return base.RedirectToReturnUrl(context);
    }

    public override Task RedirectToLogout(RedirectContext<CookieAuthenticationOptions> context)
    {
        return base.RedirectToLogout(context);
    }
}
