Imports System.Web.Optimization
Imports System.Security.Principal

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Fires when the application is started
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub

    ' Get the current role, factoring in simulation if set
    Public Shared Function GetCurrentUserRole(user As IPrincipal) As String
        Dim simulated = HttpContext.Current?.Session?("SimulatedRole")
        If simulated IsNot Nothing Then
            Return simulated.ToString()
        End If

        Dim userRoles() As String = System.Web.Security.Roles.GetRolesForUser(user.Identity.Name)
        If userRoles.Length > 0 Then
            Return userRoles(0)
        End If

        Return "Unknown"
    End Function

    Sub Application_AuthenticateRequest(sender As Object, e As EventArgs)
        Dim path = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower()
        Dim isAuthenticated = HttpContext.Current.User IsNot Nothing AndAlso HttpContext.Current.User.Identity.IsAuthenticated

        System.Diagnostics.Debug.WriteLine(">>> [AUTH REQ] Path: " & path)
        System.Diagnostics.Debug.WriteLine(">>> [AUTH REQ] IsAuthenticated: " & isAuthenticated)

        Dim allowedPages As String() = {
            "~/login.aspx",
            "~/register.aspx",
            "~/oktalogin.aspx",
            "~/signin-oidc",   ' crucial
            "~/test.aspx"
        }

        ' Normalized both as-is and with .aspx fallback
        Dim pathCheck = path
        If Not pathCheck.EndsWith(".aspx") Then
            pathCheck &= ".aspx"
        End If

        If Not isAuthenticated AndAlso Not allowedPages.Contains(pathCheck) AndAlso Not allowedPages.Contains(path) Then
            System.Diagnostics.Debug.WriteLine(">>> [AUTH REQ] Redirecting to OktaLogin.aspx")
            HttpContext.Current.Response.Redirect("~/OktaLogin.aspx", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        Else
            System.Diagnostics.Debug.WriteLine(">>> [AUTH REQ] Allowing request through")
        End If

    End Sub




End Class
