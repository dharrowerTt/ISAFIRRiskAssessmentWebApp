Imports System.Data.SqlClient
Imports System.Configuration

Public Class AssessmentStart
    Inherits System.Web.UI.Page

    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadFacilities()
            PreFillUserInfo()
        End If
    End Sub

    Private Sub LoadFacilities()
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("SELECT ID, full_name FROM Facility ORDER BY full_name", conn)
            conn.Open()
            ddlFacility.DataSource = cmd.ExecuteReader()
            ddlFacility.DataTextField = "full_name"
            ddlFacility.DataValueField = "ID"
            ddlFacility.DataBind()
        End Using
    End Sub

    Private Sub PreFillUserInfo()
        Dim username As String = User.Identity.Name
        txtAssessor.Text = username
        txtEmail.Text = $"{username}@example.com" ' ← Update domain if needed
    End Sub

    Protected Sub btnStart_Click(sender As Object, e As EventArgs)
        Dim newId As Integer
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("
                INSERT INTO Assessment (facility_id, assessor_first, assessor_last, assessor_phone, assessor_email, assessor, assessment_start, Current)
                OUTPUT INSERTED.ID
                VALUES (@facility, @first, @last, @phone, @email, @assessor, GETDATE(), 1)
            ", conn)

            cmd.Parameters.AddWithValue("@facility", ddlFacility.SelectedValue)
            cmd.Parameters.AddWithValue("@first", txtAssessorFirst.Text)
            cmd.Parameters.AddWithValue("@last", txtAssessorLast.Text)
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text)
            cmd.Parameters.AddWithValue("@email", txtEmail.Text)
            cmd.Parameters.AddWithValue("@assessor", txtAssessor.Text)

            conn.Open()
            newId = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Response.Redirect($"ThreatHazardForm.aspx?assessment_id={newId}")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("AssessmentDashboard.aspx")
    End Sub
End Class
