<%@ Page Title="Threat Assessment" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="ThreatAssessment.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.ThreatAssessment" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container py-4">
    <h2 class="mb-3">Threat Assessment</h2>

    <!-- Assessment Info (shared for both views) -->
    <div class="mb-3">
        <strong>Assessment #:</strong> <asp:Literal ID="litAssessmentID" runat="server" /><br />
        <strong>Facility:</strong> <asp:Literal ID="litFacility" runat="server" /><br />
        <strong>Assessor:</strong> <asp:Literal ID="litAssessor" runat="server" /><br />
        <strong>Phone/Email:</strong> <asp:Literal ID="litContact" runat="server" /><br />
        <strong>Assessment Started:</strong> <asp:Literal ID="litStartDate" runat="server" />
    </div>

    <asp:MultiView ID="mvThreatView" runat="server">
        <!-- View 1: Single question -->
        <asp:View ID="ViewSingle" runat="server">
            <h4 class="bg-warning p-2">External Threat: <asp:Literal ID="litSubhazard" runat="server" /></h4>

            <div class="row mt-4">
                <div class="col-md-7">
                    <h5 class="mb-2">Which of the following best describes the facility being evaluated?</h5>
                    <p><strong>(See User Guide for additional information on how to obtain MSA threat level information.)</strong></p>

                    <asp:RadioButtonList ID="rblRatings" runat="server" CssClass="mb-3" RepeatLayout="Flow" />
                    <asp:Literal ID="litExtraInfo" runat="server" />

                    <div class="mt-3 small text-muted">
                        ("N/A" if you do not know. The information required to answer this question can be acquired via a State of Illinois official information request to DHS I&A)
                    </div>
                </div>

                <div class="col-md-5 text-center">
                    <img src="images/terror.jpg" alt="Terrorism" class="img-fluid rounded shadow" style="max-height: 300px;" />
                </div>
            </div>

            <div class="mt-4 text-end">
                <asp:Button ID="btnNext" runat="server" Text="Next &gt;&gt;" CssClass="btn btn-primary" OnClick="btnNext_Click" />
            </div>
        </asp:View>

        <!-- View 2: Matrix of internal threats -->
        <asp:View ID="ViewMatrix" runat="server">
            <h4 class="bg-warning p-2">Internal Threat Matrix</h4>

<asp:Repeater ID="rptMatrix" runat="server" OnItemDataBound="rptMatrix_ItemDataBound">
    <HeaderTemplate>
        <table class="table table-bordered align-middle text-center">
            <thead>
                <tr>
                    <th rowspan="2" class="align-middle text-start">Threat</th>
                    <th colspan="5">Rating</th>
                </tr>
                <tr>
                    <th>Rare<br /><small>(>20 years)</small></th>
                    <th>Every<br /> 20 yrs</th>
                    <th>Every<br /> 5 yrs</th>
                    <th>Yearly</th>
                    <th>N/A</th>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>

<ItemTemplate>
<tr>
<td class="text-start">
    <%# Eval("Heading") %>
    <%# If(
        Not String.IsNullOrEmpty(Eval("HelpText").ToString()),
        "<svg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='currentColor' class='ms-2 text-primary' role='button' tabindex='0' data-toggle='tooltip' data-placement='right' title='" & Eval("HelpText") & "'>" &
        "<path d='M8 15A7 7 0 1 0 8 1a7 7 0 0 0 0 14zm0 1A8 8 0 1 1 8 0a8 8 0 0 1 0 16z'/>" &
        "<path d='m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.003-.252 1.25-.598l.088-.416c-.287.346-.52.527-.836.527-.275 0-.375-.193-.303-.54l.738-3.468c.194-.897-.105-1.319-.808-1.319z'/>" &
        "<circle cx='8' cy='4.5' r='1'/>" &
        "</svg>",
        ""
    ) %>
</td>






    <td class="text-center"><asp:RadioButton ID="rbRare" GroupName='<%# Eval("QuestionID") %>' runat="server" Value="Rare" /></td>
    <td class="text-center"><asp:RadioButton ID="rbEvery20" GroupName='<%# Eval("QuestionID") %>' runat="server" Value="Every20" /></td>
    <td class="text-center"><asp:RadioButton ID="rbEvery5" GroupName='<%# Eval("QuestionID") %>' runat="server" Value="Every5" /></td>
    <td class="text-center"><asp:RadioButton ID="rbYearly" GroupName='<%# Eval("QuestionID") %>' runat="server" Value="Yearly" /></td>
    <td class="text-center"><asp:RadioButton ID="rbNA" GroupName='<%# Eval("QuestionID") %>' runat="server" Value="NA" /></td>
</tr>
</ItemTemplate>




    <FooterTemplate>
            </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>


            <div class="mt-4 text-end">
                <asp:Button ID="btnMatrixSubmit" runat="server" Text="Submit All" CssClass="btn btn-primary" OnClick="btnMatrixSubmit_Click" />
            </div>
        </asp:View>
    </asp:MultiView>

    <!-- Hidden Fields -->
    <asp:HiddenField ID="hfAssessmentID" runat="server" />
    <asp:HiddenField ID="hfSubhazardID" runat="server" />

    <script>
        $(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>


</div>

</asp:Content>
