using Microsoft.AspNetCore.Authorization;

namespace Dnd_Inventory_API.authorization
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(
          AuthorizationHandlerContext context,
          HasScopeRequirement requirement
        )
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                context.Succeed(requirement);

            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == $"https://{requirement.Issuer}/"))
                return Task.CompletedTask;

            var scopes = context.User
              .FindFirst(c => c.Type == "scope" && c.Issuer == $"https://{requirement.Issuer}/")?.Value.Split(' ');

            if (scopes != null && scopes.Any(s => s == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
