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
        Dim email = User.Identity.Name
        Dim firstName = ""
        Dim lastName = ""
        Dim phone = ""

        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("SELECT FullName, Role, Phone, OktaEmail FROM appUserDetails WHERE OktaEmail = @Email", conn)
            cmd.Parameters.AddWithValue("@Email", email)
            conn.Open()
            Dim reader = cmd.ExecuteReader()
            If reader.Read() Then
                Dim fullName = reader("FullName").ToString()
                Dim parts = fullName.Split(" "c)
                If parts.Length > 0 Then firstName = parts(0)
                If parts.Length > 1 Then lastName = String.Join(" ", parts.Skip(1))
                If Not IsDBNull(reader("Phone")) Then phone = reader("Phone").ToString()
            End If
        End Using

        txtAssessor.Text = email
        txtAssessorFirst.Text = firstName
        txtAssessorLast.Text = lastName
        txtEmail.Text = email
        txtPhone.Text = phone
    End Sub

    Protected Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Dim newId As Integer

        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("
                INSERT INTO Assessment (facility_id, assessor_first, assessor_last, assessor_phone, assessor_email, assessor, assessment_start, is_current)
                OUTPUT INSERTED.ID
                VALUES (@facility, @first, @last, @phone, @email, @assessor, GETDATE(), 1)
            ", conn)

            With cmd.Parameters
                .AddWithValue("@facility", ddlFacility.SelectedValue)
                .AddWithValue("@first", txtAssessorFirst.Text)
                .AddWithValue("@last", txtAssessorLast.Text)
                .AddWithValue("@phone", txtPhone.Text)
                .AddWithValue("@email", txtEmail.Text)
                .AddWithValue("@assessor", txtAssessor.Text)
            End With

            conn.Open()
            newId = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        ' Redirect directly to first step: terrorism
        Response.Redirect($"ThreatAssessment.aspx?assessment_id={newId}&step=terrorism")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("AssessmentDashboard.aspx")
    End Sub
End Class
