Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Public Class UserRoleManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~/Login.aspx")
        End If

        ' Only allow Admins
        If Helpers.GetCurrentUserRole(User) <> "Admin" Then
            Response.Redirect("~/AccessDenied.aspx")
        End If

        If Not IsPostBack Then
            BindUserGrid()
        End If
    End Sub

    Private Sub BindUserGrid()
        Dim connStr As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString
        Dim sql As String = "SELECT AppUserId, OktaEmail AS UserName, FullName, Role FROM appUserDetails ORDER BY FullName;"

        Using conn As New SqlConnection(connStr)
            Using cmd As New SqlCommand(sql, conn)
                conn.Open()
                Dim dt As New DataTable()
                dt.Load(cmd.ExecuteReader())
                gvUsers.DataSource = dt
                gvUsers.DataBind()
            End Using
        End Using

        If gvUsers.Rows.Count > 0 Then
            gvUsers.UseAccessibleHeader = True
            gvUsers.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Overrides Sub OnInit(e As EventArgs)
        MyBase.OnInit(e)
        AddHandler gvUsers.RowCommand, AddressOf gvUsers_RowCommand
    End Sub

    Protected Sub gvUsers_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUsers.RowCommand
        If e.CommandName = "EditUser" Then
            Dim userId As String = e.CommandArgument.ToString()
            Response.Redirect("UserRoleEdit.aspx?id=" & userId)
        End If
    End Sub

End Class
