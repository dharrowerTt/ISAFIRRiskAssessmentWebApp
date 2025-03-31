Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class UserRoleManagement
    Inherits System.Web.UI.Page

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadUsers()
            LoadAuditTrail()
        End If
    End Sub

    Private Sub LoadUsers()
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("SELECT UserID, UserName FROM Users ORDER BY UserName", conn)
            conn.Open()
            ddlUserAccounts.DataSource = cmd.ExecuteReader()
            ddlUserAccounts.DataTextField = "UserName"
            ddlUserAccounts.DataValueField = "UserID"
            ddlUserAccounts.DataBind()
            ddlUserAccounts.Items.Insert(0, New ListItem("-- Select User --", ""))
        End Using
    End Sub

    Protected Sub ddlUserAccounts_SelectedIndexChanged(sender As Object, e As EventArgs)
        ClearSelections()

        If ddlUserAccounts.SelectedValue <> "" Then
            LoadUserRole(ddlUserAccounts.SelectedValue)
        End If
    End Sub

    Private Sub ClearSelections()
        ddlRoles.ClearSelection()
        cblPermissions.Items.Clear()
        pnlPermissions.Visible = False
        cblFacilityAssignments.Items.Clear()
        pnlFacilityAssignment.Visible = False
    End Sub

    Private Sub LoadUserRole(userID As String)
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("SELECT Role FROM UserRoles WHERE UserID = @UserID", conn)
            cmd.Parameters.AddWithValue("@UserID", userID)
            conn.Open()
            Dim role As Object = cmd.ExecuteScalar()
            If role IsNot Nothing Then
                ddlRoles.SelectedValue = role.ToString()
                LoadPermissionsForRole(role.ToString())
            End If
        End Using
    End Sub

    Protected Sub ddlRoles_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim role As String = ddlRoles.SelectedValue
        LoadPermissionsForRole(role)
    End Sub

    Private Sub LoadPermissionsForRole(role As String)
        pnlPermissions.Visible = True
        cblPermissions.Items.Clear()

        ' Example permissions per role (customize as needed)
        Select Case role
            Case "FacilityManager"
                cblPermissions.Items.Add(New ListItem("Edit Facility Details", "EditFacility", True))
                cblPermissions.Items.Add(New ListItem("Export Facility Reports", "ExportReports"))
            Case "Assessor"
                cblPermissions.Items.Add(New ListItem("Submit Assessments", "SubmitAssessments", True))
                cblPermissions.Items.Add(New ListItem("View Historical Assessments", "ViewAssessments"))
            Case "Supervisor"
                cblPermissions.Items.Add(New ListItem("Approve Assessments", "ApproveAssessments", True))
                cblPermissions.Items.Add(New ListItem("Manage Assessors", "ManageAssessors"))
            Case "Viewer"
                cblPermissions.Items.Add(New ListItem("View Reports", "ViewReports", True))
        End Select

        ' Load Facility Assignments if applicable
        pnlFacilityAssignment.Visible = (role = "FacilityManager" OrElse role = "Assessor" OrElse role = "Supervisor" OrElse role = "Viewer")
        If pnlFacilityAssignment.Visible Then
            LoadFacilityAssignments()
        End If
    End Sub

    Private Sub LoadFacilityAssignments()
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("SELECT FacilityID, FacilityName FROM Facility ORDER BY FacilityName", conn)
            conn.Open()
            cblFacilityAssignments.DataSource = cmd.ExecuteReader()
            cblFacilityAssignments.DataTextField = "FacilityName"
            cblFacilityAssignments.DataValueField = "FacilityID"
            cblFacilityAssignments.DataBind()
        End Using
    End Sub

    Protected Sub btnSaveRole_Click(sender As Object, e As EventArgs)
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("UPDATE UserRoles SET Role = @Role WHERE UserID = @UserID", conn)
            cmd.Parameters.AddWithValue("@UserID", ddlUserAccounts.SelectedValue)
            cmd.Parameters.AddWithValue("@Role", ddlRoles.SelectedValue)
            conn.Open()
            cmd.ExecuteNonQuery()

            ' Add audit trail logic here
            Dim auditCmd As New SqlCommand("INSERT INTO RoleAudit (UserID, Role, ChangedBy, ChangedDate) VALUES (@UserID, @Role, @ChangedBy, GETDATE())", conn)
            auditCmd.Parameters.AddWithValue("@UserID", ddlUserAccounts.SelectedValue)
            auditCmd.Parameters.AddWithValue("@Role", ddlRoles.SelectedValue)
            auditCmd.Parameters.AddWithValue("@ChangedBy", User.Identity.Name)
            auditCmd.ExecuteNonQuery()
        End Using

        LoadAuditTrail()
    End Sub

    Private Sub LoadAuditTrail()
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("SELECT TOP 20 UserID, Role, ChangedBy, ChangedDate FROM RoleAudit ORDER BY ChangedDate DESC", conn)
            conn.Open()
            gvAuditTrail.DataSource = cmd.ExecuteReader()
            gvAuditTrail.DataBind()
        End Using
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("Dashboard.aspx") ' Adjust as necessary
    End Sub

End Class
