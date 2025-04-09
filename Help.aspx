<%@ Page Title="Help & Support" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Help.aspx.vb" Inherits="ISAFIRRiskAssessmentWebApp.Help" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container py-5">
    <h1 class="mb-4">Help & Support</h1>

    <!-- Help Categories -->
    <div class="container mt-4">
  
  <div class="row">

    <div class="col-md-4 mb-3">
      <div class="card shadow-sm">
        <div class="card-body text-center">
          <h5 class="card-title">🛠️ Getting Started</h5>
          <p class="card-text">Learn how to begin using I-SAFIR Risk Assessment.</p>
          <a href="#gettingStarted" class="btn btn-outline-primary">View Topics</a>
        </div>
      </div>
    </div>

    <div class="col-md-4 mb-3">
      <div class="card shadow-sm">
        <div class="card-body text-center">
          <h5 class="card-title">🏭 Managing Facilities</h5>
          <p class="card-text">Add, edit, and organize facility information.</p>
          <a href="#managingFacilities" class="btn btn-outline-primary">View Topics</a>
        </div>
      </div>
    </div>

    <div class="col-md-4 mb-3">
      <div class="card shadow-sm">
        <div class="card-body text-center">
          <h5 class="card-title">⚖️ Risk Assessments</h5>
          <p class="card-text">Understand how to conduct and review assessments.</p>
          <a href="#riskAssessment" class="btn btn-outline-primary">View Topics</a>
        </div>
      </div>
    </div>

    <div class="col-md-4 mb-3">
      <div class="card shadow-sm">
        <div class="card-body text-center">
          <h5 class="card-title">👤 Roles & Access</h5>
          <p class="card-text">User roles and permission management explained.</p>
          <a href="#rolesAccess" class="btn btn-outline-primary">View Topics</a>
        </div>
      </div>
    </div>

    <div class="col-md-4 mb-3">
      <div class="card shadow-sm">
        <div class="card-body text-center">
          <h5 class="card-title">🔧 Admin Tools</h5>
          <p class="card-text">Advanced tools available for administrators.</p>
          <a href="#adminTools" class="btn btn-outline-primary">View Topics</a>
        </div>
      </div>
    </div>

    <div class="col-md-4 mb-3">
      <div class="card shadow-sm">
        <div class="card-body text-center">
          <h5 class="card-title">📬 Contact Support</h5>
          <p class="card-text">Reach out if you're stuck or need assistance.</p>
          <a href="#contactSupport" class="btn btn-outline-primary">View Topics</a>
        </div>
      </div>
    </div>

  </div>
</div>


<div class="container mt-5">

  <!-- 🛠️ Getting Started -->
  <div id="gettingStarted" class="mb-5">
    <h3>🛠️ Getting Started</h3>
    <div class="accordion" id="accordionGettingStarted">
      <div class="card">
        <div class="card-header" id="gs1">
          <h2 class="mb-0">
            <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseGs1">
              How to log in to the system
            </button>
          </h2>
        </div>
        <div id="collapseGs1" class="collapse show" data-parent="#accordionGettingStarted">
          <div class="card-body">
            Use your email and assigned credentials to log in. If OKTA is enabled later, this will integrate.
          </div>
        </div>
      </div>
      <div class="card">
        <div class="card-header" id="gs2">
          <h2 class="mb-0">
            <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseGs2">
              Setting up your profile
            </button>
          </h2>
        </div>
        <div id="collapseGs2" class="collapse" data-parent="#accordionGettingStarted">
          <div class="card-body">
            Profile setup is automatic from your login data, but you can update your contact info in the settings page.
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- 🏭 Managing Facilities -->
  <div id="managingFacilities" class="mb-5">
    <h3>🏭 Managing Facilities</h3>
    <div class="accordion" id="accordionFacilities">
      <div class="card">
        <div class="card-header" id="mf1">
          <h2 class="mb-0">
            <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseMf1">
              Adding a new facility
            </button>
          </h2>
        </div>
        <div id="collapseMf1" class="collapse show" data-parent="#accordionFacilities">
          <div class="card-body">
            Click “Add New Facility” on the Facility Management screen and complete the required fields.
          </div>
        </div>
      </div>
      <div class="card">
        <div class="card-header" id="mf2">
          <h2 class="mb-0">
            <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseMf2">
              Editing facility information
            </button>
          </h2>
        </div>
        <div id="collapseMf2" class="collapse" data-parent="#accordionFacilities">
          <div class="card-body">
            Use the “Edit” button in the table next to each facility to update details.
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- ⚖️ Risk Assessments -->
  <div id="riskAssessment" class="mb-5">
    <h3>⚖️ Risk Assessments</h3>
    <div class="accordion" id="accordionAssessments">
      <div class="card">
        <div class="card-header" id="ra1">
          <h2 class="mb-0">
            <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseRa1">
              Starting a new assessment
            </button>
          </h2>
        </div>
        <div id="collapseRa1" class="collapse show" data-parent="#accordionAssessments">
          <div class="card-body">
            After adding a facility, click “Begin Assessment” to launch a new risk evaluation process.
          </div>
        </div>
      </div>
      <div class="card">
        <div class="card-header" id="ra2">
          <h2 class="mb-0">
            <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseRa2">
              Reviewing historical scores
            </button>
          </h2>
        </div>
        <div id="collapseRa2" class="collapse" data-parent="#accordionAssessments">
          <div class="card-body">
            Past scores can be viewed within each facility's record once assessments are complete.
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- 👤 Roles & Access -->
  <div id="rolesAccess" class="mb-5">
    <h3>👤 Roles & Access</h3>
    <div class="accordion" id="accordionRoles">
      <div class="card">
        <div class="card-header" id="ra3">
          <h2 class="mb-0">
            <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseRa3">
              Understanding roles
            </button>
          </h2>
        </div>
        <div id="collapseRa3" class="collapse show" data-parent="#accordionRoles">
          <div class="card-body">
            Roles define what each user can access, such as Admin, Assessor, or Viewer. Admins control role permissions.
          </div>
        </div>
      </div>
      <div class="card">
        <div class="card-header" id="ra4">
          <h2 class="mb-0">
            <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseRa4">
              Simulating another role
            </button>
          </h2>
        </div>
        <div id="collapseRa4" class="collapse" data-parent="#accordionRoles">
          <div class="card-body">
            Admins can simulate another role to test the system’s behavior for different user types.
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- 🔧 Admin Tools -->
  <div id="adminTools" class="mb-5">
    <h3>🔧 Admin Tools</h3>
    <div class="accordion" id="accordionAdmin">
      <div class="card">
        <div class="card-header" id="at1">
          <h2 class="mb-0">
            <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseAt1">
              Managing user roles
            </button>
          </h2>
        </div>
        <div id="collapseAt1" class="collapse show" data-parent="#accordionAdmin">
          <div class="card-body">
            Go to the User Role Management screen to assign or change user roles.
          </div>
        </div>
      </div>
      <div class="card">
        <div class="card-header" id="at2">
          <h2 class="mb-0">
            <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseAt2">
              Controlling permissions per role
            </button>
          </h2>
        </div>
        <div id="collapseAt2" class="collapse" data-parent="#accordionAdmin">
          <div class="card-body">
            The Role Function Management screen allows you to define what actions each role can perform.
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- 📬 Contact Support -->
  <div id="contactSupport" class="mb-5">
    <h3>📬 Contact Support</h3>
    <div class="accordion" id="accordionSupport">
      <div class="card">
        <div class="card-header" id="cs1">
          <h2 class="mb-0">
            <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseCs1">
              Getting help
            </button>
          </h2>
        </div>
        <div id="collapseCs1" class="collapse show" data-parent="#accordionSupport">
          <div class="card-body">
            Reach out to <strong>email address</strong> for technical issues or guidance.
          </div>
        </div>
      </div>
      <div class="card">
        <div class="card-header" id="cs2">
          <h2 class="mb-0">
            <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseCs2">
              Reporting bugs
            </button>
          </h2>
        </div>
        <div id="collapseCs2" class="collapse" data-parent="#accordionSupport">
          <div class="card-body">
            Use the "Report a Bug" link at the bottom of the page, or email with a screenshot and steps to reproduce.
          </div>
        </div>
      </div>
    </div>
  </div>

</div>

  </div>
</asp:Content>
