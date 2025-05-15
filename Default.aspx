<%@ Page Title="Home Dashboard" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="mt-4">ISAFIR Risk Assessment Dashboard</h1>
        <p class="lead">Welcome to the I-SAFIR Risk Assessment Web Application. Your dashboard reflects the tools and data you have access to.</p>

        <!-- Summary Metrics (Admin only) -->
        <asp:Panel ID="pnlMetrics" runat="server" Visible="false">
            <div class="row mb-4">
                <div class="col-md-4">
                    <div class="card text-white bg-primary mb-3">
                        <div class="card-header">Total Facilities</div>
                        <div class="card-body">
                            <h5 class="card-title"><asp:Label ID="lblTotalFacilities" runat="server" Text="..." /></h5>
                            <p class="card-text">Registered facilities in the system.</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card text-white bg-success mb-3">
                        <div class="card-header">Pending Assessments</div>
                        <div class="card-body">
                            <h5 class="card-title"><asp:Label ID="lblPendingAssessments" runat="server" Text="..." /></h5>
                            <p class="card-text">Assessments awaiting completion or review.</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card text-white bg-secondary mb-3">
                        <div class="card-header">User Count</div>
                        <div class="card-body">
                            <h5 class="card-title"><asp:Label ID="lblUserCount" runat="server" Text="..." /></h5>
                            <p class="card-text">Registered users in the system.</p>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Navigation Buttons -->
        <div class="row text-center mb-5">
            <asp:Panel ID="pnlManageFacilities" runat="server" Visible="false">
                <a href="FacilityManagement.aspx" class="btn btn-lg btn-info m-2">Manage Facilities</a>
            </asp:Panel>
            <asp:Panel ID="pnlStartAssessment" runat="server" Visible="false">
                <a href="AssessmentStart.aspx" class="btn btn-lg btn-secondary m-2">Start New Assessment</a>
            </asp:Panel>
            <asp:Panel ID="pnlUserManagement" runat="server" Visible="false">
                <a href="UserRoleManagement.aspx" class="btn btn-lg btn-outline-dark m-2">Manage User Roles</a>
            </asp:Panel>
            <asp:Panel ID="pnlReports" runat="server" Visible="false">
                <a href="AssessmentDashboard.aspx" class="btn btn-lg btn-outline-success m-2">View Assessment Reports</a>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
