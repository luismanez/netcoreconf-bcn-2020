using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Instanyam.Jobs.Common
{
    public static class Constants
    {
        public static class FunctionsNaming
        {
            public const string ProcessImageStarter = nameof(ProcessImageStarter);
            public const string ProcessImageOrchestrator = nameof(ProcessImageOrchestrator);
            public const string ProcessImageTagsSubOrchestrator = nameof(ProcessImageTagsSubOrchestrator);
            public const string GetTagsActivity = nameof(GetTagsActivity);
            public const string ResizeImageActivity = nameof(ResizeImageActivity);
            public const string SendNotificationActivity = nameof(SendNotificationActivity);
            public const string SendRejectNotificationActivity = nameof(SendRejectNotificationActivity);
            public const string UpdateTagsIndexAndAggregatesActivity = nameof(UpdateTagsIndexAndAggregatesActivity);
        }
    }
}
