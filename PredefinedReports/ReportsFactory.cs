using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace ASPNETCoreCreateDrillThroughReports.PredefinedReports {
    public static class ReportsFactory {
        public static Dictionary<string, Func<XtraReport>> Reports = new Dictionary<string, Func<XtraReport>>() {
            ["ProductsReport"] = () => new XtraReportProducts(),
            ["CategoriesReport"] = () => new XtraReportCategories()
        };
    }
}
