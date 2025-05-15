Imports System.Data.SqlClient
Imports System.Configuration

Public Class _Default
    Inherits Page

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            Dim userEmail As String = User.Identity.Name

            ' Metrics: Show only to users with permission to manage users or view facilities
            If Helpers.HasPermission(userEmail, "ManageUsers") OrElse Helpers.HasPermission(userEmail, "ViewFacilities") Then
                pnlMetrics.Visible = True
                LoadDashboardMetrics()
            End If

            ' Button visibility by permission
            pnlManageFacilities.Visible = Helpers.HasPermission(userEmail, "ViewFacilities") OrElse Helpers.HasPermission(userEmail, "EditFacilities")
            pnlStartAssessment.Visible = Helpers.HasPermission(userEmail, "EditFacilities")
            pnlUserManagement.Visible = Helpers.HasPermission(userEmail, "ManageUsers")
            pnlReports.Visible = Helpers.HasPermission(userEmail, "ViewReports") OrElse Helpers.HasPermission(userEmail, "ExportData")
        End If
    End Sub

    Private Sub LoadDashboardMetrics()
        Using conn As New SqlConnection(connString)
            conn.Open()

            ' Facilities
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM Facility", conn)
                lblTotalFacilities.Text = cmd.ExecuteScalar().ToString()
            End Using

            ' Pending assessments
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM Assessment WHERE assessment_end IS NULL", conn)
                lblPendingAssessments.Text = cmd.ExecuteScalar().ToString()
            End Using

            ' User count
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM appUserDetails", conn)
                lblUserCount.Text = cmd.ExecuteScalar().ToString()
            End Using
        End Using
    End Sub
End Class
