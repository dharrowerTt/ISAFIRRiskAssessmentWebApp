<%@ Page Language="VB" AutoEventWireup="true" CodeFile="ClaimsViewer.aspx.vb" Inherits="ClaimsViewer" %>

<!DOCTYPE html>
<html>
<head><title>Claims Viewer</title></head>
<body>
    <h2>Claims for @User.Identity.Name</h2>
    <ul>
        <% For Each claim In User.Identity.Claims
               Response.Write("<li>" & claim.Type & " = " & claim.Value & "</li>")
           Next
        %>
    </ul>
</body>
</html>
