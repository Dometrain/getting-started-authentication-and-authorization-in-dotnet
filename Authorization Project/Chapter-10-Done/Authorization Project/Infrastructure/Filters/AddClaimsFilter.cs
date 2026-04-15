using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace StartcodeAuthorization.Infrastructure.Filters
{
    public class AddClaimsFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User?.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                // Create a copy to avoid modifying the original
                var newIdentity = new ClaimsIdentity(identity.Claims,
                                                     identity.AuthenticationType,
                                                     identity.NameClaimType,
                                                     identity.RoleClaimType);

                // Add new claims
                newIdentity.AddClaim(new Claim(type: "request_time",
                                               value: DateTimeOffset.UtcNow.ToString(),
                                               valueType: ClaimValueTypes.String,
                                               issuer: "AddClaimFilter"));

                newIdentity.AddClaim(new Claim(type: "dynamic_role",
                                               value: "temp_admin",
                                               valueType: ClaimValueTypes.String,
                                               issuer: "AddClaimFilter"));

                // Create new principal with the updated identity
                context.HttpContext.User = new ClaimsPrincipal(newIdentity);
            }
        }
    }
}
