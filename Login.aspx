<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - I-SAFIR</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #EEEEEE;
            color: #342334;
        }

        .form-container {
            max-width: 400px;
            margin: 60px auto;
            padding: 2rem;
            border: 2px solid #D4BEE4;
            border-radius: 1rem;
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
            <h2 class="text-center mb-4" style="color: #3B1E54;">Login</h2>
            <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Default.aspx" CssClass="w-100" DisplayRememberMe="true">
            </asp:Login>
        </div>
    </form>
</body>
</html>
