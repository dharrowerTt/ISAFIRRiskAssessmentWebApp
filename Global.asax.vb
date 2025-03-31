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

End Class
