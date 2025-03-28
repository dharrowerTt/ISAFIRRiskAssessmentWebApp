Imports System.Data.SqlClient
Imports System.Configuration

Public Class FacilityAdd
    Inherits System.Web.UI.Page

    Private connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PopulateDropdowns()
        End If
    End Sub

    Private Sub PopulateDropdowns()
        PopulateSector()
        PopulateFacilityType()
        PopulateCounty()
    End Sub

    Private Sub PopulateSector()
        Using conn As New SqlConnection(connString)
            Dim cmd = New SqlCommand("SELECT NIPP_id, sector FROM Sector_LU ORDER BY sector", conn)
            conn.Open()
            ddlSector.DataSource = cmd.ExecuteReader()
            ddlSector.DataValueField = "NIPP_id"
            ddlSector.DataTextField = "sector"
            ddlSector.DataBind()
            ddlSector.Items.Insert(0, New ListItem("-- Select Sector --", ""))
        End Using
    End Sub

    Private Sub PopulateFacilityType()
        Using conn As New SqlConnection(connString)
            Dim cmd = New SqlCommand("SELECT ID, type FROM FacilityType_LU ORDER BY type", conn)
            conn.Open()
            ddlFacilityType.DataSource = cmd.ExecuteReader()
            ddlFacilityType.DataValueField = "ID"
            ddlFacilityType.DataTextField = "type"
            ddlFacilityType.DataBind()
            ddlFacilityType.Items.Insert(0, New ListItem("-- Select Facility Type --", ""))
        End Using
    End Sub

    Private Sub PopulateCounty()
        Using conn As New SqlConnection(connString)
            Dim cmd = New SqlCommand("SELECT ID, County FROM County_LU ORDER BY County", conn)
            conn.Open()
            ddlCounty.DataSource = cmd.ExecuteReader()
            ddlCounty.DataValueField = "ID"
            ddlCounty.DataTextField = "County"
            ddlCounty.DataBind()
            ddlCounty.Items.Insert(0, New ListItem("-- Select County --", ""))
        End Using
    End Sub

    Protected Sub ddlSector_SelectedIndexChanged(sender As Object, e As EventArgs)
        Using conn As New SqlConnection(connString)
            Dim cmd = New SqlCommand("SELECT ID, subsector FROM Subsector_LU WHERE sector_LU_NIPP_id=@sectorID ORDER BY subsector", conn)
            cmd.Parameters.AddWithValue("@sectorID", ddlSector.SelectedValue)
            conn.Open()
            ddlSubsector.DataSource = cmd.ExecuteReader()
            ddlSubsector.DataValueField = "ID"
            ddlSubsector.DataTextField = "subsector"
            ddlSubsector.DataBind()
            ddlSubsector.Items.Insert(0, New ListItem("-- Select Subsector --", ""))
        End Using
    End Sub

    Protected Sub btnBeginAssessment_Click(sender As Object, e As EventArgs)
        ' Same Insert logic as previously provided.
        ' Save new Facility ID in session & redirect.
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/FacilityManagement.aspx")
    End Sub

End Class
