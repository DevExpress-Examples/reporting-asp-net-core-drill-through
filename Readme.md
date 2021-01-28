<!-- default file list -->
*Files to look at*:

* **[CustomDrillThroughProcessor.cs](./CS/WebApplication1/CustomDrillThroughProcessor.cs) (VB: [CustomDrillThroughProcessor.vb](./VB/WebApplication1/CustomDrillThroughProcessor.vb))**
* [Global.asax.cs](./CS/WebApplication1/Global.asax.cs) (VB: [Global.asax.cs](./VB/WebApplication1/Global.asax.cs))
* [WebForm1.aspx](./CS/WebApplication1/WebForm1.aspx) (VB: [WebForm1.aspx](./VB/WebApplication1/WebForm1.aspx))
* [WebForm1.aspx.cs](./CS/WebApplication1/WebForm1.aspx.cs)
<!-- default file list end -->
# How to provide drill-through functionality to web reports


This example illustrates how to provide navigation between different reports so that clicking an element in the main report opens another report in the same <a href="https://documentation.devexpress.com/#XtraReports/CustomDocument17738">Web Document Viewer</a> instance.<br><br>To do this, implement the <a href="https://documentation.devexpress.com/#XtraReports/clsDevExpressXtraReportsWebWebDocumentViewerIWebDocumentViewerDrillThroughProcessortopic">IWebDocumentViewerDrillThroughProcessor</a><strong> </strong>interface. In its <a href="https://documentation.devexpress.com/#XtraReports/DevExpressXtraReportsWebWebDocumentViewerIWebDocumentViewerDrillThroughProcessor_CreateReporttopic">CreateReport</a><strong> </strong>method, pass a <a href="https://documentation.devexpress.com/#XtraReports/clsDevExpressXtraReportsWebWebDocumentViewerDrillThroughContexttopic">DrillThroughContext</a><strong> </strong>instance and use its <a href="https://documentation.devexpress.com/#XtraReports/DevExpressXtraReportsWebWebDocumentViewerDrillThroughContext_CustomDatatopic">CustomData</a><strong> </strong>property to specify the navigation logic.<br><br>In this example, the <strong>CustomData</strong> value is compared against the <strong>NavigateUrl</strong> property values of report elements acting as links to open other reports.<br><br>To register a custom drill-through processor, call the <a href="https://documentation.devexpress.com/#XtraReports/DevExpressXtraReportsWebWebDocumentViewerDefaultWebDocumentViewerContainer_RegisterWebDocumentViewerDrillThroughProcessor~T~topic">RegisterWebDocumentViewerDrillThroughProcessor</a><strong> </strong>method of the <a href="https://documentation.devexpress.com/#XtraReports/clsDevExpressXtraReportsWebWebDocumentViewerDefaultWebDocumentViewerContainertopic">DefaultWebDocumentViewerContainer</a><strong> </strong>class at the application startup.<br><br>Handle the client-side <a href="https://documentation.devexpress.com/XtraReports/DevExpress.XtraReports.Web.Scripts.ASPxClientWebDocumentViewer.PreviewClick.event">PreviewClick</a><strong> </strong>event of the Document Viewer to process mouse events related to report elements.

<br/>


