Imports System.Data.SqlClient
Imports System.Configuration

Partial Public Class AddFacility
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim connStr As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString
        Dim sql As String = "INSERT INTO Facility (full_name, address1, city, state, zip) VALUES (@full_name, @address1, @city, @state, @zip);"

        Using conn As New SqlConnection(connStr)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@full_name", txtFullName.Text.Trim())
                cmd.Parameters.AddWithValue("@address1", txtAddress1.Text.Trim())
                cmd.Parameters.AddWithValue("@city", txtCity.Text.Trim())
                cmd.Parameters.AddWithValue("@state", txtState.Text.Trim())
                cmd.Parameters.AddWithValue("@zip", txtZip.Text.Trim())

                conn.Open()
                Try
                    cmd.ExecuteNonQuery()
                    Response.Redirect("FacilityManagement.aspx")
                Catch ex As Exception
                    lblMessage.Text = "Error saving facility: " & ex.Message
                End Try
            End Using
        End Using
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("FacilityManagement.aspx")
    End Sub
End Class
