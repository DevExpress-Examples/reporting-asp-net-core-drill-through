Imports System
Imports WebApplication1.Reports

Namespace WebApplication1

    Public Partial Class WebForm1
        Inherits Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            ASPxWebDocumentViewer1.OpenReport(New MainReport())
        End Sub
    End Class
End Namespace
