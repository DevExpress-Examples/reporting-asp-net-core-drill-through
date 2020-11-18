using System;
using System.Text.Json;
using System.Threading.Tasks;
using DevExpress.XtraReports.Services;
using DevExpress.XtraReports.Web.WebDocumentViewer;

namespace ASPNETCoreHowToCreateDrillDownReports.Services {
    public class NavigateInfo {
        public string NavigateTo { get; set; }
        public string MasterID { get; set; }
    }
    public class CustomDrillThroughProcessorAsync : IDrillThroughProcessorAsync {
        readonly IReportProviderAsync reportProviderAsync;

        public CustomDrillThroughProcessorAsync(IReportProviderAsync reportProviderAsync) {
            this.reportProviderAsync = reportProviderAsync;
        }
        public async Task<DrillThroughResult> CreateReportAsync(DrillThroughContext context) {
            NavigateInfo navigateInfo = JsonSerializer.Deserialize<NavigateInfo>(context.CustomData);
            var reportNameToOpen = navigateInfo.NavigateTo == "back" ? "MainReport"
                : navigateInfo.NavigateTo == "details" ? "DetailReport1" : null;
            var report = await reportProviderAsync.GetReportAsync(reportNameToOpen, null) ?? context.Report;

            if(navigateInfo.NavigateTo == "details") {
                int catID = 0;
                Int32.TryParse(navigateInfo.MasterID, out catID);
                report.Parameters["categoryID"].Value = catID;
            }
            return new DrillThroughResult(report);
        }
    }
}
