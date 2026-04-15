using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CustomAuth;

/// <summary>
/// Minimal insecure custom authentication handler for ASP.NET Core
/// Demonstrates cookie-based authentication with customizable options
/// </summary>
public class CustomAuthHandler : SignInAuthenticationHandler<CustomAuthHandlerOptions>
{
    public CustomAuthHandler(IOptionsMonitor<CustomAuthHandlerOptions> options,
                            ILoggerFactory logger,
                            UrlEncoder encoder) : base(options, logger, encoder)
    {
        WriteToLog("CustomAuthHandler Constructor()");
    }

    [DebuggerHidden]
    private void WriteToLog(string message)
    {
        var scheme = Scheme?.Name ?? "[Default]";

        var msg = $"### [{DateTime.Now:HH:mm:ss}] - {scheme} - {message}";
        Console.WriteLine(msg);
    }

    /// <summary>
    /// Authenticate user by checking for authentication cookie.
    /// This method is called automatically by the framework on every incoming request
    /// </summary>
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var path = Request?.Path ?? "[Unknown]";
        WriteToLog($"HandleAuthenticateAsync: Called for: {path}");

        // Check for authentication cookie using the configured cookie name
        var username = Request.Cookies[Options.CookieName];

        if (string.IsNullOrEmpty(username))
        {
            // No cookie found - user not authenticated
            WriteToLog($"HandleAuthenticateAsync: No cookie '{Options.CookieName}' found - user not authenticated");
            return Task.FromResult(AuthenticateResult.NoResult());
        }
        else
        {
            // Cookie found - user authenticated

            // Create claims identity with username
            var myClaims = new List<Claim>()
                            {
                                new("name",username),
                            };

            var myIdentity = new ClaimsIdentity(claims: myClaims,
                                                authenticationType: "pwd",
                                                nameType: "name",
                                                roleType: "role");

            var myPrincipal = new ClaimsPrincipal(myIdentity);

            // Put the principal into an authentication ticket
            var ticket = new AuthenticationTicket(myPrincipal, Scheme.Name);

            WriteToLog($"HandleAuthenticateAsync: User '{username}' authenticated successfully from cookie");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        return Task.FromResult(AuthenticateResult.NoResult());
    }

    /// <summary>
    /// Sign in the user by setting authentication cookie. 
    /// </summary>
    protected override Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
    {
        var username = user.Identity?.Name ?? "Unknown";
        WriteToLog($"HandleSignInAsync: Signing in user '{username}'");

  
        // Set authentication cookie with configured name and expiration
        Response.Cookies.Append(key: Options.CookieName,value: username, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Lax
        });

        var redirectUri = Options.DefaultRedirectPath;
        Response.Redirect(redirectUri);

        WriteToLog($"HandleSignInAsync: Cookie '{Options.CookieName}' set, redirecting to '{redirectUri}'");




        return Task.CompletedTask;
    }

    /// <summary>
    /// Sign out user by removing authentication cookie
    /// </summary>
    
    protected override Task HandleSignOutAsync(AuthenticationProperties properties)
    {
        WriteToLog($"HandleSignOutAsync: Signing out user, deleting cookie '{Options.CookieName}'");


        // Delete cookie by setting it with past expiration
        Response.Cookies.Delete(Options.CookieName);

        // Redirect to return URL or default
        var redirectUri = Options.DefaultRedirectPath;
        Response.Redirect(redirectUri);

        WriteToLog($"HandleSignOutAsync: Cookie deleted, redirecting to '{redirectUri}'");


        return Task.CompletedTask;
    }

    /// <summary>
    /// Handle challenge (user not authenticated) - redirect to login page.
    /// </summary>
    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        WriteToLog($"HandleChallengeAsync: User needs to authenticate, redirecting to '{Options.LoginPath}'");

        Response.Redirect(Options.LoginPath);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Handle forbidden (user authenticated but not authorized)
    /// </summary>
    protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        WriteToLog($"HandleForbiddenAsync: Access denied, redirecting to '{Options.AccessDeniedPath}'");

        Response.Redirect(Options.AccessDeniedPath);    

        return Task.CompletedTask;
    }
}
