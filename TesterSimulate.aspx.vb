Imports System

Public Class TesterSimulate
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not User.IsInRole("Tester") Then
                Response.Redirect("Unauthorized.aspx")
            End If
        End If
    End Sub

    Protected Sub btnSimulate_Click(sender As Object, e As EventArgs)
        Dim selectedRole As String = ddlSimulatedRole.SelectedValue

        If String.IsNullOrEmpty(selectedRole) Then
            lblStatus.Text = "Please select a role to simulate."
            lblStatus.CssClass = "text-danger"
            lblStatus.Visible = True
            Return
        End If

        ' Store the simulated role in session
        Session("SimulatedRole") = selectedRole

        lblStatus.Text = $"You are now simulating the '{selectedRole}' role."
        lblStatus.CssClass = "text-success"
        lblStatus.Visible = True
    End Sub
End Class
