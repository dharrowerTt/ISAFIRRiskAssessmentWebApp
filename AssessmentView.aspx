<%@ Page Title="Assessment Details" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/Site.Master" CodeBehind="AssessmentView.aspx.vb"
    Inherits="ISAFIRRiskAssessmentWebApp.AssessmentView" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h2 class="mb-4">Assessment Details</h2>

        <asp:Panel ID="pnlDetails" runat="server" CssClass="card p-4 shadow-sm">
            <dl class="row">
                <dt class="col-sm-3">Facility</dt>
                <dd class="col-sm-9"><asp:Label ID="lblFacilityName" runat="server" /></dd>

                <dt class="col-sm-3">Assessor</dt>
                <dd class="col-sm-9"><asp:Label ID="lblAssessor" runat="server" /></dd>

                <dt class="col-sm-3">Email</dt>
                <dd class="col-sm-9"><asp:Label ID="lblEmail" runat="server" /></dd>

                <dt class="col-sm-3">Phone</dt>
                <dd class="col-sm-9"><asp:Label ID="lblPhone" runat="server" /></dd>

                <dt class="col-sm-3">Start Date</dt>
                <dd class="col-sm-9"><asp:Label ID="lblStart" runat="server" /></dd>

                <dt class="col-sm-3">End Date</dt>
                <dd class="col-sm-9"><asp:Label ID="lblEnd" runat="server" /></dd>

                <dt class="col-sm-3">Current</dt>
                <dd class="col-sm-9"><asp:Label ID="lblCurrent" runat="server" /></dd>
            </dl>
        </asp:Panel>

        <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard"
            CssClass="btn btn-secondary mt-4" OnClick="btnBack_Click" />
    </div>
</asp:Content>
