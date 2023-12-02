using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dnd_Inventory_API.authorization
{
    public class OAuthAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers["Authorization"].Any())
            {
                context.Result = new UnauthorizedObjectResult(string.Empty);
                return;
            }

            var token = context.HttpContext.Request.Headers["Authorization"].First();

            var validPayload = await GoogleJsonWebSignature.ValidateAsync(token);

            if (validPayload == null)
                context.Result = new UnauthorizedObjectResult(string.Empty);


            return;
        }
    }
}
