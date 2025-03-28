<%@ Page Title="Add Facility" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/Site.Master" CodeBehind="FacilityAdd.aspx.vb"
    Inherits="ISAFIRRiskAssessmentWebApp.FacilityAdd" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
  <script src="https://cdn.jsdelivr.net/npm/ol@v10.4.0/dist/ol.js"></script>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/ol@v10.4.0/ol.css">

</asp:Content>


<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h2>Add New Facility</h2>

    <!-- Step 3: Facility Details -->
    <div class="card my-4">
        <div class="card-header"><strong>Step 3: Facility Details</strong></div>
        <div class="card-body">
            <div class="form-group">
                <asp:Label runat="server">Facility Name</asp:Label>
                <asp:TextBox runat="server" ID="txtFacilityName" CssClass="form-control"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFacilityName" 
                    CssClass="text-danger" ErrorMessage="Facility name is required." />
            </div>

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label runat="server">Sector 
                        (<a href="https://www.cisa.gov/national-infrastructure-protection-plan" target="_blank">NIPP sectors</a>)
                    </asp:Label>
                    <asp:DropDownList runat="server" ID="ddlSector" CssClass="form-control" AutoPostBack="True" 
                        OnSelectedIndexChanged="ddlSector_SelectedIndexChanged"/>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSector" CssClass="text-danger" InitialValue=""
                        ErrorMessage="Sector is required." />
                </div>
                <div class="form-group col-md-4">
                    <asp:Label runat="server">Subsector</asp:Label>
                    <asp:DropDownList runat="server" ID="ddlSubsector" CssClass="form-control"/>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSubsector" CssClass="text-danger" InitialValue=""
                        ErrorMessage="Subsector is required." />
                </div>
                <div class="form-group col-md-4">
                    <asp:Label runat="server">Facility Type</asp:Label>
                    <asp:DropDownList runat="server" ID="ddlFacilityType" CssClass="form-control"/>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFacilityType" CssClass="text-danger" InitialValue=""
                        ErrorMessage="Facility type is required." />
                </div>
            </div>

            <div class="form-check">
                <asp:CheckBox runat="server" ID="chkNetwork" CssClass="form-check-input"/>
                <asp:Label runat="server" CssClass="form-check-label" AssociatedControlID="chkNetwork">
                    Facility is connected to external IT network
                </asp:Label>
            </div>
        </div>
    </div>

    <!-- Step 4: Point of Contact Information -->
    <div class="card my-4">
        <div class="card-header"><strong>Step 4: Facility Point of Contact</strong></div>
        <div class="card-body">
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label runat="server">Name</asp:Label>
                    <asp:TextBox runat="server" ID="txtPOCName" CssClass="form-control"/>
                </div>
                <div class="form-group col-md-4">
                    <asp:Label runat="server">Phone</asp:Label>
                    <asp:TextBox runat="server" ID="txtPOCPhone" CssClass="form-control"/>
                </div>
                <div class="form-group col-md-4">
                    <asp:Label runat="server">Email</asp:Label>
                    <asp:TextBox runat="server" ID="txtPOCEmail" CssClass="form-control"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Step 5: Facility Location Details -->
    <div class="card my-4">
        <div class="card-header"><strong>Step 5: Facility Location Details</strong></div>
        <div class="card-body">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label runat="server">Latitude</asp:Label>
                    <asp:TextBox runat="server" ID="txtLatitude" CssClass="form-control"/>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label runat="server">Longitude</asp:Label>
                    <asp:TextBox runat="server" ID="txtLongitude" CssClass="form-control"/>
                </div>
            </div>

             <div id="map" class="mb-3 border" style="width:100%; height:400px;"></div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label runat="server">Address 1</asp:Label>
                    <asp:TextBox runat="server" ID="txtAddress1" CssClass="form-control"/>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label runat="server">Address 2</asp:Label>
                    <asp:TextBox runat="server" ID="txtAddress2" CssClass="form-control"/>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label runat="server">County</asp:Label>
                    <asp:DropDownList runat="server" ID="ddlCounty" CssClass="form-control"/>
                </div>
                <div class="form-group col-md-2">
                    <asp:Label runat="server">Zip</asp:Label>
                    <asp:TextBox runat="server" ID="txtZip" CssClass="form-control"/>
                </div>
                <div class="form-group col-md-4">
                    <asp:Label runat="server">City</asp:Label>
                    <asp:TextBox runat="server" ID="txtCity" CssClass="form-control"/>
                </div>
                <div class="form-group col-md-2">
                    <asp:Label runat="server">State</asp:Label>
                    <asp:TextBox runat="server" ID="txtState" CssClass="form-control"/>
                </div>
            </div>
        </div>
    </div>

    <asp:Button runat="server" ID="btnBeginAssessment" CssClass="btn btn-primary" Text="Begin Assessment" OnClick="btnBeginAssessment_Click" />
    <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-secondary ml-2" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
</div>

  <!-- Explicitly wait until ol library is fully loaded -->
    <script>
        window.onload = function () {
            if (typeof ol === 'undefined') {
                alert('OpenLayers failed to load.');
                return;
            }

            var map = new ol.Map({
                target: 'map',
                layers: [new ol.layer.Tile({ source: new ol.source.OSM() })],
                view: new ol.View({ center: ol.proj.fromLonLat([-87.6359, 41.8789]), zoom: 12 })
            });

            map.on('click', function (evt) {
                var coord = ol.proj.toLonLat(evt.coordinate);
                document.getElementById('<%=txtLongitude.ClientID%>').value = coord[0].toFixed(6);
                document.getElementById('<%=txtLatitude.ClientID%>').value = coord[1].toFixed(6);
            });
        };
    </script>
</asp:Content>

