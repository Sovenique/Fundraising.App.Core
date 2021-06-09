using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising.App.Core.ApplicationInsights
{
    class ApplicationInsightsSettings
    {
        public const string SectionKey = "ApplicationInsights";
        public string CloudRoleName { get; set; }
        public string InstrumentationKey { get; set; }
    }
}
