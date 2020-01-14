using Instanyam.Jobs.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace Instanyam.Jobs.Activities
{
    public static class ResizeImageActivity
    {
        [FunctionName(Constants.FunctionsNaming.ResizeImageActivity)]
        public static async Task<string> ResizeImage([ActivityTrigger] string image, ILogger log)
        {
            log.LogInformation($"Resizing image: {image}");

            // simulate resizing operations...
            await Task.Delay(3000);

            return "200x200";
        }
    }
}
