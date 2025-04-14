Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.OpenIdConnect

Partial Class Test
    Inherits System.Web.UI.Page

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Dim props As New AuthenticationProperties() With {
            .RedirectUri = "/OktaLogin.aspx"
        }

        Context.GetOwinContext().Authentication.Challenge(props, OpenIdConnectAuthenticationDefaults.AuthenticationType)
        Response.StatusCode = 401
        Response.End()
    End Sub
End Class
