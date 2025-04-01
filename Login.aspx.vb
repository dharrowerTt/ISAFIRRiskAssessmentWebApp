Imports Microsoft.Owin.Security
Imports Microsoft.AspNet.Identity
Imports Owin
Imports System.Web
Imports System.Web.Http
Imports Microsoft.Owin


Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub btnOktaLogin_Click(sender As Object, e As EventArgs)
        HttpContext.Current.GetOwinContext().Authentication.Challenge(
            New AuthenticationProperties() With {
                .RedirectUri = "~/Default.aspx"
            }, "OpenIdConnect")

    End Sub
End Class
