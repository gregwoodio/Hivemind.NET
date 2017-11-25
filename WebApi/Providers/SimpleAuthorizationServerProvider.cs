using Hivemind.Entities;
using Hivemind.Exceptions;
using Hivemind.Managers;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebApi.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // This method validates the client (the Angular app). Since I'm only using the one app,
            // I'm assuming the client is valid.
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Allow CORS for middleware.
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var container = new UnityResolver();
            var userManager = (IUserManager)container.GetService(typeof (IUserManager));

            try
            {
                userManager.Login(new User()
                {
                    Email = context.UserName,
                    Password = context.Password
                });
            }
            catch (HivemindException)
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            // Generate a token
            context.Validated(identity);
        }
    }
}