<h1>I-SAFIR Web Application</h1>

<h2>Overview</h2>
<p>
  The <strong>Illinois State Assessment for Infrastructure Resilience (I-SAFIR)</strong> web application is a secure, internal-use tool built to support the identification, assessment, and management of risks to public- and private-sector critical infrastructure assets and systems within the State of Illinois.
  It is overseen by the <strong>Critical Infrastructure Committee of the Illinois Terrorism Task Force (ITTF)</strong>, with implementation co-led by <strong>IDOT</strong> and <strong>IEMA</strong>.
</p>
<p>
  This web version is a modernization of the original Microsoft Access-based I-SAFIR Risk Assessment Tool.
</p>

<h2>Purpose</h2>
<p>
  The I-SAFIR Risk Assessment Tool provides an all-hazards methodology and automated risk calculation engine to support:
</p>
<ul>
  <li>Risk analysis at various levels (facility, corporate, municipal, regional, etc.)</li>
  <li>Informed planning, investment, and prioritization across agencies and jurisdictions</li>
  <li>Restoration and recovery planning for incidents of varying complexity</li>
  <li>Grant application support through standardized risk metrics</li>
</ul>
<p>
  The tool is built to be <strong>consistent</strong>, <strong>repeatable</strong>, and <strong>objective</strong>, using authoritative data, SME input, and nationally recognized standards.
</p>

<h2>Technology Stack</h2>
<ul>
  <li><strong>Backend:</strong> ASP.NET Web Forms (VB.NET)</li>
  <li><strong>Database:</strong> Microsoft SQL Server</li>
  <li><strong>Frontend:</strong> HTML, JavaScript, Bootstrap</li>
  <li><strong>Hosting:</strong> Private, secured server environment (non-public)</li>
</ul>

<h2>Repository Details</h2>
<blockquote>
  🛑 This is a <strong>private repository</strong>. The I-SAFIR system is intended for internal use only and will <strong>never</strong> be made publicly accessible.
</blockquote>

<h2>Features</h2>
<ul>
  <li>Step-by-step guided risk assessment workflow</li>
  <li>Data-driven scoring based on user input and predefined criteria</li>
  <li>Role-based access for different user types (e.g., agency admins, assessors)</li>
  <li>Reporting engine with support for multiple output formats</li>
  <li>Secure login and session management</li>
</ul>

<h2>Development & Setup</h2>

<h3>Requirements</h3>
<ul>
  <li>Visual Studio with .NET Framework support</li>
  <li>SQL Server instance (local or remote)</li>
  <li>IIS (for local testing or staging)</li>
  <li>Access to internal network dependencies (if applicable)</li>
</ul>

<h3>Configuration</h3>
<ul>
  <li>Update <code>web.config</code> with environment-specific connection strings and settings.</li>
  <li>Ensure appropriate access controls are set up for handling sensitive data.</li>
</ul>

<h3>Deployment</h3>
<p>
  Deployment is handled via secured, internal pipelines. Reach out to the project admin for access to staging or production environments.
</p>
<h2>Recent Updates</h2>
<ul>
  <li>Added dynamic Threat Assessment matrix with improved UI/UX (Bootstrap 4-compatible).</li>
  <li>Introduced internal threat matrix view with accessibility enhancements (screenreader-compatible tooltips).</li>
  <li>Created Consequence Assessment module framework for dynamic threat × impact consequence rating.</li>
  <li>Refactored legacy markup for Bootstrap 4 compatibility (removed Bootstrap 5 dependencies).</li>
  <li>Improved SQL-driven loading of Threat Assessment and Consequence Assessment data.</li>
  <li>Resolved long-standing jQuery duplication issues and streamlined client-side scripting.</li>
</ul>
<h2>Upcoming Development</h2>
<ul>
  <li>Complete Consequence Assessment save and load functionality.</li>
  <li>Implement full guidance text for all Impact areas.</li>
  <li>Polish data validation, accessibility, and error handling for matrix inputs.</li>
  <li>Continue interface modernization and prep for full security review before go-live.</li>
</ul>

