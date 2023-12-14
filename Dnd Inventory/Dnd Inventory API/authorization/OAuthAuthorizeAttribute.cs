using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dnd_Inventory_API.authorization
{
    public class OAuthAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                return;

            if (!context.HttpContext.Request.Headers["Authorization"].Any())
            {
                context.Result = new UnauthorizedObjectResult(string.Empty);
                return;
            }

            var token = context.HttpContext.Request.Headers["Authorization"].First();

            try
            {
                GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(token).Result;

                if (payload == null)
                    context.Result = new UnauthorizedObjectResult(string.Empty);
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedObjectResult(ex.Message);
            }

            return;
        }
    }
}