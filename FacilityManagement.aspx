<%@ Page Title="Facility Management" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FacilityManagement.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.FacilityManagement" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- DataTables CSS -->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.21/css/jquery.dataTables.min.css" />
    <!-- DataTables Buttons CSS -->
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.2/css/buttons.dataTables.min.css" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h1>Facility Management</h1>
        <!-- GridView to be enhanced by DataTables -->
        <asp:Button ID="btnAddFacility" runat="server" CssClass="btn btn-success mb-3" 
            Text="Add New Facility" OnClick="btnAddFacility_Click" />

<asp:GridView ID="gvFacilities" runat="server" AutoGenerateColumns="False" CssClass="display" GridLines="None" UseAccessibleHeader="True">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
        <asp:BoundField DataField="full_name" HeaderText="Facility Name" />
        <asp:BoundField DataField="address1" HeaderText="Address" />
        <asp:BoundField DataField="city" HeaderText="City" />
        <asp:BoundField DataField="state" HeaderText="State" />
        <asp:BoundField DataField="zip" HeaderText="Zip" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-sm btn-info" 
                                Text="Edit" 
                                CommandName="EditFacility" 
                                CommandArgument='<%# Eval("ID") %>'>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>




    </div>

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <!-- DataTables JS -->
    <script src="https://cdn.datatables.net/1.10.21/js/jquery.dataTables.min.js"></script>
    <!-- DataTables Buttons extension JS and dependencies -->
    <script src="https://cdn.datatables.net/buttons/1.6.2/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.6.2/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.6.2/js/buttons.print.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Initialize DataTables with export controls
            $("#<%= gvFacilities.ClientID %>").DataTable({
                dom: 'Bfrtip', // Layout: Buttons, filtering input, table, etc.
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>
</asp:Content>
