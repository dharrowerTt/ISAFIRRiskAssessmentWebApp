Imports Microsoft.IdentityModel.Protocols.OpenIdConnect
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.Notifications
Imports Microsoft.Owin.Security.OpenIdConnect
Imports Okta.AspNet
Imports Owin
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Security.Claims
Imports System.Threading.Tasks

<Assembly: OwinStartup(GetType(ISAFIRRiskAssessmentWebApp.Startup))>

Namespace ISAFIRRiskAssessmentWebApp
    Public Class Startup
        Public Const PreferredUsername As String = "preferred_username"
        Public Const OktaUserId As String = "sub"
        Public Const Roles As String = "roles"
        Public Const AuthorizedAgencyEsam As String = "authorizedAgency"

        Private ReadOnly _authority As String = ConfigurationManager.AppSettings("okta:OktaDomain")
        Private ReadOnly _clientId As String = ConfigurationManager.AppSettings("okta:ClientId")
        Private ReadOnly _clientSecret As String = ConfigurationManager.AppSettings("okta:ClientSecret")
        Private ReadOnly _redirectUri As String = ConfigurationManager.AppSettings("okta:RedirectUri")

        Public Sub Configuration(app As IAppBuilder)
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType)

            app.UseCookieAuthentication(New CookieAuthenticationOptions())

            app.UseOktaMvc(New OktaMvcOptions() With {
                .OktaDomain = _authority,
                .ClientId = _clientId,
                .ClientSecret = _clientSecret,
                .RedirectUri = _redirectUri,
                .GetClaimsFromUserInfoEndpoint = True,
                .Scope = New List(Of String) From {"openid", "profile", "email"},
                .OpenIdConnectEvents = New OpenIdConnectAuthenticationNotifications() With {
                    .SecurityTokenValidated = AddressOf OnTokenValidated
                }
            })
        End Sub
        Private Function OnTokenValidated(context As SecurityTokenValidatedNotification(Of OpenIdConnectMessage, OpenIdConnectAuthenticationOptions)) As Task
            System.Diagnostics.Debug.WriteLine(">>> [Okta] Token validated")

            Dim currentIdentity = context.AuthenticationTicket.Identity
            Dim customIdentity = New ClaimsIdentity(currentIdentity, New List(Of Claim)(), currentIdentity.AuthenticationType, PreferredUsername, Roles)
            Dim properties = context.AuthenticationTicket.Properties
            Dim ticket = New AuthenticationTicket(customIdentity, properties)
            context.AuthenticationTicket = ticket

            Return Task.CompletedTask
        End Function

    End Class
End Namespace
