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


    ' Check if the user has a specific permission based on RolePermissionMap
    Public Shared Function HasPermission(userEmail As String, permissionName As String) As Boolean
        Dim connStr As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString
        Dim query As String = "
        SELECT 1
        FROM appUserDetails aud
        JOIN RolePermissionMap rpm ON aud.Role = rpm.RoleName
        JOIN RolePermissions rp ON rpm.PermissionId = rp.PermissionId
        WHERE aud.OktaEmail = @Email
          AND rp.PermissionName = @Permission
          AND rpm.IsAllowed = 1
    "

        Using conn As New SqlConnection(connStr)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Email", userEmail)
                cmd.Parameters.AddWithValue("@Permission", permissionName)
                conn.Open()
                Return cmd.ExecuteScalar() IsNot Nothing
            End Using
        End Using
    End Function

    Public Shared Function HasAnyPermission(userEmail As String, ParamArray permissions() As String) As Boolean
        For Each p In permissions
            If HasPermission(userEmail, p) Then
                Return True
            End If
        Next
        Return False
    End Function



End Class
