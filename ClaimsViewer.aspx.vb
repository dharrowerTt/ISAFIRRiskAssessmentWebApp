Partial Class ClaimsViewer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Write("Not authenticated.")
        End If
    End Sub
End Class
