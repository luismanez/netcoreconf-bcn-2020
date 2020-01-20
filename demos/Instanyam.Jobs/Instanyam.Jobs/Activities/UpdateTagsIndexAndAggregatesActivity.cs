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
    public static class UpdateTagsIndexAndAggregatesActivity
    {
        [FunctionName(Constants.FunctionsNaming.UpdateTagsIndexAndAggregatesActivity)]
        public static async Task<string> UpdateTagsIndexAndAggregates([ActivityTrigger] string tag, ILogger log)
        {
            log.LogInformation($"Updating Tags search index and other stats for tag: {tag}");

            // simulate updating aggregates and search/db indexes for the Tag passed
            await Task.Delay(500);

            return $"Indexes and Aggregates updated for tag: {tag}";
        }
    }
}
