Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.OpenIdConnect

Partial Class OktaLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        System.Diagnostics.Debug.WriteLine(">>> OktaLogin.aspx Loaded")

        Dim owinUser = Context.GetOwinContext().Authentication.User
        Dim isOwinAuthenticated = owinUser?.Identity IsNot Nothing AndAlso owinUser.Identity.IsAuthenticated
        Dim isOidcReturn = Request.Url.AbsolutePath.ToLower().Contains("signin-oidc")

        System.Diagnostics.Debug.WriteLine($">>> [OktaLogin] Identity Authenticated: {isOwinAuthenticated}")
        System.Diagnostics.Debug.WriteLine($">>> [OktaLogin] Identity Name: {owinUser?.Identity?.Name}")


        If isOwinAuthenticated Then
            System.Diagnostics.Debug.WriteLine(">>> [OktaLogin] User is authenticated via OWIN. Redirecting to Login.aspx")
            Response.Redirect("~/Login.aspx", False)
            Context.ApplicationInstance.CompleteRequest()
            Return
        End If

        If Not isOidcReturn Then
            System.Diagnostics.Debug.WriteLine(">>> [OktaLogin] Not OIDC return - triggering Okta challenge")
            Dim props As New AuthenticationProperties With {
                .RedirectUri = "/Login.aspx"
            }
            Context.GetOwinContext().Authentication.Challenge(props, OpenIdConnectAuthenticationDefaults.AuthenticationType)
            Response.StatusCode = 401
            Response.End()
        Else
            System.Diagnostics.Debug.WriteLine(">>> [OktaLogin] Returned from OIDC - awaiting token processing")
        End If
    End Sub
End Class
