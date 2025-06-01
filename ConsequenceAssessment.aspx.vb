Imports System.Data.SqlClient
Imports System.Configuration

Partial Class ConsequenceAssessment
    Inherits System.Web.UI.Page
    Private demoMode As Boolean = False
    Private ImpactCategories As New List(Of (Name As String, HelpText As String))

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

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
            LoadImpactCategories()
            LoadConsequenceMatrix()

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
    SELECT ID, Subhazard AS ThreatName 
    FROM dbo.Subhazard_LU 
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
        If demoMode Then
            ' Display a warning and skip DB logic
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Demo mode: Data not saved.');", True)
            Return
        End If

        ' Save consequence data
        Using conn As New SqlConnection(connString)
            conn.Open()
            
            ' Clear existing consequences for this assessment
            Dim clearCmd As New SqlCommand("DELETE FROM Consequence WHERE assessment_id = @AID", conn)
            clearCmd.Parameters.AddWithValue("@AID", Convert.ToInt32(hfAssessmentID.Value))
            clearCmd.ExecuteNonQuery()

            ' Insert new consequences
            For Each row As RepeaterItem In rptConsequence.Items
                Dim threatId As Integer = Convert.ToInt32(DataBinder.Eval(row.DataItem, "ThreatID"))
                
                For i As Integer = 1 To ImpactCategories.Count
                    Dim input As TextBox = row.FindControl($"c_{threatId}_{i}")
                    If input IsNot Nothing AndAlso Not String.IsNullOrEmpty(input.Text) Then
                        Dim cmd As New SqlCommand("
                            INSERT INTO Consequence (assessment_id, threat_id, impact_id, rating)
                            VALUES (@AID, @TID, @IID, @Rating)", conn)
                        
                        cmd.Parameters.AddWithValue("@AID", Convert.ToInt32(hfAssessmentID.Value))
                        cmd.Parameters.AddWithValue("@TID", threatId)
                        cmd.Parameters.AddWithValue("@IID", i)
                        cmd.Parameters.AddWithValue("@Rating", Convert.ToInt32(input.Text))
                        cmd.ExecuteNonQuery()
                    End If
                Next
            Next
        End Using

        ' Navigate to Lifeline Assessment
        Response.Redirect($"LifelineAssessment.aspx?assessment_id={hfAssessmentID.Value}")
    End Sub


    Public Function GetImpactHeaders() As String
        Dim sb As New StringBuilder()

        For Each impact In ImpactCategories
            sb.AppendFormat("<th>{0}</th>", impact.Name)

        Next

        Return sb.ToString()
    End Function


    Public Function GetBulkInputs() As String
        Dim sb As New StringBuilder()
        For i As Integer = 1 To ImpactCategories.Count
            sb.Append("<th><input type='number' class='form-control form-control-sm bulk-input consequence-input text-center' min='0' max='3' maxlength='1' /></th>")
        Next
        Return sb.ToString()
    End Function



    Public Function GetConsequenceInputs(dataItem As Object) As String
        Dim threatId As Integer = DataBinder.Eval(dataItem, "ThreatID")
        Dim sb As New StringBuilder()

        For i As Integer = 1 To ImpactCategories.Count
            sb.AppendFormat(
            "<td><input type='number' name='c_{0}_{1}' class='form-control form-control-sm consequence-input text-center' min='0' max='3' maxlength='1' /></td>",
            threatId, i)
        Next

        Return sb.ToString()
    End Function
    Public ReadOnly Property ImpactCount As Integer
        Get
            Return ImpactCategories.Count
        End Get
    End Property



    Private Sub LoadImpactCategories()
        Using conn As New SqlConnection(connString)
            conn.Open()
            Dim cmd As New SqlCommand("
            SELECT ImpactName, HelpText 
            FROM dbo.ConsequenceImpact_LU 
            ORDER BY ImpactID", conn)

            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                ImpactCategories.Add((reader("ImpactName").ToString(), reader("HelpText").ToString()))
            End While
        End Using
    End Sub

    Public Function GetHelperButtons() As String
        Dim sb As New StringBuilder()
        For i As Integer = 1 To ImpactCategories.Count
            Dim helpText = ImpactCategories(i - 1).HelpText.Replace("'", "\'")
            sb.AppendFormat(
            "<th class='text-center'><button type='button' class='btn btn-sm btn-outline-secondary' onclick='showHelp({0})'>?</button></th>",
            i)
        Next
        Return sb.ToString()
    End Function

    Public ReadOnly Property HelpTextJSArray As String
        Get
            If ImpactCategories.Count = 0 Then
                Return "[]"
            End If

            Dim sb As New StringBuilder()
            sb.Append("[")

            For i As Integer = 0 To ImpactCategories.Count - 1
                Dim helpText = ImpactCategories(i).HelpText.Replace("""", "\""").Replace(vbCrLf, "").Replace(Environment.NewLine, "")
                sb.Append("""" & helpText & """")
                If i < ImpactCategories.Count - 1 Then sb.Append(",")
            Next

            sb.Append("]")
            Return sb.ToString()
        End Get
    End Property



End Class
