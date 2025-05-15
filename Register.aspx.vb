Imports System.Web.Security
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Register
    Inherits System.Web.UI.Page

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

    Protected Sub CreateUserWizard1_CreatedUser(sender As Object, e As EventArgs)
        Dim userName As String = CreateUserWizard1.UserName
        Dim defaultRole As String = "Viewer"

        ' Assign default role if needed
        If Not Roles.RoleExists(defaultRole) Then
            Roles.CreateRole(defaultRole)
        End If

        If Not Roles.IsUserInRole(userName, defaultRole) Then
            Roles.AddUserToRole(userName, defaultRole)
        End If

        ' Insert into UserProfile table
        Dim user As MembershipUser = Membership.GetUser(userName)
        If user IsNot Nothing Then
            Dim userId As Guid = CType(user.ProviderUserKey, Guid)

            Using conn As New SqlConnection(connString)
                Dim cmd As New SqlCommand("
                    INSERT INTO UserProfile (UserId)
                    VALUES (@UserId)", conn)

                cmd.Parameters.AddWithValue("@UserId", userId)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End If
    End Sub
End Class
