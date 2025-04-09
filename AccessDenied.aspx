<%@ Page Title="Access Denied" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.AccessDenied" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        body {
            background-color: #1f1f2e !important;
            color: #EEEEEE;
        }

        .access-denied-container {
            max-width: 600px;
            margin: 100px auto;
            padding: 2rem;
            background-color: #2d2d3f;
            border: 2px solid #D4BEE4;
            border-radius: 1rem;
            text-align: center;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.5);
        }

        .access-denied-container h1 {
            color: #FF5E5E;
        }

        .access-denied-container p {
            color: #CCCCCC;
        }

        .access-denied-container a.btn {
            background-color: #3B1E54;
            color: #EEEEEE;
        }

        .access-denied-container a.btn:hover {
            background-color: #9B7EBD;
            color: #342334;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="access-denied-container">
        <h1><i class="fas fa-ban"></i> Access Denied</h1>
        <p class="lead">You do not have permission to view this page or perform this action.</p>
        <a href="Default.aspx" class="btn btn-lg mt-3">Return to Dashboard</a>
    </div>
</asp:Content>
