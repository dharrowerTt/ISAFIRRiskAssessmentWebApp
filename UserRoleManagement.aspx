<%@ Page Title="User Role Management" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/Site.Master" CodeBehind="UserRoleManagement.aspx.vb"
    Inherits="ISAFIRRiskAssessmentWebApp.UserRoleManagement" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
  <!-- Any custom JS or CSS specific to this page could go here -->
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container py-4">
    <h2>User Role Management</h2>

    <!-- User Selection -->
    <div class="card my-4">
        <div class="card-header"><strong>Select User</strong></div>
        <div class="card-body">
            <div class="form-group">
                <asp:Label runat="server">User Account</asp:Label>
                <asp:DropDownList runat="server" ID="ddlUserAccounts" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUserAccounts_SelectedIndexChanged"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlUserAccounts" InitialValue="" CssClass="text-danger"
                    ErrorMessage="Selecting a user is required." />
            </div>
        </div>
    </div>

    <!-- Role Assignment -->
    <div class="card my-4">
        <div class="card-header"><strong>Assign Role and Permissions</strong></div>
        <div class="card-body">
            <div class="form-group">
                <asp:Label runat="server">Role</asp:Label>
                <asp:DropDownList runat="server" ID="ddlRoles" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                    <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                    <asp:ListItem Text="Tester" Value="Tester"></asp:ListItem>
                    <asp:ListItem Text="Facility Manager" Value="FacilityManager"></asp:ListItem>
                    <asp:ListItem Text="Assessor" Value="Assessor"></asp:ListItem>
                    <asp:ListItem Text="Supervisor" Value="Supervisor"></asp:ListItem>
                    <asp:ListItem Text="Viewer" Value="Viewer"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRoles" InitialValue="" CssClass="text-danger"
                    ErrorMessage="Role selection is required." />
            </div>

            <!-- Dynamic permissions checkboxes -->
            <asp:Panel ID="pnlPermissions" runat="server" Visible="false" CssClass="border p-3 mt-4">
                <strong>Permissions:</strong>
                <asp:CheckBoxList runat="server" ID="cblPermissions" CssClass="form-check mt-2">
                    <!-- Items dynamically loaded from CodeBehind based on role selection -->
                </asp:CheckBoxList>
            </asp:Panel>

            <!-- Facility assignment if applicable -->
            <asp:Panel ID="pnlFacilityAssignment" runat="server" Visible="false" CssClass="border p-3 mt-4">
                <strong>Assign Facilities:</strong>
                <asp:CheckBoxList runat="server" ID="cblFacilityAssignments" CssClass="form-check mt-2">
                    <!-- Facility names dynamically loaded from CodeBehind -->
                </asp:CheckBoxList>
            </asp:Panel>
        </div>
    </div>

    <!-- Action Buttons -->
    <asp:Button runat="server" ID="btnSaveRole" CssClass="btn btn-primary" Text="Save Changes" OnClick="btnSaveRole_Click" />
    <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-secondary ml-2" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />

    <!-- Audit Trail -->
    <div class="card my-5">
        <div class="card-header"><strong>Role Assignment Audit Trail</strong></div>
        <div class="card-body">
            <asp:GridView runat="server" ID="gvAuditTrail" CssClass="table table-bordered table-striped">
                <!-- Audit trail data bound from CodeBehind -->
            </asp:GridView>
        </div>
    </div>
</div>
</asp:Content>
