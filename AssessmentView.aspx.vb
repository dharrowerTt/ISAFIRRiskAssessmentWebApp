Imports System.Data.SqlClient
Imports System.Configuration

Public Class AssessmentView
    Inherits System.Web.UI.Page

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim idStr As String = Request.QueryString("id")
            If Not String.IsNullOrEmpty(idStr) AndAlso IsNumeric(idStr) Then
                LoadAssessment(CInt(idStr))
            Else
                Response.Redirect("AssessmentDashboard.aspx")
            End If
        End If
    End Sub

    Private Sub LoadAssessment(assessmentId As Integer)
        Using conn As New SqlConnection(connString)
            Dim query As String = "
                SELECT a.*, f.full_name AS FacilityName
                FROM Assessment a
                INNER JOIN Facility f ON a.facility_id = f.ID
                WHERE a.ID = @ID"

            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ID", assessmentId)

            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                lblFacilityName.Text = reader("FacilityName").ToString()
                lblAssessor.Text = reader("assessor_first").ToString() & " " & reader("assessor_last").ToString()
                lblEmail.Text = reader("assessor_email").ToString()
                lblPhone.Text = reader("assessor_phone").ToString()
                lblStart.Text = If(reader("assessment_start") IsNot DBNull.Value, Convert.ToDateTime(reader("assessment_start")).ToString("g"), "")
                lblEnd.Text = If(reader("assessment_end") IsNot DBNull.Value, Convert.ToDateTime(reader("assessment_end")).ToString("g"), "—")
                lblCurrent.Text = If(reader("Current") = True, "Yes", "No")
            End If
        End Using
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Response.Redirect("AssessmentDashboard.aspx")
    End Sub
End Class
