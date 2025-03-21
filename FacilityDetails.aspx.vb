Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net.NetworkInformation


Partial Public Class FacilityDetails
        Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not IsPostBack Then
            ' Check if a facility ID was provided for editing
            If Not String.IsNullOrEmpty(Request.QueryString("facid")) Then
                LoadFacility(Request.QueryString("facid"))
            End If
        End If
    End Sub

    Private Sub LoadFacility(facilityId As String)
            Dim connStr As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString
            Dim sql As String = "SELECT full_name, address1, city, state, zip FROM Facility WHERE ID = @ID;"

            Using conn As New SqlConnection(connStr)
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", facilityId)
                    conn.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        txtFullName.Text = reader("full_name").ToString()
                        txtAddress1.Text = reader("address1").ToString()
                        txtCity.Text = reader("city").ToString()
                        txtState.Text = reader("state").ToString()
                        txtZip.Text = reader("zip").ToString()
                    Else
                        lblMessage.Text = "Facility not found."
                    End If
                End Using
            End Using
        End Sub

        Protected Sub btnSave_Click(sender As Object, e As EventArgs)
            Dim connStr As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString
            Dim sql As String = ""
            Dim isUpdate As Boolean = Not String.IsNullOrEmpty(Request.QueryString("facid"))

            If isUpdate Then
                sql = "UPDATE Facility SET full_name = @full_name, address1 = @address1, city = @city, state = @state, zip = @zip WHERE ID = @ID;"
            Else
                sql = "INSERT INTO Facility (full_name, address1, city, state, zip) VALUES (@full_name, @address1, @city, @state, @zip);"
            End If

            Using conn As New SqlConnection(connStr)
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@full_name", txtFullName.Text.Trim())
                    cmd.Parameters.AddWithValue("@address1", txtAddress1.Text.Trim())
                    cmd.Parameters.AddWithValue("@city", txtCity.Text.Trim())
                    cmd.Parameters.AddWithValue("@state", txtState.Text.Trim())
                    cmd.Parameters.AddWithValue("@zip", txtZip.Text.Trim())

                    If isUpdate Then
                        cmd.Parameters.AddWithValue("@ID", Request.QueryString("facid"))
                    End If

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

