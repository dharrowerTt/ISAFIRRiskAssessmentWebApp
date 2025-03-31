<%@ Page Title="Simulate Role" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/Site.Master" CodeBehind="TesterSimulate.aspx.vb"
    Inherits="ISAFIRRiskAssessmentWebApp.TesterSimulate" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container py-4">
    <h2>Simulate Role</h2>
    <p class="text-muted">As a tester, you may simulate the experience of another role below. This will override your session role until you return to this page or log out.</p>

    <div class="card my-4">
        <div class="card-body">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlSimulatedRole">Select Role to Simulate</asp:Label>
                <asp:DropDownList runat="server" ID="ddlSimulatedRole" CssClass="form-control">
                    <asp:ListItem Text="-- Select Role --" Value="" />
                    <asp:ListItem Text="Admin" Value="Admin" />
                    <asp:ListItem Text="Facility Manager" Value="FacilityManager" />
                    <asp:ListItem Text="Assessor" Value="Assessor" />
                    <asp:ListItem Text="Supervisor" Value="Supervisor" />
                    <asp:ListItem Text="Viewer" Value="Viewer" />
                </asp:DropDownList>
            </div>

            <asp:Button runat="server" ID="btnSimulate" Text="Simulate Role" CssClass="btn btn-primary" OnClick="btnSimulate_Click" />
        </div>
    </div>

    <asp:Label runat="server" ID="lblStatus" CssClass="text-success font-weight-bold" Visible="false" />
</div>
</asp:Content>
