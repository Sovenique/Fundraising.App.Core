using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System.Collections.Generic;
using System.Linq;

namespace Fundraising.App.Core.ApplicationInsights
{
    public class StaticWebAssetsTelemetryProcessor : ITelemetryProcessor
    {
        private static readonly List<string> staticResourceNames = new List<string>
        {
            "css",
            "js",
            "lib",
            "favicon.ico",
        };

        private ITelemetryProcessor next { get; set; }

        public StaticWebAssetsTelemetryProcessor(ITelemetryProcessor next)
        {
            this.next = next;
        }

        public void Process(ITelemetry item)
        {

            if (!OkToSend(item))
            {
                return;
            }

            next.Process(item);
        }

        private bool OkToSend(ITelemetry item)
        {
            var requestTelemetry = item as RequestTelemetry;

            if (requestTelemetry != null)
            {
                var requestMethod = requestTelemetry.Name.Split(" ")[0];

                if (requestMethod == "GET" && staticResourceNames.Any(assetName => requestTelemetry.Name.Contains(assetName)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
