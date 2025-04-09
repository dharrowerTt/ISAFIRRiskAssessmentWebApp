<%@ Page Title="Role Function Management" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoleFunctionManagement.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.RoleFunctionManagement" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.21/css/jquery.dataTables.min.css" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h1>Role Function Management</h1>

        <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-select w-auto mb-3" AutoPostBack="true" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" />

        <asp:GridView ID="gvPermissions" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" GridLines="None">
            <Columns>
                <asp:BoundField DataField="PermissionName" HeaderText="Permission" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:TemplateField HeaderText="Allowed">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkAllowed" runat="server" Checked='<%# Eval("IsAllowed") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary mt-3" Text="Save Changes" OnClick="btnSave_Click" />
    </div>

    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.21/js/jquery.dataTables.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#<%= gvPermissions.ClientID %>").DataTable();
        });
    </script>
</asp:Content>
