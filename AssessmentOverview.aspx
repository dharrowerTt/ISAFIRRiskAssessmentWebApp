<%@ Page Title="Assessment Overview" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="AssessmentOverview.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.AssessmentOverview" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container py-4">
    <h2>Assessment Overview</h2>

    <asp:Panel ID="pnlOverview" runat="server" CssClass="card p-3 mb-4 shadow-sm">
      <asp:Literal ID="litAssessorInfo" runat="server" />
    </asp:Panel>

    <div class="row" id="statusCards" runat="server">
      <div class="col-md-3">
        <asp:HyperLink ID="lnkThreat" runat="server" CssClass="card text-white text-center p-3" NavigateUrl="">
          <asp:Literal ID="litThreatStatus" runat="server" />
          <h5>Threat</h5>
        </asp:HyperLink>
      </div>
      <div class="col-md-3">
        <asp:HyperLink ID="lnkConsequence" runat="server" CssClass="card text-white text-center p-3" NavigateUrl="">
          <asp:Literal ID="litConsequenceStatus" runat="server" />
          <h5>Consequence</h5>
        </asp:HyperLink>
      </div>
      <div class="col-md-3">
        <asp:HyperLink ID="lnkLifelines" runat="server" CssClass="card text-white text-center p-3" NavigateUrl="">
          <asp:Literal ID="litLifelinesStatus" runat="server" />
          <h5>Life Lines</h5>
        </asp:HyperLink>
      </div>
      <div class="col-md-3">
        <asp:HyperLink ID="lnkVulnerability" runat="server" CssClass="card text-white text-center p-3" NavigateUrl="">
          <asp:Literal ID="litVulnerabilityStatus" runat="server" />
          <h5>Vulnerability</h5>
        </asp:HyperLink>
      </div>
    </div>
  </div>
</asp:Content>
