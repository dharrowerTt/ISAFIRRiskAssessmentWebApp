<%@ Page Title="Lifeline Assessment" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/Site.Master"
    CodeBehind="LifelineAssessment.aspx.vb"
    Inherits="ISAFIRRiskAssessmentWebApp.LifelineAssessment" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container py-4">

    <h2 class="mb-3">Lifeline Assessment</h2>

    <uc:AssessmentProgress ID="ucProgress" runat="server" CurrentStep="lifeline" />

    <!-- Assessment Info -->
    <div class="mb-3">
        <strong>Assessment #:</strong> <asp:Literal ID="litAssessmentID" runat="server" /><br />
        <strong>Facility:</strong> <asp:Literal ID="litFacility" runat="server" /><br />
        <strong>Assessor:</strong> <asp:Literal ID="litAssessor" runat="server" /><br />
        <strong>Phone/Email:</strong> <asp:Literal ID="litContact" runat="server" /><br />
        <strong>Assessment Started:</strong> <asp:Literal ID="litStartDate" runat="server" />
    </div>
<!-- Lifeline Grid Start -->

<div class="lifeline-grid">

    <!-- Top-Level Lifelines -->
    <div class="top-lifelines">
        <% For Each item As DataRow In TopLevelLifelines.Rows %>
            <div class="lifeline-tile top" data-id="<%= item("ID") %>" data-label="<%= item("LifelineLabel") %>">
                <img src="<%= item("IconPath") %>" alt="" class="lifeline-icon" />
            </div>
        <% Next %>
    </div>

    <!-- Impact Legend -->
    <div class="impact-legend d-flex text-center mb-3">
        <div class="legend-block bg-success text-white">No Impact</div>
        <div class="legend-block bg-warning text-dark">Intermediate Impact</div>
        <div class="legend-block bg-danger text-white">Significant Impact</div>
    </div>

    <!-- Sublifelines in columns under each top-level lifeline -->
    <div class="lifeline-columns">
<% For Each parent As DataRow In TopLevelLifelines.Rows %>
    <div class="lifeline-column" data-parentid="<%= parent("ID") %>">
        <% 
            Dim subRows As DataRow() = SubLifelines.Select("ParentID = " & parent("ID"))
            Dim i As Integer
            For i = 0 To subRows.Length - 1
                Dim subRow = subRows(i)
        %>
            <div class="lifeline-tile sub lifeline-parent-<%= subRow("ParentID") %>" 
                 data-parent="<%= subRow("ParentID") %>" 
                 data-id="<%= subRow("ID") %>" 
                 data-label="<%= subRow("LifelineLabel") %>" 
                 data-impact="">
                <img src="<%= subRow("IconPath") %>" alt="" class="lifeline-icon" />
            </div>
        <% Next %>
    </div>
<% Next %>



    </div>

</div>



    <div class="mt-4 text-end">
        <asp:Button ID="btnSubmitLifeline" runat="server" Text="Submit Assessment" CssClass="btn btn-primary" OnClick="btnSubmitLifeline_Click" />
    </div>

    <asp:HiddenField ID="hfAssessmentID" runat="server" />
</div>

<!-- Scripts -->
<script>
document.addEventListener("DOMContentLoaded", function () {
    const topTiles = document.querySelectorAll(".lifeline-tile.top");
    const subTiles = document.querySelectorAll(".lifeline-tile.sub");

    topTiles.forEach(tile => {
        tile.addEventListener("click", () => {
            tile.classList.toggle("active-top");
            const id = tile.dataset.id;
            document.querySelectorAll(`.lifeline-parent-${id}`).forEach(sub => {
                if (tile.classList.contains("active-top")) {
                    sub.classList.add("impact-green");
                    sub.dataset.impact = "green";
                } else {
                    sub.classList.remove("impact-green", "impact-yellow", "impact-red");
                    sub.dataset.impact = "";
                }
            });
        });
    });

    subTiles.forEach(tile => {
        tile.addEventListener("click", () => {
            const parent = document.querySelector(`.lifeline-tile.top[data-id='${tile.dataset.parent}']`);
            if (!parent.classList.contains("active-top")) return;

            const nextState = { "": "green", "green": "yellow", "yellow": "red", "red": "green" };
            const current = tile.dataset.impact;

            tile.classList.remove("impact-green", "impact-yellow", "impact-red");
            tile.dataset.impact = nextState[current];
            tile.classList.add(`impact-${nextState[current]}`);
        });
    });
});
</script>

<style>
/* Grid containers */
.lifeline-grid {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 1rem;
}

.top-lifelines {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    gap: 16px;
    max-width: 1200px;
    width: 100%;
}

.lifeline-columns {
    display: flex;
    flex-wrap: nowrap;
    justify-content: center;
    gap: 16px;
    width: 100%;
    overflow-x: auto;
    padding-top: 1rem;
}

.lifeline-column {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 10px;
}

/* Lifeline tiles */
.lifeline-tile {
    position: relative;
    width: 125px;
    height: 110px;
    background-color: #fff;
    border: 2px solid transparent;
    border-radius: 8px;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    box-sizing: border-box;
    overflow: hidden;
    text-align: center;
    padding: 8px;
    flex-shrink: 0;
}

/* Icon inside tile */
.lifeline-icon {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    object-fit: contain;
    opacity: 0.15;
    pointer-events: none;
    z-index: 1;
}

/* Label always visible */
.lifeline-tile::after {
    content: attr(data-label);
    position: relative;
    z-index: 2;
    font-weight: bold;
    font-size: 0.8rem;
    color: #000;
    text-align: center;
    padding: 4px;
    line-height: 1.2;
    word-break: break-word;
}

/* States */
.active-top {
    border-color: #007bff;
}
.impact-green {
    background-color: #d4edda !important;
    border-color: #28a745 !important;
}
.impact-yellow {
    background-color: #fff3cd !important;
    border-color: #ffc107 !important;
}
.impact-red {
    background-color: #f8d7da !important;
    border-color: #dc3545 !important;
}

/* Legend */
.impact-legend {
    display: flex;
    justify-content: center;
    gap: 12px;
    width: 100%;
    max-width: 960px;
}
.legend-block {
    flex: 1;
    padding: 0.5rem 1rem;
    font-weight: bold;
    text-align: center;
    border-radius: 4px;
}

</style>


</asp:Content>
