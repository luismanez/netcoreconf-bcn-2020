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
    public static class GetTagsActivity
    {
        [FunctionName(Constants.FunctionsNaming.GetTagsActivity)]
        public static async Task<List<string>> GetTags([ActivityTrigger] string image, ILogger log)
        {
            log.LogInformation($"Extracting Tags from image: {image}");

            // simulate calling Cognitive Services Vision API to extract Tags from image...
            await Task.Delay(6000);

            if (image.StartsWith("food", StringComparison.InvariantCultureIgnoreCase))
            {
                return new List<string> {"Food", "Veggie", "Broccoli", "Healthy"};
            }

            return new List<string> { "Nature", "Cat", "Pets" };
        }
    }
}
