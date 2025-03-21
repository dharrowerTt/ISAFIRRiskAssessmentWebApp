<%@ Page Title="Home Dashboard" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <!-- Dashboard Header -->
        <div class="row mb-4">
            <div class="col">
                <h1 class="mt-4">ISAFIR Risk Assessment Dashboard</h1>
                <p class="lead">Welcome to the ISAFIR Risk Assessment Web Application. Use the dashboard below to view key metrics and navigate to main modules.</p>
            </div>
        </div>
        <!-- Summary Metrics -->
        <div class="row">
            <div class="col-md-4">
                <div class="card text-white bg-primary mb-3">
                    <div class="card-header">Total Facilities</div>
                    <div class="card-body">
                        <h5 class="card-title">25</h5>
                        <p class="card-text">Facilities currently registered in the system.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-white bg-success mb-3">
                    <div class="card-header">Average Risk Score</div>
                    <div class="card-body">
                        <h5 class="card-title">75</h5>
                        <p class="card-text">Average risk score across all facilities.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-white bg-warning mb-3">
                    <div class="card-header">Pending Assessments</div>
                    <div class="card-body">
                        <h5 class="card-title">5</h5>
                        <p class="card-text">Assessments that require completion or review.</p>
                    </div>
                </div>
            </div>
        </div>
        <!-- Navigation Buttons -->
        <div class="row">
            <div class="col text-center">
                <a href="FacilityManagement.aspx" class="btn btn-lg btn-info m-2">Manage Facilities</a>
                <a href="ThreatAssessment.aspx" class="btn btn-lg btn-secondary m-2">Start New Assessment</a>
            </div>
        </div>
    </div>
</asp:Content>
