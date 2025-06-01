Imports System.Data.SqlClient
Imports System.Configuration

Public Class AssessmentProgress
    Inherits System.Web.UI.UserControl

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString
    Public Property CurrentStep As String = ""
    Public Property AssessmentId As Integer = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Get assessment ID from query string if not set
            If AssessmentId = 0 AndAlso Not String.IsNullOrEmpty(Request.QueryString("assessment_id")) Then
                AssessmentId = Convert.ToInt32(Request.QueryString("assessment_id"))
            End If
        End If
    End Sub

    Public Function StepCompleted(step As String) As Boolean
        If AssessmentId = 0 Then Return False

        Using conn As New SqlConnection(connString)
            conn.Open()

            Select Case step.ToLower()
                Case "facility"
                    ' Facility is always completed if we have an assessment ID
                    Return True

                Case "threat"
                    ' Check if any threats are recorded
                    Dim cmd As New SqlCommand("
                        SELECT COUNT(*) FROM Threat 
                        WHERE assessment_id = @AID", conn)
                    cmd.Parameters.AddWithValue("@AID", AssessmentId)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0

                Case "consequence"
                    ' Check if any consequences are recorded
                    Dim cmd As New SqlCommand("
                        SELECT COUNT(*) FROM Consequence 
                        WHERE assessment_id = @AID", conn)
                    cmd.Parameters.AddWithValue("@AID", AssessmentId)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0

                Case "lifeline"
                    ' Check if any lifelines are recorded
                    Dim cmd As New SqlCommand("
                        SELECT COUNT(*) FROM LifelineAssessment 
                        WHERE assessment_id = @AID", conn)
                    cmd.Parameters.AddWithValue("@AID", AssessmentId)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0

                Case "vulnerability"
                    ' Check if any vulnerabilities are recorded
                    Dim cmd As New SqlCommand("
                        SELECT COUNT(*) FROM Vulnerability 
                        WHERE assessment_id = @AID", conn)
                    cmd.Parameters.AddWithValue("@AID", AssessmentId)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0

                Case Else
                    Return False
            End Select
        End Using
    End Function
End Class 