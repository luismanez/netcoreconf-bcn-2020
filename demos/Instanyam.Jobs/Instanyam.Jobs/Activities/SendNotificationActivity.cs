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
    public static class SendNotificationActivity
    {
        [FunctionName(Constants.FunctionsNaming.SendNotificationActivity)]
        public static async Task<string> SendNotification([ActivityTrigger] string image, ILogger log)
        {
            log.LogInformation($"Sending notification for Image: {image}");

            // simulate sending notifications (email, SMS...)...
            await Task.Delay(1000);

            return $"hey you, the image: {image} has been fully processed!";
        }
    }
}
