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
            BindRoleDropdown()

            Dim userIdStr As String = Request.QueryString("id")
            If String.IsNullOrEmpty(userIdStr) Then
                lblMessage.Text = "Missing user ID."
                lblMessage.Visible = True
                Exit Sub
            End If

            LoadUserDetails(userIdStr)
        End If
    End Sub

    Private Sub BindRoleDropdown()
        ddlRole.Items.Clear()

        Dim query As String = "SELECT RoleName, DisplayName, Scope FROM Role_LU ORDER BY Scope, DisplayName"
        Dim currentScope As String = ""

        Using conn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim scope = reader("Scope").ToString()
                        Dim name = reader("DisplayName").ToString()
                        Dim value = reader("RoleName").ToString()

                        If scope <> currentScope Then
                            Dim groupItem As New ListItem($"--- {scope.ToUpper()} ROLES ---", "")
                            groupItem.Attributes.Add("disabled", "true")
                            groupItem.Attributes.Add("style", "font-weight:bold; color:#999;")
                            ddlRole.Items.Add(groupItem)
                            currentScope = scope
                        End If

                        ddlRole.Items.Add(New ListItem(name, value))
                    End While

                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadUserDetails(appUserId As String)
        Dim query As String = "
        SELECT OktaEmail, FullName, Role, StateCode, FacilityID 
        FROM appUserDetails 
        WHERE AppUserId = @AppUserId"

        Using conn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@AppUserId", appUserId)
                conn.Open()
                Dim reader = cmd.ExecuteReader()
                If reader.Read() Then
                    txtEmail.Text = reader("OktaEmail").ToString()
                    txtFullName.Text = reader("FullName").ToString()

                    ' Safely assign role if it still exists in the dropdown
                    Dim savedRole As String = reader("Role").ToString()
                    If ddlRole.Items.FindByValue(savedRole) IsNot Nothing Then
                        ddlRole.SelectedValue = savedRole
                    Else
                        ddlRole.Items.Insert(0, New ListItem($"[Missing Role: {savedRole}]", savedRole))
                        ddlRole.SelectedValue = savedRole
                    End If

                    txtState.Text = reader("StateCode").ToString()
                    txtFacility.Text = reader("FacilityID").ToString()
                    pnlEdit.Visible = True
                Else
                    lblMessage.Text = "User not found."
                    lblMessage.Visible = True
                End If
            End Using
        End Using
    End Sub


    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim query As String = "
            UPDATE appUserDetails 
            SET Role = @Role, 
                StateCode = @StateCode, 
                FacilityID = @FacilityID 
            WHERE OktaEmail = @Email"

        Using conn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Role", ddlRole.SelectedValue)
                cmd.Parameters.AddWithValue("@StateCode", txtState.Text)
                cmd.Parameters.AddWithValue("@FacilityID", txtFacility.Text)
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
