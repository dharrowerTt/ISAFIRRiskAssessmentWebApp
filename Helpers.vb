Imports System.Data.SqlClient
Imports System.Configuration

Public Class Helpers

    ' Retrieve current user role (supporting simulation)
    Public Shared Function GetCurrentUserRole(user As System.Security.Principal.IPrincipal) As String
        Dim simulated = HttpContext.Current?.Session?("SimulatedRole")
        If simulated IsNot Nothing Then
            Return simulated.ToString()
        End If

        Dim userRoles() As String = System.Web.Security.Roles.GetRolesForUser(user.Identity.Name)
        If userRoles.Length > 0 Then
            Return userRoles(0)
        End If

        Return "Unknown"
    End Function

    ' Check if the user has a specific permission
    Public Shared Function HasPermission(userEmail As String, permissionName As String) As Boolean
        Dim connStr As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString
        Dim query As String = "
            SELECT COUNT(*) FROM appUserDetails d
            INNER JOIN appRolePermissions p ON d.Role = p.Role
            WHERE d.OktaEmail = @Email AND p.Permission = @Permission
        "

        Using conn As New SqlConnection(connStr)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Email", userEmail)
                cmd.Parameters.AddWithValue("@Permission", permissionName)
                conn.Open()
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function

End Class
