using Microsoft.AspNetCore.Authentication;

namespace CustomAuth;

/// <summary>
/// Extension methods for registering custom authentication
/// </summary>
public static class CustomAuthHandlerExtensions
{
    /// <summary>
    /// Add custom authentication with default options
    /// </summary>
    public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder)
    {
        return builder.AddCustomAuth(CustomAuthHandlerOptions.DefaultAuthenticationScheme, _ => { });
    }

    /// <summary>
    /// Add custom authentication with scheme name
    /// </summary>
    public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, 
                                                      string authenticationScheme)
    {
        return builder.AddCustomAuth(authenticationScheme, _ => { });
    }

    /// <summary>
    /// Add custom authentication with configuration
    /// </summary>
    public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, 
                                                      Action<CustomAuthHandlerOptions> configureOptions)
    {
        return builder.AddCustomAuth(CustomAuthHandlerOptions.DefaultAuthenticationScheme, 
                                     configureOptions);
    }

    /// <summary>
    /// Add custom authentication with scheme name and configuration
    /// </summary>
    public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, 
                                                      string authenticationScheme, 
                                                      Action<CustomAuthHandlerOptions> configureOptions)
    {
        return builder.AddCustomAuth(authenticationScheme, 
                                     displayName: null, 
                                     configureOptions: configureOptions);
    }

    /// <summary>
    /// Add custom authentication with scheme name, display name, and configuration
    /// </summary>
    public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, 
                                                      string authenticationScheme, 
                                                      string? displayName, 
                                                      Action<CustomAuthHandlerOptions> configureOptions)
    {
        return builder.AddScheme<CustomAuthHandlerOptions, CustomAuthHandler>(authenticationScheme, 
                                                                              displayName, 
                                                                              configureOptions);
    }
}