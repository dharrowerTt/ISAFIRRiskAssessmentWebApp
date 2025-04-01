<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Register.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register - I-SAFIR</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #EEEEEE;
            color: #342334;
        }

        .form-container {
            max-width: 500px;
            margin: 40px auto;
            border: 2px solid #D4BEE4;
            border-radius: 1rem;
            padding: 2rem;
            background-color: white;
        }

        .aspNet-Button, .btn-primary {
            background-color: #3B1E54;
            color: #EEEEEE;
            border: none;
        }

        .aspNet-Button:hover, .btn-primary:hover {
            background-color: #9B7EBD;
            color: #342334;
        }

        .CreateUserWizardText, .CreateUserWizardLabel {
            font-weight: bold;
        }

        a {
            color: #9B7EBD;
            font-weight: bold;
        }

        a:hover {
            color: #3B1E54;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container shadow">
            <h2 class="text-center mb-4" style="color: #3B1E54;">Register</h2>
<asp:Panel ID="RegisterPanel" runat="server">
    <div class="mb-3">
        <label for="txtFullName" class="form-label">Full Name</label>
        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
    </div>
    <div class="mb-3">
        <label for="txtDepartment" class="form-label">Department</label>
        <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" />
    </div>
    <div class="mb-3">
        <label for="ddlRole" class="form-label">Role</label>
        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select">
            <asp:ListItem Text="Viewer" Value="Viewer" />
            <asp:ListItem Text="Editor" Value="Editor" />
            <asp:ListItem Text="Admin" Value="Admin" />
        </asp:DropDownList>
    </div>
    <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-primary w-100" Text="Submit Registration" OnClick="btnRegister_Click" />
</asp:Panel>

        </div>
    </form>
</body>
</html>
