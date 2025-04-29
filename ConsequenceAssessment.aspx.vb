Imports System.Data.SqlClient
Imports System.Configuration

Partial Class ConsequenceAssessment
    Inherits System.Web.UI.Page

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim assessmentId As String = Request.QueryString("assessment_id")
            If String.IsNullOrEmpty(assessmentId) Then
                Response.Redirect("AssessmentDashboard.aspx")
            End If

            hfAssessmentID.Value = assessmentId
            LoadAssessmentDetails(Convert.ToInt32(assessmentId))
            LoadConsequenceMatrix()
        End If
    End Sub

    Private Sub LoadAssessmentDetails(assessmentId As Integer)
        ' (Use same logic you already have for loading assessment details)
    End Sub

    Private Sub LoadConsequenceMatrix()
        Using conn As New SqlConnection(connString)
            Dim dt As New DataTable()
            dt.Columns.Add("ThreatID", GetType(Integer))
            dt.Columns.Add("ThreatName", GetType(String))
            ' (Add rating1 to rating9 columns if needed)

            conn.Open()
            Dim cmd As New SqlCommand("
                SELECT ID, subhazard AS ThreatName 
                FROM Subhazard 
                WHERE Int_Ext = 'Int' OR Int_Ext = 'Ext'
                ORDER BY ID", conn)

            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                Dim row = dt.NewRow()
                row("ThreatID") = reader("ID")
                row("ThreatName") = reader("ThreatName").ToString()
                dt.Rows.Add(row)
            End While

            rptConsequence.DataSource = dt
            rptConsequence.DataBind()
        End Using
    End Sub

    Protected Sub btnSubmitConsequence_Click(sender As Object, e As EventArgs)
        ' (Save all the inputs from the matrix into Consequence table)
    End Sub

    Public Function GetImpactHeaders() As String
        ' Output the 9 header cells, each with impact name + tooltip
        ' (return literal HTML string)
    End Function

    Public Function GetBulkInputs() As String
        ' Output the 9 bulk input <input> fields
        ' (return literal HTML string)
    End Function

    Public Function GetConsequenceInputs(dataItem As Object) As String
        ' Output 9 textboxes for each row
        ' (return literal HTML string)
    End Function

End Class
