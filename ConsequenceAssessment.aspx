<%@ Page Title="Consequence Assessment" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="ConsequenceAssessment.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.ConsequenceAssessment" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container py-4">
    <h2 class="mb-3">Consequence Assessment</h2>

    <!-- Assessment Info -->
    <div class="mb-3">
        <strong>Assessment #:</strong> <asp:Literal ID="litAssessmentID" runat="server" /><br />
        <strong>Facility:</strong> <asp:Literal ID="litFacility" runat="server" /><br />
        <strong>Assessor:</strong> <asp:Literal ID="litAssessor" runat="server" /><br />
        <strong>Phone/Email:</strong> <asp:Literal ID="litContact" runat="server" /><br />
        <strong>Assessment Started:</strong> <asp:Literal ID="litStartDate" runat="server" />
    </div>

    <!-- Consequence Matrix -->
    <asp:Repeater ID="rptConsequence" runat="server">
        <HeaderTemplate>
            <table class="table table-bordered align-middle text-center">
                <thead>
                    <tr>
                        <th rowspan="2" class="align-middle text-start">Threat</th>
                        <th colspan="9">Impact Categories</th>
                    </tr>
                    <tr>
                        <%-- Impact Names with tooltips will go here --%>
                        <%# GetImpactHeaders() %>
                    </tr>
                    <tr>
                        <td class="text-start"><strong>Bulk Entry</strong></td>
                        <%-- Bulk input fields --%>
                        <%# GetBulkInputs() %>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>

        <ItemTemplate>
            <tr>
                <td class="text-start"><%# Eval("ThreatName") %></td>
                <%-- 9 columns of inputs --%>
                <%# GetConsequenceInputs(Container.DataItem) %>
            </tr>
        </ItemTemplate>

        <FooterTemplate>
                </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    <div class="mt-4 text-end">
        <asp:Button ID="btnSubmitConsequence" runat="server" Text="Submit All" CssClass="btn btn-primary" OnClick="btnSubmitConsequence_Click" />
    </div>

    <!-- Hidden Fields -->
    <asp:HiddenField ID="hfAssessmentID" runat="server" />
</div>

<!-- Scripts -->
<script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
<script>
$(document).ready(function() {
    $('.bulk-input').on('change', function() {
        var colIndex = $(this).closest('th').index();
        var bulkValue = $(this).val();

        if (bulkValue !== "") {
            $('tbody tr').each(function() {
                var $cell = $(this).find('td').eq(colIndex);
                var $input = $cell.find('input[type="text"]');
                if ($input.length > 0 && $input.val() === "") {
                    $input.val(bulkValue);
                }
            });
        }
    });

    $('[data-toggle="tooltip"]').tooltip();
});
</script>
</asp:Content>
