using Microsoft.AspNetCore.Authorization;
using StartcodeAuthorization.Infrastructure.Attributes;

internal static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {

        services.AddSingleton<IAuthorizationHandler, FinanceAccessHandler>();
        services.AddSingleton<IAuthorizationHandler, ManagementAccessHandler>();
        services.AddSingleton<IAuthorizationHandler, ActiveUserHandler>();
        services.AddSingleton<IAuthorizationHandler, InvoiceOwnershipHandler>();


        services.AddAuthorization(o =>
        {
            o.FallbackPolicy = new AuthorizationPolicyBuilder()
                                   .RequireAuthenticatedUser()
                                   .Build();
            
            // Default policy, if none is specified [Authorize]
            o.DefaultPolicy = new AuthorizationPolicyBuilder()
               .RequireAuthenticatedUser()
               .RequireClaim("JobTitle")
               .Build();


            o.AddPolicy(Policies.Finance, policy =>
            {
                policy.Requirements.Add(new FinanceAccessRequirement());
            });


            o.AddPolicy(Policies.Sales, policy =>
                policy.RequireRole("manager", "sales")
                      .RequireClaim("country", ["Sweden", "Denmark", "Norway"]));



            o.AddPolicy(Policies.Admin, policy =>
                policy.RequireRole("admin")
                      .RequireRole("management"));          //AND

            o.AddPolicy(Policies.Customer, policy =>
                    policy.RequireAuthenticatedUser());


            o.AddPolicy("invoiceowner", policy =>
                    policy.Requirements.Add(new InvoiceOwnershipRequirement()));
        });

        return services;
    }
}
