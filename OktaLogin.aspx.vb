Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.OpenIdConnect

Public Class OktaLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        System.Diagnostics.Debug.WriteLine("OktaLogin.aspx Loaded")
        System.Diagnostics.Debug.WriteLine("IsAuthenticated: " & Context.User.Identity.IsAuthenticated)

        If Not Page.IsPostBack Then
            If Context.User.Identity.IsAuthenticated Then
                System.Diagnostics.Debug.WriteLine("Already authenticated, redirecting to Login.aspx")
                Response.Redirect("~/Login.aspx", False)
                Context.ApplicationInstance.CompleteRequest()
                Return
            End If

            ' Only challenge if not already coming back from Okta
            If Not Request.Url.AbsolutePath.ToLower().Contains("signin-oidc") Then
                System.Diagnostics.Debug.WriteLine("Challenging authentication via Okta")
                Dim props As New AuthenticationProperties With {
                    .RedirectUri = "/OktaLogin.aspx"
                }
                Context.GetOwinContext().Authentication.Challenge(props, OpenIdConnectAuthenticationDefaults.AuthenticationType)
                Response.StatusCode = 401
                Response.End()
            End If
        End If
    End Sub


End Class
