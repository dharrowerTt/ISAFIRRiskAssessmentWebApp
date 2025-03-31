<%@ Page Title="Complete Assessment" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/Site.Master" CodeBehind="AssessmentComplete.aspx.vb"
    Inherits="ISAFIRRiskAssessmentWebApp.AssessmentComplete" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h2 class="mb-4">Complete Assessment</h2>

        <asp:Panel ID="pnlSummary" runat="server" CssClass="card p-4 shadow-sm">
            <dl class="row">
                <dt class="col-sm-3">Facility</dt>
                <dd class="col-sm-9"><asp:Label ID="lblFacility" runat="server" /></dd>

                <dt class="col-sm-3">Assessor</dt>
                <dd class="col-sm-9"><asp:Label ID="lblAssessor" runat="server" /></dd>

                <dt class="col-sm-3">Start Time</dt>
                <dd class="col-sm-9"><asp:Label ID="lblStart" runat="server" /></dd>
            </dl>
        </asp:Panel>

        <div class="mt-4">
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Confirm & Submit Assessment"
                OnClick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary ml-2" Text="Cancel"
                OnClick="btnCancel_Click" CausesValidation="false" />
        </div>
    </div>
</asp:Content>
