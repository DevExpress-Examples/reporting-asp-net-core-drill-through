using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace ASPNETCoreHowToCreateDrillDownReports.PredefinedReports {
    public static class ReportsFactory
    {
        public static Dictionary<string, Func<XtraReport>> Reports = new Dictionary<string, Func<XtraReport>>()
        {
            ["MainReport"] = () => new MainReport(),
            ["DetailReport1"] = () => new Details()
        };
    }
}
