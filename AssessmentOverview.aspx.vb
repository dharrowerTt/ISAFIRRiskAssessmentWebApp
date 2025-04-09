Imports System.Data.SqlClient
Imports System.Configuration

Public Class AssessmentOverview
    Inherits System.Web.UI.Page

    Private connStr As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim idStr = Request.QueryString("assessment_id")
            If String.IsNullOrEmpty(idStr) OrElse Not IsNumeric(idStr) Then
                Response.Redirect("AssessmentDashboard.aspx")
            End If

            LoadOverview(CInt(idStr))
        End If
    End Sub

    Private Sub LoadOverview(assessmentId As Integer)
        Using conn As New SqlConnection(connStr)
            Dim sql As String = "
                SELECT a.*, f.full_name 
                FROM Assessment a
                INNER JOIN Facility f ON a.facility_id = f.ID
                WHERE a.ID = @id"
            Dim cmd As New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", assessmentId)

            conn.Open()
            Using reader = cmd.ExecuteReader()
                If reader.Read() Then
                    ' Assessor Summary
                    litAssessorInfo.Text = $"<strong>Assessment ID:</strong> {assessmentId}<br/>
                        <strong>Assessor:</strong> {reader("assessor_first")} {reader("assessor_last")}<br/>
                        <strong>Email:</strong> {reader("assessor_email")}<br/>
                        <strong>Phone:</strong> {reader("assessor_phone")}<br/>
                        <strong>Facility:</strong> {reader("full_name")}<br/>
                        <strong>Started:</strong> {Convert.ToDateTime(reader("assessment_start")).ToString("g")}"

                    ' Set links + statuses
                    SetStatusCard(lnkThreat, litThreatStatus, assessmentId, "Threat", reader("progress_threat"))
                    SetStatusCard(lnkConsequence, litConsequenceStatus, assessmentId, "Consequence", reader("progress_consequence"))
                    SetStatusCard(lnkLifelines, litLifelinesStatus, assessmentId, "Lifelines", reader("progress_lifelines"))
                    SetStatusCard(lnkVulnerability, litVulnerabilityStatus, assessmentId, "Vulnerability", reader("progress_vulnerability"))
                Else
                    Response.Redirect("AssessmentDashboard.aspx")
                End If
            End Using
        End Using
    End Sub

    Private Sub SetStatusCard(link As HyperLink, label As Literal, assessmentId As Integer, section As String, progressValue As Object)
        Dim statusText As String = "Not Started"
        Dim cardClass As String = "bg-danger"

        If Not IsDBNull(progressValue) Then
            Select Case progressValue.ToString().ToLower()
                Case "in progress"
                    statusText = "In Progress"
                    cardClass = "bg-warning"
                Case "complete"
                    statusText = "Complete"
                    cardClass = "bg-success"
            End Select
        End If

        link.CssClass += " " & cardClass
        link.NavigateUrl = $"{section}Form.aspx?assessment_id={assessmentId}"
        label.Text = $"<strong>{statusText}</strong><br/>"
    End Sub

End Class
