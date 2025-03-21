<%@ Page Title="Facility Details" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FacilityDetails.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.FacilityDetails" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Optionally include additional CSS specific to this page -->
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h1>Facility Details</h1>
        <asp:Panel ID="pnlForm" runat="server" CssClass="mt-4">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <div class="form-group">
                <asp:Label ID="lblFullName" runat="server" Text="Facility Name:" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblAddress1" runat="server" Text="Address:" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblCity" runat="server" Text="City:" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblState" runat="server" Text="State:" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="txtState" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblZip" runat="server" Text="Zip:" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="txtZip" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary ml-2" OnClick="btnCancel_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
