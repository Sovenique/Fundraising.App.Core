

namespace Fundraising.App.Core.Entities
{
    public enum ProjectStatus
    {
        ON_HOLD,        // It will not accept new payments until reopened
        IN_PROGRESS,    // It will accept new payments
        COMPLETED       // It will not accept any other payments
    }
}
