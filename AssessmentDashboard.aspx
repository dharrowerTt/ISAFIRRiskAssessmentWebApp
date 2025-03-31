<%@ Page Title="Assessment Dashboard" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/Site.Master" CodeBehind="AssessmentDashboard.aspx.vb"
    Inherits="ISAFIRRiskAssessmentWebApp.AssessmentDashboard" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap5.min.css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script runat="server">
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not IsPostBack Then
                Dim status As String = Request.QueryString("status")
                If status = "completed" Then
                    Dim toastScript As String = "
                        Swal.fire({
                            toast: true,
                            position: 'top-end',
                            icon: 'success',
                            title: 'Assessment marked complete',
                            showConfirmButton: false,
                            timer: 3000,
                            timerProgressBar: true,
                            background: '#EEEEEE',
                            color: '#3B1E54',
                            iconColor: '#3B1E54'
                        });"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "showToast", toastScript, True)
                End If
            End If
        End Sub
    </script>
    <div class="container py-4">
        <h2 class="mb-4">Assessment Dashboard</h2>

        <asp:Button ID="btnNewAssessment" runat="server" Text="Create New Assessment"
            CssClass="btn btn-success mb-3" OnClick="btnNewAssessment_Click" />

        <asp:GridView ID="gvAssessments" runat="server" CssClass="table table-striped display"
            AutoGenerateColumns="False" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="FacilityName" HeaderText="Facility" />
                <asp:BoundField DataField="Assessor" HeaderText="Assessor" />
                <asp:BoundField DataField="AssessmentStart" HeaderText="Start Date" DataFormatString="{0:g}" />
                <asp:BoundField DataField="AssessmentEnd" HeaderText="End Date" DataFormatString="{0:g}" />
                <asp:BoundField DataField="Current" HeaderText="Current" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkView" runat="server"
                            NavigateUrl='<%# Eval("ID", "AssessmentView.aspx?id={0}") %>'
                            Text="View" CssClass="btn btn-sm btn-primary" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/dataTables.bootstrap5.min.js"></script>
    <script>
        $(document).ready(function () {
            $('.display').DataTable({
                pageLength: 10,
                lengthChange: false
            });
        });
    </script>
</asp:Content>
