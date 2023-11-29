using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.XtraReports.Services;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Hosting;

namespace ASPNETCoreCreateDrillThroughReports.Services {
    public class CustomReportProviderAsync : IReportProviderAsync {
        public const string MyReportsDirectoryName = "Reports";
        readonly IWebHostEnvironment env;

        public CustomReportProviderAsync(IWebHostEnvironment env) {
            this.env = env;
        }
        public async Task<XtraReport> GetReportAsync(string id, ReportProviderContext context) {
            if(string.IsNullOrEmpty(id))
                return null;

            var reportDirectoryPath = Path.Combine(env.ContentRootPath, MyReportsDirectoryName);
            var reportLaytoutFileInfo = new DirectoryInfo(reportDirectoryPath).GetFiles(id + ".repx", SearchOption.TopDirectoryOnly).SingleOrDefault();
            if(reportLaytoutFileInfo == null)
                return null;
            var reportLayout = await File.ReadAllBytesAsync(reportLaytoutFileInfo.FullName);
            using(var ms = new MemoryStream(reportLayout)) {
                ms.Position = 0;
                return XtraReport.FromXmlStream(ms);
            }
        }
    }
}
