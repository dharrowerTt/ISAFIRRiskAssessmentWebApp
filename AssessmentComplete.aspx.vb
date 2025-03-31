Imports System.Data.SqlClient
Imports System.Configuration

Public Class AssessmentComplete
    Inherits System.Web.UI.Page

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString
    Private assessmentId As Integer = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Integer.TryParse(Request.QueryString("assessment_id"), assessmentId) Then
                Response.Redirect("AssessmentDashboard.aspx")
            End If
            LoadAssessmentSummary()
        End If
    End Sub

    Private Sub LoadAssessmentSummary()
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("
                SELECT a.assessor_first, a.assessor_last, a.assessment_start, f.full_name AS FacilityName, a.facility_id
                FROM Assessment a
                INNER JOIN Facility f ON a.facility_id = f.ID
                WHERE a.ID = @ID", conn)
            cmd.Parameters.AddWithValue("@ID", assessmentId)

            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                lblFacility.Text = reader("FacilityName").ToString()
                lblAssessor.Text = $"{reader("assessor_first")} {reader("assessor_last")}"
                lblStart.Text = Convert.ToDateTime(reader("assessment_start")).ToString("g")

                ' Store facility_id in ViewState for later
                ViewState("facility_id") = reader("facility_id")
            End If
        End Using
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim facilityId As Integer = CInt(ViewState("facility_id"))

        Using conn As New SqlConnection(connString)
            conn.Open()

            ' Mark previous assessments as not current
            Dim clearCurrentCmd As New SqlCommand("
                UPDATE Assessment SET Current = 0 
                WHERE facility_id = @facilityId AND ID <> @assessmentId", conn)
            clearCurrentCmd.Parameters.AddWithValue("@facilityId", facilityId)
            clearCurrentCmd.Parameters.AddWithValue("@assessmentId", assessmentId)
            clearCurrentCmd.ExecuteNonQuery()

            ' Set this assessment as completed
            Dim completeCmd As New SqlCommand("
                UPDATE Assessment SET assessment_end = GETDATE(), Current = 1 
                WHERE ID = @ID", conn)
            completeCmd.Parameters.AddWithValue("@ID", assessmentId)
            completeCmd.ExecuteNonQuery()
        End Using

        Response.Redirect("AssessmentDashboard.aspx?status=completed")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("AssessmentDashboard.aspx")
    End Sub
End Class
