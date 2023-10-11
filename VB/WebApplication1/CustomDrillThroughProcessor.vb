Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Web.WebDocumentViewer
Imports WebApplication1.Reports

Namespace WebApplication1

    Public Class CustomDrillThroughProcessor
        Implements IWebDocumentViewerDrillThroughProcessor

        Public Function CreateReport(ByVal context As DrillThroughContext) As XtraReport Implements IWebDocumentViewerDrillThroughProcessor.CreateReport
            If Equals(context.CustomData, "#back") Then Return New MainReport()
            If Equals(context.CustomData, "#detail1") Then Return New DetailReport1()
            If Equals(context.CustomData, "#detail2") Then Return New DetailReport2()
            Return context.Report
        End Function
    End Class
End Namespace
