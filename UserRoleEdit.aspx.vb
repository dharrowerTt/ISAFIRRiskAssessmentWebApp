Imports System.Data.SqlClient
Imports System.Configuration

Partial Public Class UserRoleEdit
    Inherits System.Web.UI.Page

    Private ReadOnly Property ConnStr As String
        Get
            Return ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            Dim userIdStr As String = Request.QueryString("id")
            If String.IsNullOrEmpty(userIdStr) Then
                lblMessage.Text = "Missing user ID."
                lblMessage.Visible = True
                Exit Sub
            End If

            LoadUserDetails(userIdStr)
        End If
    End Sub

    Private Sub LoadUserDetails(appUserId As String)
        Dim query As String = "SELECT OktaEmail, FullName, Role FROM appUserDetails WHERE AppUserId = @AppUserId"
        Using conn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@AppUserId", appUserId)
                conn.Open()
                Dim reader = cmd.ExecuteReader()
                If reader.Read() Then
                    txtEmail.Text = reader("OktaEmail").ToString()
                    txtFullName.Text = reader("FullName").ToString()
                    ddlRole.SelectedValue = reader("Role").ToString()
                    pnlEdit.Visible = True
                Else
                    lblMessage.Text = "User not found."
                    lblMessage.Visible = True
                End If
            End Using
        End Using
    End Sub


    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim query As String = "UPDATE appUserDetails SET Role = @Role WHERE OktaEmail = @Email"
        Using conn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Role", ddlRole.SelectedValue)
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("UserRoleManagement.aspx")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("UserRoleManagement.aspx")
    End Sub
End Class
