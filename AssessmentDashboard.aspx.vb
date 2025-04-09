Imports System.Data.SqlClient
Imports System.Configuration

Public Class AssessmentDashboard
    Inherits System.Web.UI.Page

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadAssessments()
        End If
    End Sub

    Private Sub LoadAssessments()
        Using conn As New SqlConnection(connString)
            Dim query As String = "
                SELECT 
                    a.ID,
                    f.full_name AS FacilityName,
                    a.assessor,
                    a.assessment_start AS AssessmentStart,
                    a.assessment_end AS AssessmentEnd,
                    CASE WHEN a.is_current = 1 THEN 'Yes' ELSE 'No' END AS is_current
                FROM Assessment a
                INNER JOIN Facility f ON a.facility_id = f.ID
                ORDER BY a.assessment_start DESC"

            Dim cmd As New SqlCommand(query, conn)
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            gvAssessments.DataSource = reader
            gvAssessments.DataBind()
        End Using
    End Sub

    Protected Sub btnNewAssessment_Click(sender As Object, e As EventArgs)
        Response.Redirect("AssessmentStart.aspx")
    End Sub

End Class
