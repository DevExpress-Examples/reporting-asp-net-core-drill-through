<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<%@ Register assembly="DevExpress.XtraReports.v17.1.Web, Version=17.1.17.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function previewClick(s, e) {
            var brick = e.Brick;
            var previewModel = s.GetPreviewModel();
            var reportPreview = previewModel && previewModel.reportPreview;
            if (brick && brick.navigation && brick.navigation.url && ["#back", "#detail1", "#detail2"].indexOf(brick.navigation.url) >= 0) {
                reportPreview.drillThrough(brick.navigation.url);
                e.Handled = true;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server">
            <ClientSideEvents PreviewClick="previewClick"/>
        </dx:ASPxWebDocumentViewer>
    
    </div>
    </form>
</body>
</html>
