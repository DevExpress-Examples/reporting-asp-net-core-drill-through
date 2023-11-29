using System.Threading.Tasks;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreCreateDrillThroughReports.Controllers {
    public class HomeController : Controller {        
        public async Task<IActionResult> Index([FromServices] IWebDocumentViewerClientSideModelGenerator viewerModelGenerator, [FromQuery] string reportName = "CategoriesReport") {
            var model = await viewerModelGenerator.GetModelAsync(reportName, WebDocumentViewerController.DefaultUri);
            return View(model);
        }
    }
}
