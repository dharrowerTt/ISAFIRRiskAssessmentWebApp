Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Optional: Add any logic you need here
    End Sub

    Protected Sub Login1_LoggedIn(ByVal sender As Object, ByVal e As EventArgs)
        Dim userName As String = Login1.UserName
        Dim userId As Guid = GetUserIdByUserName(userName)
        Dim role As String = GetUserRoleByUserId(userId)

        Session("UserId") = userId
        Session("UserRole") = role
        Session("UserEmail") = userName
        Session("LoggedInUser") = userName

        System.Diagnostics.Debug.WriteLine(">>> Login1_LoggedIn: Session(LoggedInUser) set")
        System.Diagnostics.Debug.WriteLine(">>> Session after login:")
        System.Diagnostics.Debug.WriteLine(">>> UserId: " & userId.ToString())
        System.Diagnostics.Debug.WriteLine(">>> UserRole: " & role)
        System.Diagnostics.Debug.WriteLine(">>> UserEmail: " & userName)
        System.Diagnostics.Debug.WriteLine(">>> LoggedInUser: " & userName)


        Response.Redirect("~/Default.aspx")
    End Sub

    Private Function GetUserIdByUserName(userName As String) As Guid
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString)
            conn.Open()
            Dim cmd As New SqlCommand("SELECT UserId FROM aspnet_Users WHERE LoweredUserName = @uname", conn)
            cmd.Parameters.AddWithValue("@uname", userName.ToLower())
            Return CType(cmd.ExecuteScalar(), Guid)
        End Using
    End Function

    Private Function GetUserRoleByUserId(userId As Guid) As String
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString)
            conn.Open()
            Dim cmd As New SqlCommand("
                SELECT r.RoleName 
                FROM aspnet_UsersInRoles ur 
                JOIN aspnet_Roles r ON ur.RoleId = r.RoleId 
                WHERE ur.UserId = @uid", conn)
            cmd.Parameters.AddWithValue("@uid", userId)
            Return CType(cmd.ExecuteScalar(), String)
        End Using
    End Function

End Class
