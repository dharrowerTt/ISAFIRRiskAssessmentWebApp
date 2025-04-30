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

    <div class="table-wrapper">
        <!-- Help Display -->
<div id="impactHelpBox" class="alert alert-secondary sticky-help">

    <strong>Impact Info:</strong>
    <div id="impactHelpTextWrapper" style="transition: max-height 0.3s ease, opacity 0.3s ease;">
        <div id="impactHelpText">Click a “?” button to view an explanation for that column.</div>
    </div>
</div>


    <!-- Consequence Matrix -->
    <asp:Repeater ID="rptConsequence" runat="server">
        <HeaderTemplate>
            <table id="consequenceTable" class="table table-bordered align-middle text-center">

                <thead>
                    <tr>
                        <th rowspan="3" class="align-middle text-start">Threat</th>
                       <th colspan="<%= ImpactCount %>">Impact Categories</th>

                    </tr>
                    <tr>
                        <%# GetImpactHeaders() %>
                    </tr>
                    <tr>
                        <%# GetHelperButtons() %>
                    </tr>
                    <tr class="bulk-row">
                        <td class="text-start"><strong>Bulk Entry</strong></td>
                        <%# GetBulkInputs() %>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>

        <ItemTemplate>
            <tr>
                <td class="text-start"><%# Eval("ThreatName") %></td>
                <%# GetConsequenceInputs(Container.DataItem) %>
            </tr>
        </ItemTemplate>

        <FooterTemplate>
                </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>
        </div>

    <div class="mt-4 text-end">
        <asp:Button ID="btnSubmitConsequence" runat="server" Text="Submit All" CssClass="btn btn-primary" OnClick="btnSubmitConsequence_Click" />
    </div>



    <!-- Hidden Fields -->
    <asp:HiddenField ID="hfAssessmentID" runat="server" />
</div>

<!-- Scripts and Styling -->
<script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
<script>
$(document).ready(function () {
    $('.bulk-input').on('input', function () {
        const colIndex = $(this).closest('th').index();
        const bulkValue = $(this).val();

        $('tbody tr').each(function () {
            const $cell = $(this).find('td').eq(colIndex);
            const $input = $cell.find('input[type="number"]');
            if ($input.length > 0) {
                $input.val(bulkValue);
            }
        });
    });


    $(document).on('input', '.consequence-input', function () {
        const val = $(this).val();
        if (!/^[0-3]?$/.test(val)) {
            $(this).val('');
        }
    });
});

    function showHelp(index) {
        const helpTextMap = <%= HelpTextJSArray %>;

        const helpText = helpTextMap[index - 1] || "No help text available.";

        // Animate helper box content change
        const $wrapper = $('#impactHelpTextWrapper');
        const $content = $('#impactHelpText');

        $wrapper.css('opacity', 0);
        setTimeout(() => {
            $content.html(helpText);
            $wrapper.css('opacity', 1);
        }, 150);

        // Remove previous highlights
        $('td, th').removeClass('highlighted-col');

        // ✅ Highlight inputs
        $('tbody tr').each(function () {
            $(this).find('td').eq(index).addClass('highlighted-col');
        });

        // ✅ Highlight header and ? button — subtract 1 for the "Threat" column
        $('thead tr').each(function (i) {
            if (i >= 1) {
                $(this).find('th').eq(index - 1).addClass('highlighted-col'); // <- FIXED
            }
        });
    }

    $(document).ready(function () {
        function syncHelperWidth() {
            const tableWidth = $('#consequenceTable').outerWidth();
            $('#impactHelpBox').css('width', tableWidth + 'px');
        }

        // Run on load
        syncHelperWidth();

        // Re-run if window resizes (e.g., responsive change)
        $(window).on('resize', function () {
            syncHelperWidth();
        });
    });





</script>

<style>
.bulk-row td,
.bulk-row th {
    background-color: #f5f5f5 !important;
    font-weight: bold;
}

.table-wrapper {
    position: relative;
}

.table-wrapper table {
    width: auto;
    margin: 0 auto;
}



.sticky-help {
    background-color: #e7f3fe;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    padding: 1rem;
    border-left: 4px solid #3399ff;
    position: sticky;
    top: 0;
    z-index: 10;
    margin: 0 auto 1rem auto;
}


    .highlighted-col {
        background-color: #e7f3fe !important;
        border: 2px solid #3399ff;
    }

    .impact-col {
        width: 120px;
        min-width: 120px;
        max-width: 120px;
    }



</style>
</asp:Content>
