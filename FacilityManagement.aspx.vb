Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration


Partial Public Class FacilityManagement
        Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        ' Retrieve the connection string from Web.config
        Dim connStr As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString
        ' Simple SELECT query to retrieve facility data
        Dim sql As String = "SELECT ID, full_name, address1, city, state, zip FROM Facility ORDER BY full_name;"

        Using conn As New SqlConnection(connStr)
            Using cmd As New SqlCommand(sql, conn)
                conn.Open()
                Dim dt As New DataTable()
                dt.Load(cmd.ExecuteReader())
                gvFacilities.DataSource = dt
                gvFacilities.DataBind()
            End Using
        End Using

        ' Set the header row's TableSection for DataTables
        If gvFacilities.Rows.Count > 0 Then
            gvFacilities.UseAccessibleHeader = True
            gvFacilities.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub


    Protected Overrides Sub OnInit(e As EventArgs)
            MyBase.OnInit(e)
            AddHandler gvFacilities.RowCommand, AddressOf gvFacilities_RowCommand
        End Sub

        Protected Sub gvFacilities_RowCommand(source As Object, e As GridViewCommandEventArgs)
            If e.CommandName = "EditFacility" Then
                Dim facId As String = e.CommandArgument.ToString()
                ' Redirect to FacilityDetails.aspx with the facility ID for editing
                Response.Redirect("FacilityDetails.aspx?facid=" & facId)
            End If
        End Sub
    End Class

