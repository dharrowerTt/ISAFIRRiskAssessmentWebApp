Imports System.Data.SqlClient
Imports System.Configuration

Partial Public Class RoleFunctionManagement
    Inherits System.Web.UI.Page

    Private ReadOnly connStr As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            LoadRoles()
        End If

        If ddlRoles.SelectedIndex > -1 AndAlso ddlRoles.SelectedValue <> "" Then
            LoadPermissionsForRole(ddlRoles.SelectedValue)
        End If
    End Sub

    Private Sub LoadRoles()
        ddlRoles.Items.Clear()

        Dim query As String = "SELECT RoleName, DisplayName, Scope, Description FROM Role_LU ORDER BY Scope, DisplayName"

        Using conn As New SqlConnection(connStr)
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Using reader = cmd.ExecuteReader()
                    Dim currentScope As String = ""

                    While reader.Read()
                        Dim scope = reader("Scope").ToString()
                        Dim roleName = reader("RoleName").ToString()
                        Dim displayName = reader("DisplayName").ToString()
                        Dim description = reader("Description").ToString()

                        ' Add section header
                        If scope <> currentScope Then
                            Dim headerItem As New ListItem($"--- {scope.ToUpper()} ROLES ---", "")
                            headerItem.Attributes.Add("disabled", "true")
                            headerItem.Attributes.Add("style", "font-weight:bold; color:#999;")
                            ddlRoles.Items.Add(headerItem)
                            currentScope = scope
                        End If

                        ' Add role option
                        Dim item As New ListItem(displayName, roleName)
                        item.Attributes.Add("title", description) ' optional tooltip
                        ddlRoles.Items.Add(item)
                    End While
                End Using
            End Using
        End Using
    End Sub

    Protected Sub ddlRoles_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlRoles.SelectedValue <> "" Then
            LoadPermissionsForRole(ddlRoles.SelectedValue)
        End If
    End Sub

    Private Sub LoadPermissionsForRole(roleName As String)
        Dim dt As New DataTable()

        Using conn As New SqlConnection(connStr)
            Dim sql As String = "
                SELECT rp.PermissionId, rp.PermissionName, rp.Description,
                       ISNULL(rpm.IsAllowed, 0) AS IsAllowed
                FROM RolePermissions rp
                LEFT JOIN RolePermissionMap rpm ON rp.PermissionId = rpm.PermissionId AND rpm.RoleName = @RoleName
                ORDER BY rp.PermissionName"

            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@RoleName", roleName)
                conn.Open()
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        gvPermissions.DataSource = dt
        gvPermissions.DataBind()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim roleName As String = ddlRoles.SelectedValue
        If String.IsNullOrEmpty(roleName) Then Exit Sub

        Using conn As New SqlConnection(connStr)
            conn.Open()

            For Each row As GridViewRow In gvPermissions.Rows
                Dim permissionName As String = row.Cells(0).Text
                Dim chkAllowed As CheckBox = CType(row.FindControl("chkAllowed"), CheckBox)

                Dim permissionId As Integer = GetPermissionIdByName(permissionName, conn)

                ' Delete existing mapping
                Dim deleteCmd As New SqlCommand("
                    DELETE FROM RolePermissionMap 
                    WHERE RoleName = @RoleName AND PermissionId = @PermissionId", conn)
                deleteCmd.Parameters.AddWithValue("@RoleName", roleName)
                deleteCmd.Parameters.AddWithValue("@PermissionId", permissionId)
                deleteCmd.ExecuteNonQuery()

                ' Insert new mapping if checked
                If chkAllowed.Checked Then
                    Dim insertCmd As New SqlCommand("
                        INSERT INTO RolePermissionMap (RoleName, PermissionId, IsAllowed) 
                        VALUES (@RoleName, @PermissionId, 1)", conn)
                    insertCmd.Parameters.AddWithValue("@RoleName", roleName)
                    insertCmd.Parameters.AddWithValue("@PermissionId", permissionId)
                    insertCmd.ExecuteNonQuery()
                End If
            Next
        End Using

        LoadPermissionsForRole(roleName)
    End Sub

    Private Function GetPermissionIdByName(permissionName As String, conn As SqlConnection) As Integer
        Dim cmd As New SqlCommand("SELECT PermissionId FROM RolePermissions WHERE PermissionName = @Name", conn)
        cmd.Parameters.AddWithValue("@Name", permissionName)
        Return Convert.ToInt32(cmd.ExecuteScalar())
    End Function
End Class
