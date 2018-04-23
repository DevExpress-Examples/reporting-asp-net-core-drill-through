using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using WebApplication1.Reports;

namespace WebApplication1 {
    public class CustomDrillThroughProcessor : IWebDocumentViewerDrillThroughProcessor {
        public XtraReport CreateReport(DrillThroughContext context) {
            if (context.CustomData == "#back")
                return new MainReport();
            if (context.CustomData == "#detail1")
                return new DetailReport1();
            if (context.CustomData == "#detail2")
                return new DetailReport2();
            return context.Report;
        }
    }
}