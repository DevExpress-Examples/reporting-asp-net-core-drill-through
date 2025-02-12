<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128602993/22.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T483368)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Web Reporting - How to create drill-through reports

## Overview 

The example shows how to add drill-through functionality to [DevExpress Reports](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XtraReport). 
A drill-through report allows users to click a report element (it can be a text or an image) and open another report with the same [viewer](https://docs.devexpress.com/XtraReports/400248/web-reporting/asp-net-core-reporting/document-viewer). 

In this example, a user clicks the "Show Details" [label](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XRLabel) in the main report, and the [Web Document Viewer](https://docs.devexpress.com/XtraReports/400248/web-reporting/asp-net-core-reporting/document-viewer) displays a detailed report associated with the clicked item. The detailed report contains a ‘back’ button (actually it is an [image](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XRPictureBox)) that navigates back to the master report. 

## Implementation Details 

A click in the report triggers a client-side [PreviewClick](https://docs.devexpress.com/XtraReports/DevExpress.AspNetCore.Reporting.WebDocumentViewer.WebDocumentViewerClientSideEventsBuilder.PreviewClick(System.String)) event. In this event handler, obtain information about a clicked element and pass it to the `drillThrough` method of a report preview. This client method accesses the server-side [IDrillThroughProcessorAsync](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.IDrillThroughProcessorAsync) service and calls its [CreateReportAsync](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.IDrillThroughProcessorAsync.CreateReportAsync(DevExpress.XtraReports.Web.WebDocumentViewer.DrillThroughContext)) method. 
The [CreateReportAsync](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.IDrillThroughProcessorAsync.CreateReportAsync(DevExpress.XtraReports.Web.WebDocumentViewer.DrillThroughContext)) method receives a [DrillThroughContext](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.DrillThroughContext) object as an argument. Its [DrillThroughContext.CustomData](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.DrillThroughContext.CustomData) property contains information supplied earlier in the `PreviewClick` event handler. Process this information and determine a report to open. Call the [IReportProviderAsync.GetReportAsync](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Services.IReportProviderAsync) method to asynchronously request a report. At this stage, you can configure a report before it is passed to the viewer - specify [report parameters](https://docs.devexpress.com/XtraReports/4812/detailed-guide-to-devexpress-reporting/shape-report-data/use-report-parameters) or adjust report settings. 

### Report  

Assign the “details” string (in a master report) or “back” string (in a detail report) to a report control’s [NavigateUrl](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XRControl.NavigateUrl) property to create a link and specify a navigation path. To specify a master value, [bind a label's Tag property](https://docs.devexpress.com/XtraReports/1180/detailed-guide-to-devexpress-reporting/use-report-controls/bind-report-controls-to-data/specify-a-control-s-binding-expression) to the main report's ID field.
The master value is used to  filter a detail report [at the data source level](https://docs.devexpress.com/XtraReports/4804/detailed-guide-to-devexpress-reporting/shape-report-data/filter-data/filter-data-at-the-data-source-level).

### Client  
A click in the report triggers a client-side [PreviewClick](https://docs.devexpress.com/XtraReports/DevExpress.AspNetCore.Reporting.WebDocumentViewer.WebDocumentViewerClientSideEventsBuilder.PreviewClick(System.String)) event. In the event handler, obtain a report control's `NavigateUrl` property value from the`Brick.navigation.url` property.
Use the [GetBrickValue](https://docs.devexpress.com/XtraReports/js-ASPxClientPreviewClickEventArgs#js_aspxclientpreviewclickeventargs_getbrickvalue) method to get a report control's `Tag` property value. 
The data is serialized to JSON, and the client-side`drillThrough` method of a report preview sends them to the server. 

The following code is the `PreviewClick` event handler function: 

```
function previewClick(s, e) {
        var brick = e.Brick;
        var navigationUrl = brick && brick.navigation && brick.navigation.url;
        if(navigationUrl && ["back", "details"].indexOf(navigationUrl) >= 0) {
            var reportPreview = s.GetReportPreview();
            var navigateInfo = {
                NavigateTo: navigationUrl,
                MasterID: e.GetBrickValue(),
            };
            reportPreview.drillThrough(JSON.stringify(navigateInfo));
            e.Handled = true;
        }
}
```

### Server 

The `CustomDrillThroughProcessorAsync` class implements the [IDrillThroughProcessorAsync](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.IDrillThroughProcessorAsync) interface. Its [CreateReportAsync](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.IDrillThroughProcessorAsync.CreateReportAsync(DevExpress.XtraReports.Web.WebDocumentViewer.DrillThroughContext)) method receives the [DrillThroughContext](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.DrillThroughContext) object as an argument. The [CustomData](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.DrillThroughContext.CustomData) property contains data sent from the client-side `PreviewClick` event. 
Use this data to determine a report to open. After that, you should call the [GetReportAsync](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Services.IReportProviderAsync) method of the `CustomReportProviderAsync` service (a service that implements the [IReportProviderAsync](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Services.IReportProviderAsync) interface) to asynchronously request a report. A custom method implementation allows you to specify a [report parameter](https://docs.devexpress.com/XtraReports/4812/detailed-guide-to-devexpress-reporting/shape-report-data/use-report-parameters):
 

```
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
            var reportToOpen = navigateInfo.NavigateTo == "back" ? "MainReport"
                : navigateInfo.NavigateTo == "details" ? "DetailReport1" : null;
            var report = await reportProviderAsync.GetReportAsync(reportToOpen, null) ?? context.Report;

            if(navigateInfo.NavigateTo == "details") {
                int catID = 0;
                Int32.TryParse(navigateInfo.MasterID, out catID);
                report.Parameters["categoryID"].Value = catID;
            }
            return new DrillThroughResult(report);
        }
}
```
At application startup, register the `CustomDrillThroughProcessorAsync` and `CustomReportProviderAsync` services and call the [ReportingConfigurationBuilder.UseAsyncEngine](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.UseAsyncEngine?f=export) method to enable asynchronous mode: 
```
using Microsoft.Extensions.DependencyInjection;
using DevExpress.XtraReports.Services;

  public class Startup {
  // ...
      public void ConfigureServices(IServiceCollection services) {          
          // ...
          services.ConfigureReportingServices(configurator => {
              // ...
              configurator.UseAsyncEngine();
          });
          // ...
          services.AddScoped<IReportProviderAsync, CustomReportProviderAsync>();
          services.AddScoped<IDrillThroughProcessorAsync, CustomDrillThroughProcessorAsync>();
      }
      // ...
  }
```

**See also**
* [ASP.NET Core Reporting](https://docs.devexpress.com/XtraReports/119717/web-reporting/aspnet-core-reporting)
* [How to Use the Asynchronous Engine for Web Reporting](https://github.com/DevExpress-Examples/Reporting-Use-Async-Engine-In-AspNet-Core)


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-asp-net-core-drill-through&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-asp-net-core-drill-through&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
