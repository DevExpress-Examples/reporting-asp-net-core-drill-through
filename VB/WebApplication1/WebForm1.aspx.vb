Imports System
Imports WebApplication1.Reports

Namespace WebApplication1
	Partial Public Class WebForm1
		Inherits System.Web.UI.Page

		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
			ASPxWebDocumentViewer1.OpenReport(New MainReport())
		End Sub
	End Class
End Namespace