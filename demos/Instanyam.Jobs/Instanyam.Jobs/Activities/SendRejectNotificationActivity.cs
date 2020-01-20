using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Instanyam.Jobs.Common;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace Instanyam.Jobs.Activities
{
    public static class SendRejectNotificationActivity
    {
        [FunctionName(Constants.FunctionsNaming.SendRejectNotificationActivity)]
        public static async Task<string> SendRejectNotification([ActivityTrigger] string image, ILogger log)
        {
            // simulate sending notifications (email, SMS...)...
            await Task.Delay(1000);

            return $"hey you, the image: {image} has been rejected... no more cat images!!";
        }
    }
}
