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

    Protected Sub Application_AcquireRequestState(ByVal sender As Object, ByVal e As EventArgs)
        Dim ctx = HttpContext.Current
        Dim path = ctx.Request.AppRelativeCurrentExecutionFilePath.ToLower()

        System.Diagnostics.Debug.WriteLine(">>> Global.asax: Application_AcquireRequestState triggered")
        System.Diagnostics.Debug.WriteLine(">>> Path: " & path)

        Dim owinIdentity = ctx.GetOwinContext().Authentication.User?.Identity
        Dim isOktaAuthenticated = owinIdentity IsNot Nothing AndAlso owinIdentity.IsAuthenticated

        Dim isSiteAuthenticated = False
        If ctx.Session IsNot Nothing Then
            isSiteAuthenticated = ctx.Session("LoggedInUser") IsNot Nothing
        End If

        System.Diagnostics.Debug.WriteLine($">>> Okta Authenticated: {isOktaAuthenticated}")
        System.Diagnostics.Debug.WriteLine($">>> Session exists: {ctx.Session IsNot Nothing}")
        System.Diagnostics.Debug.WriteLine($">>> LoggedInUser present: {isSiteAuthenticated}")

        ' --- ALLOW SPECIFIC PATHS UNCONDITIONALLY ---
        If path.Contains("signin-oidc") Then Return ' Let OIDC finish
        If path.StartsWith("~/content") OrElse path.StartsWith("~/scripts") OrElse path.Contains("favicon") Then Return
        If path.Contains("oktalogin") Then
            System.Diagnostics.Debug.WriteLine(">>> OktaLogin requested - skipping auth enforcement")
            Return
        End If

        ' --- REDIRECT TO OKTA IF NOT AUTHENTICATED ---
        If Not isOktaAuthenticated Then
            ' Only allow OktaLogin as a public page pre-auth
            If Not path.Contains("oktalogin") Then
                System.Diagnostics.Debug.WriteLine(">>> Redirecting to OktaLogin.aspx - not Okta-authenticated")
                ctx.Response.Redirect("~/OktaLogin.aspx", False)
                ctx.ApplicationInstance.CompleteRequest()
                Return
            End If
        End If

        ' --- ONCE OKTA-AUTHENTICATED, ENSURE SITE LOGIN EXISTS ---
        If isOktaAuthenticated AndAlso Not isSiteAuthenticated Then
            ' Let user reach Login.aspx or Register.aspx to log into the app
            If Not path.Contains("login") AndAlso Not path.Contains("register") Then
                System.Diagnostics.Debug.WriteLine(">>> Redirecting to Login.aspx - not site-authenticated")
                ctx.Response.Redirect("~/Login.aspx", False)
                ctx.ApplicationInstance.CompleteRequest()
            End If
        End If
    End Sub




End Class
