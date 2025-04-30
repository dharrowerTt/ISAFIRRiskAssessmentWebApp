Imports System.Data.SqlClient
Imports System.Configuration

Partial Class LifelineAssessment
    Inherits System.Web.UI.Page

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString
    Public TopLevelLifelines As DataTable
    Public SubLifelines As DataTable
    Private demoMode As Boolean = False

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim assessmentId As String = Request.QueryString("assessment_id")
            If String.IsNullOrEmpty(assessmentId) Then
                demoMode = True
                hfAssessmentID.Value = "0"
                LoadDemoAssessmentDetails()
            Else
                hfAssessmentID.Value = assessmentId
                LoadAssessmentDetails(Convert.ToInt32(assessmentId))
            End If

            LoadLifelineData()
        End If
    End Sub

    Private Sub LoadDemoAssessmentDetails()
        litAssessmentID.Text = "DEMO123"
        litFacility.Text = "Sample Facility"
        litAssessor.Text = "Jane Doe"
        litContact.Text = "jane.doe@example.com / 555-1234"
        litStartDate.Text = "2025-04-01"
    End Sub

    Private Sub LoadAssessmentDetails(assessmentId As Integer)
        Using conn As New SqlConnection(connString)
            conn.Open()
            Dim cmd As New SqlCommand("
                SELECT a.ID, f.FacilityName, u.FullName, u.Email, u.Phone, a.DateStarted
                FROM Assessment a
                JOIN Facility f ON a.FacilityID = f.ID
                JOIN AppUserDetails u ON a.AssessorID = u.UserID
                WHERE a.ID = @id", conn)
            cmd.Parameters.AddWithValue("@id", assessmentId)
            Dim reader = cmd.ExecuteReader()
            If reader.Read() Then
                litAssessmentID.Text = assessmentId.ToString()
                litFacility.Text = reader("FacilityName").ToString()
                litAssessor.Text = reader("FullName").ToString()
                litContact.Text = reader("Email").ToString() & " / " & reader("Phone").ToString()
                litStartDate.Text = Convert.ToDateTime(reader("DateStarted")).ToString("yyyy-MM-dd")
            End If
        End Using
    End Sub

    Private Sub LoadLifelineData()
        TopLevelLifelines = New DataTable()
        SubLifelines = New DataTable()

        Using conn As New SqlConnection(connString)
            conn.Open()

            ' Top-level lifelines
            Dim topCmd As New SqlCommand("SELECT ID, LifelineLabel, IconPath FROM Lifeline_LU WHERE Toplevel = 1 ORDER BY ColOrder", conn)
            Using da As New SqlDataAdapter(topCmd)
                da.Fill(TopLevelLifelines)
            End Using

            ' Sub-lifelines
            Dim subCmd As New SqlCommand("SELECT ID, LifelineLabel, IconPath, ParentID FROM Lifeline_LU WHERE Toplevel = 0 ORDER BY ParentID, [Order]", conn)
            Using da As New SqlDataAdapter(subCmd)
                da.Fill(SubLifelines)
            End Using
        End Using
    End Sub

    Protected Sub btnSubmitLifeline_Click(sender As Object, e As EventArgs)
        If demoMode Then
            ClientScript.RegisterStartupScript(Me.GetType(), "demoAlert", "alert('Demo mode: No data was saved.');", True)
            Return
        End If

        ' TODO: Parse and save user inputs for top-level toggles and sub-lifeline impact levels
        ' You can collect the data via hidden fields or form posts via JS.
    End Sub
End Class
