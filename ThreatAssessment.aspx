<%@ Page Title="Threat Assessment" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="ThreatAssessment.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.ThreatAssessment" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h2 class="mb-3">Threat Assessment</h2>

        <!-- Assessment Info -->
        <div class="mb-3">
            <strong>Assessment #:</strong> <asp:Literal ID="litAssessmentID" runat="server" /> <br />
            <strong>Facility:</strong> <asp:Literal ID="litFacility" runat="server" /> <br />
            <strong>Assessor:</strong> <asp:Literal ID="litAssessor" runat="server" /> <br />
            <strong>Phone/Email:</strong> <asp:Literal ID="litContact" runat="server" /> <br />
            <strong>Assessment Started:</strong> <asp:Literal ID="litStartDate" runat="server" />
        </div>

        <h4 class="bg-warning p-2">External Threat: <asp:Literal ID="litSubhazard" runat="server" /></h4>

              <!-- Side-by-side layout -->
        <div class="row mt-4">
            <!-- Left side: question and options -->
            <div class="col-md-7">
                <h5 class="mb-2">Which of the following best describes the facility being evaluated?</h5>
                <p><strong>(See User Guide for additional information on how to obtain MSA threat level information.)</strong></p>

                <asp:RadioButtonList ID="rblRatings" runat="server" CssClass="mb-3" RepeatLayout="Flow" />

                <asp:Literal ID="litExtraInfo" runat="server" />

                <div class="mt-3 small text-muted">
                    ("N/A" if you do not know. The information required to answer this question can be acquired via a State of Illinois official information request to DHS I&A)
                </div>
            </div>

            <!-- Right side: image -->
            <div class="col-md-5 text-center">
                <img src="images/terror.jpg" alt="Terrorism" class="img-fluid rounded shadow" style="max-height: 300px;" />
            </div>
        </div>

        <div class="mt-4 text-end">
            <asp:Button ID="btnNext" runat="server" Text="Next &gt;&gt;" CssClass="btn btn-primary" OnClick="btnNext_Click" />
        </div>

        <!-- Hidden fields -->
        <asp:HiddenField ID="hfAssessmentID" runat="server" />
        <asp:HiddenField ID="hfSubhazardID" runat="server" />
    </div>
</asp:Content>
