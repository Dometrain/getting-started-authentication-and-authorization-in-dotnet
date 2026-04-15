using Microsoft.AspNetCore.Authentication;

namespace StartcodeAuthentication.CustomAuthHandler;

/// <summary>
/// Options for custom authentication handler
/// </summary>
public class CustomAuthHandlerOptions : AuthenticationSchemeOptions
{
    /// <summary>
    /// The default scheme for this handler
    /// </summary>
    public static string DefaultAuthenticationScheme = "CustomAuthHandler";

    /// <summary>
    /// Name of the authentication cookie (default: "AuthCookie")
    /// </summary>
    public string CookieName { get; set; } = "AuthCookie";

    /// <summary>
    /// Path to redirect for login challenges    
    /// </summary>
    public string LoginPath { get; set; } = "/User/Login";

    /// <summary>
    /// Path to redirect when access is denied 
    /// </summary>
    public string AccessDeniedPath { get; set; } = "/User/AccessDenied";

    /// <summary>
    /// Default redirect path after sign in/out 
    /// </summary>
    public string DefaultRedirectPath { get; set; } = "/AuthTest";
}
