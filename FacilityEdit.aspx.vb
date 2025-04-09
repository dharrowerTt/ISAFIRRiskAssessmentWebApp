Imports System.Data.SqlClient
Imports System.Configuration

Public Class FacilityEdit
    Inherits System.Web.UI.Page

    Private connString As String = ConfigurationManager.ConnectionStrings("MembershipDB").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PopulateDropdowns()

            Dim facilityIdStr = Request.QueryString("id")
            If Not String.IsNullOrEmpty(facilityIdStr) AndAlso IsNumeric(facilityIdStr) Then
                LoadFacility(CInt(facilityIdStr))
            Else
                Response.Redirect("FacilityManagement.aspx")
            End If
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
        Dim sectorId = ddlSector.SelectedValue
        If String.IsNullOrEmpty(sectorId) Then Return

        Using conn As New SqlConnection(connString)
            Dim cmd = New SqlCommand("SELECT ID, subsector FROM Subsector_LU WHERE sector_LU_NIPP_id=@sectorID ORDER BY subsector", conn)
            cmd.Parameters.AddWithValue("@sectorID", sectorId)
            conn.Open()
            ddlSubsector.DataSource = cmd.ExecuteReader()
            ddlSubsector.DataValueField = "ID"
            ddlSubsector.DataTextField = "subsector"
            ddlSubsector.DataBind()
            ddlSubsector.Items.Insert(0, New ListItem("-- Select Subsector --", ""))
        End Using
    End Sub

    Private Sub LoadFacility(facilityId As Integer)
        Using conn As New SqlConnection(connString)
            Dim cmd = New SqlCommand("SELECT * FROM Facility WHERE ID = @ID", conn)
            cmd.Parameters.AddWithValue("@ID", facilityId)
            conn.Open()
            Dim reader = cmd.ExecuteReader()
            If reader.Read() Then
                txtFacilityName.Text = reader("full_name").ToString()
                ddlSector.SelectedValue = reader("sector_LU_id").ToString()
                ddlSector_SelectedIndexChanged(Nothing, Nothing)
                ddlSubsector.SelectedValue = reader("subsector_LU_id").ToString()
                ddlFacilityType.SelectedValue = reader("facility_type_LU_id").ToString()
                chkNetwork.Checked = Convert.ToBoolean(reader("cyber_threat"))

                txtPOCName.Text = reader("poc_name").ToString()
                txtPOCPhone.Text = reader("poc_phone").ToString()
                txtPOCEmail.Text = reader("poc_email").ToString()

                txtLatitude.Text = reader("lat").ToString()
                txtLongitude.Text = reader("lon").ToString()
                txtAddress1.Text = reader("address1").ToString()
                txtAddress2.Text = reader("address2").ToString()
                txtZip.Text = reader("zip").ToString()
                txtCity.Text = reader("city").ToString()
                txtState.Text = reader("state").ToString()

                ddlCounty.SelectedValue = reader("county_lu_id").ToString()
            Else
                Response.Redirect("FacilityManagement.aspx")
            End If
        End Using
    End Sub

    Protected Sub btnSaveChanges_Click(sender As Object, e As EventArgs)
        Dim facilityId = CInt(Request.QueryString("id"))

        Using conn As New SqlConnection(connString)
            Dim cmd = New SqlCommand("
                UPDATE Facility SET
                    full_name = @full_name,
                    sector_LU_id = @sector_LU_id,
                    subsector_LU_id = @subsector_LU_id,
                    facility_type_LU_id = @facility_type_LU_id,
                    cyber_threat = @cyber_threat,
                    poc_name = @poc_name,
                    poc_phone = @poc_phone,
                    poc_email = @poc_email,
                    lat = @lat,
                    lon = @lon,
                    address1 = @address1,
                    address2 = @address2,
                    county_lu_id = @county_lu_id,
                    zip = @zip,
                    city = @city,
                    state = @state
                WHERE ID = @ID", conn)

            With cmd.Parameters
                .AddWithValue("@full_name", txtFacilityName.Text)
                .AddWithValue("@sector_LU_id", ddlSector.SelectedValue)
                .AddWithValue("@subsector_LU_id", ddlSubsector.SelectedValue)
                .AddWithValue("@facility_type_LU_id", ddlFacilityType.SelectedValue)
                .AddWithValue("@cyber_threat", chkNetwork.Checked)
                .AddWithValue("@poc_name", txtPOCName.Text)
                .AddWithValue("@poc_phone", txtPOCPhone.Text)
                .AddWithValue("@poc_email", txtPOCEmail.Text)
                .AddWithValue("@lat", txtLatitude.Text)
                .AddWithValue("@lon", txtLongitude.Text)
                .AddWithValue("@address1", txtAddress1.Text)
                .AddWithValue("@address2", txtAddress2.Text)
                .AddWithValue("@county_lu_id", ddlCounty.SelectedValue)
                .AddWithValue("@zip", txtZip.Text)
                .AddWithValue("@city", txtCity.Text)
                .AddWithValue("@state", txtState.Text)
                .AddWithValue("@ID", facilityId)
            End With

            conn.Open()
            cmd.ExecuteNonQuery()
        End Using

        Response.Redirect("FacilityManagement.aspx")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("FacilityManagement.aspx")
    End Sub

End Class
