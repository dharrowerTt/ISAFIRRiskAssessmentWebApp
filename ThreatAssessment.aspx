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
                    <table class="table table-bordered align-middle">
                        <thead>
                            <tr>
                                <th style="width: 30%;">Threat</th>
                                <th style="width: 50%;">Description</th>
                                <th style="width: 20%;">Rating</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("Heading") %>
                            <asp:HiddenField ID="hfQuestionID" runat="server" Value='<%# Eval("QuestionID") %>' />
                        </td>
                        <td>
                            <%# Eval("Content") %>
                            <% If Not String.IsNullOrEmpty(Eval("HelpText")) Then %>
                                <button type="button" class="btn btn-sm btn-outline-info ms-2" title='<%# Eval("HelpText") %>'>
                                    ?
                                </button>
                            <% End If %>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblMatrixOptions" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" />
                        </td>
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
</div>

</asp:Content>
