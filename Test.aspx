<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Test.aspx.vb" Inherits="Test" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Test Page</h1>
            <asp:Button ID="btnLogin" runat="server" Text="Login with Okta" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>
