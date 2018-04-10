using Hivemind.Entities;
using Hivemind.Exceptions;
using Hivemind.Managers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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
            var userManager = (IUserManager)container.GetService(typeof(IUserManager));

            try
            {
                var user = userManager.Login(new Login()
                {
                    Email = context.UserName,
                    Password = context.Password,
                });

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", user.Email));
                identity.AddClaim(new Claim("userId", user.UserGUID));
                identity.AddClaim(new Claim("role", "user"));

                // Generate a token
                context.Validated(identity);
            }
            catch (HivemindException)
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return;
            }
        }

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            SetCORSPolicy(context.OwinContext);

            // handle preflight requests
            if (context.Request.Method == "OPTIONS")
            {
                context.RequestCompleted();
                return Task.FromResult(0);
            }

            return base.MatchEndpoint(context);
        }

        public void SetCORSPolicy(IOwinContext context)
        {
            // TODO: Add a whitelist of allowed URLs
            //var allowedUrls = ConfigurationManager.AppSettings["allowedOrigins"];
            var allowedUrls = "http://localhost:4200";

            if (!string.IsNullOrWhiteSpace(allowedUrls))
            {
                var list = allowedUrls.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (list.Length > 0)
                {
                    string origin = context.Request.Headers.Get("Origin");
                    var found = list.Where(item => item == origin).Any();
                    if (found && !context.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
                    {
                        context.Response.Headers.Add("Access-Control-Allow-Origin",
                                                     new string[] { origin });

                    }
                }

                context.Response.Headers.Add("Access-Control-Allow-Headers",
                                   new string[] { "Authorization", "Content-Type" });
                context.Response.Headers.Add("Access-Control-Allow-Methods",
                                       new string[] { "OPTIONS", "POST", "GET", "PUT", "DELETE" });

            }
        }
    }
}