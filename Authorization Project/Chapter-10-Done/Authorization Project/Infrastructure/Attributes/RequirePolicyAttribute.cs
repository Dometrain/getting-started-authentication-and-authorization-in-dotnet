using Microsoft.AspNetCore.Authorization;

namespace StartcodeAuthorization.Infrastructure.Attributes;

public sealed class RequirePolicyAttribute : AuthorizeAttribute
{
    public RequirePolicyAttribute(string policy) : base(policy) { }
}
