Imports System.Data.SqlClient
Imports System.Configuration
Imports Microsoft.Owin.Security

Partial Class Register
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not HttpContext.Current.Request.IsAuthenticated Then
                HttpContext.Current.GetOwinContext().Authentication.Challenge(New AuthenticationProperties() With {
                    .RedirectUri = "~/Register.aspx"
                }, "OpenIdConnect")
            End If
        End If
    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs)
        Dim userEmail As String = HttpContext.Current.User.Identity.Name
        Dim fullName As String = txtFullName.Text.Trim()
        Dim department As String = txtDepartment.Text.Trim()
        Dim role As String = ddlRole.SelectedValue

        Dim connStr As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

        Using conn As New SqlConnection(connStr)
            conn.Open()

            ' Prevent duplicate registration
            Dim checkCmd As New SqlCommand("SELECT COUNT(*) FROM AppUserDetails WHERE OktaEmail = @Email", conn)
            checkCmd.Parameters.AddWithValue("@Email", userEmail)

            Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
            If exists > 0 Then
                HttpContext.Current.Response.Redirect("/Default.aspx")
            End If

            ' Insert new user
            Dim insertCmd As New SqlCommand("
                INSERT INTO AppUserDetails (OktaEmail, FullName, Department, Role)
                VALUES (@Email, @FullName, @Department, @Role)", conn)

            insertCmd.Parameters.AddWithValue("@Email", userEmail)
            insertCmd.Parameters.AddWithValue("@FullName", fullName)
            insertCmd.Parameters.AddWithValue("@Department", department)
            insertCmd.Parameters.AddWithValue("@Role", role)

            insertCmd.ExecuteNonQuery()
        End Using

        HttpContext.Current.Response.Redirect("~/Default.aspx")
    End Sub
End Class
