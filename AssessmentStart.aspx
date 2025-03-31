<%@ Page Title="Start New Assessment" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/Site.Master" CodeBehind="AssessmentStart.aspx.vb"
    Inherits="ISAFIRRiskAssessmentWebApp.AssessmentStart" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h2 class="mb-4">Start New Assessment</h2>

        <asp:ValidationSummary runat="server" CssClass="text-danger" />

        <div class="card p-4 shadow-sm">
            <div class="form-group mb-3">
                <asp:Label runat="server" AssociatedControlID="ddlFacility">Select Facility</asp:Label>
                <asp:DropDownList ID="ddlFacility" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                    <asp:ListItem Text="-- Select --" Value="" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFacility" 
                    InitialValue="" CssClass="text-danger" ErrorMessage="Please select a facility." />
            </div>

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label runat="server" AssociatedControlID="txtAssessorFirst">First Name</asp:Label>
                    <asp:TextBox ID="txtAssessorFirst" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group col-md-4">
                    <asp:Label runat="server" AssociatedControlID="txtAssessorLast">Last Name</asp:Label>
                    <asp:TextBox ID="txtAssessorLast" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group col-md-4">
                    <asp:Label runat="server" AssociatedControlID="txtAssessor">Username</asp:Label>
                    <asp:TextBox ID="txtAssessor" runat="server" CssClass="form-control" />
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label runat="server" AssociatedControlID="txtEmail">Email</asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <asp:Label runat="server" AssociatedControlID="txtPhone">Phone</asp:Label>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
                </div>
            </div>

            <asp:Button ID="btnStart" runat="server" Text="Begin Assessment"
                CssClass="btn btn-primary mt-3" OnClick="btnStart_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False"
                CssClass="btn btn-secondary mt-3 ml-2" OnClick="btnCancel_Click" />
        </div>
    </div>
</asp:Content>
