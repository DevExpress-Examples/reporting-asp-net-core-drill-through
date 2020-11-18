using System.Threading.Tasks;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreHowToCreateDrillDownReports.Controllers {
    public class HomeController : Controller {        
        public async Task<IActionResult> Index([FromServices] IWebDocumentViewerClientSideModelGenerator viewerModelGenerator, [FromQuery] string reportName = "MainReport") {
            var model = await viewerModelGenerator.GetModelAsync(reportName, WebDocumentViewerController.DefaultUri);
            return View(model);
        }
    }
}
