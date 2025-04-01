using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Okta.AspNet;
using Owin;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(ISAFIRRiskAssessmentWebApp.Startup))]

namespace ISAFIRRiskAssessmentWebApp
{
    public class Startup
    {
        public const string PreferredUsername = "preferred_username";
        public const string OktaUserId = "sub";
        public const string Roles = "roles";
        public const string AuthorizedAgencyEsam = "authorizedAgency";

        private readonly string _authority = ConfigurationManager.AppSettings["okta:OktaDomain"];
        private readonly string _clientId = ConfigurationManager.AppSettings["okta:ClientId"];
        private readonly string _clientSecret = ConfigurationManager.AppSettings["okta:ClientSecret"];
        private readonly string _redirectUri = ConfigurationManager.AppSettings["okta:RedirectUri"];

        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOktaMvc(new OktaMvcOptions()
            {
                OktaDomain = _authority,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                RedirectUri = _redirectUri,
                GetClaimsFromUserInfoEndpoint = true,
                Scope = new List<string> { "openid", "profile", "email" },
                OpenIdConnectEvents = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = OnTokenValidated
                }
            });
        }

        /// <summary>
        /// Custom logic to manipulate identity after token is validated.
        /// </summary>
        private Task OnTokenValidated(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> context)
        {
            var currentIdentity = context.AuthenticationTicket.Identity;
            var customIdentity = new ClaimsIdentity(currentIdentity, new List<Claim>(), currentIdentity.AuthenticationType, PreferredUsername, Roles);
            var properties = context.AuthenticationTicket.Properties;
            var ticket = new AuthenticationTicket(customIdentity, properties);
            context.AuthenticationTicket = ticket;
            return Task.CompletedTask;
        }
    }
}
