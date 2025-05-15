<%@ Page Title="Edit User Role" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRoleEdit.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.UserRoleEdit" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h1 class="mb-4">Edit User Role</h1>

        <asp:Panel ID="pnlEdit" runat="server" CssClass="card p-4 border border-2 rounded shadow-sm" Visible="false">
            <div class="mb-3">
                <label for="txtFullName" class="form-label">Full Name</label>
                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" ReadOnly="True" />
            </div>
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ReadOnly="True" />
            </div>
            <div class="mb-3">
                <label for="ddlRole" class="form-label">Role</label>
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select" AppendDataBoundItems="true" />
            </div>
            <div class="mb-3">
                <label for="txtState" class="form-label">State</label>
                <asp:TextBox ID="txtState" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtFacility" class="form-label">Facility ID</label>
                <asp:TextBox ID="txtFacility" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary me-2" Text="Save Changes" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="Cancel" OnClick="btnCancel_Click" />
        </asp:Panel>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3 d-block" Visible="false" />
    </div>
</asp:Content>
