using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace Fundraising.App.Core.ApplicationInsights
{

    class CloudRoleTelemetryInitializer : ITelemetryInitializer
    {
        private static readonly string MachineName = Environment.MachineName.ToLower();
        private readonly ApplicationInsightsSettings _applicationInsightsSettings;

        public CloudRoleTelemetryInitializer(ApplicationInsightsSettings applicationInsightsSettings)
        {
            _applicationInsightsSettings = applicationInsightsSettings;
        }
        public void Initialize(ITelemetry telemetry)

        {
            telemetry.Context.Cloud.RoleName = _applicationInsightsSettings.CloudRoleName;
            telemetry.Context.Cloud.RoleInstance = "MachineName";
        }
    }
}
