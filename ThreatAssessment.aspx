<%@ Page Title="External Threat/Hazard Assessment" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ThreatHazardForm.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.ThreatHazardForm" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .hazard-table th, .hazard-table td {
            vertical-align: middle;
        }
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h2 class="mb-4">External Threat/Hazard Assessment</h2>

        <asp:Literal runat="server" ID="litAssessmentInfo" />

        <asp:GridView ID="gvHazards" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered hazard-table" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField HeaderText="Subhazard" DataField="Subhazard" />
                <asp:TemplateField HeaderText="Rating">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtRating" CssClass="form-control" Text='<%# Bind("Rating") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Applicable?">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkApplicable" Checked='<%# Eval("Answer") = 1 %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Description" DataField="Description" />
            </Columns>
        </asp:GridView>

        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save Assessment" OnClick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary ml-2" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" />
    </div>
</asp:Content>