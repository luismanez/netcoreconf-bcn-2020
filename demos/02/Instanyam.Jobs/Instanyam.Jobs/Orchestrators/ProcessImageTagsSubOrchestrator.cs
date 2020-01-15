using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instanyam.Jobs.Common;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace Instanyam.Jobs.Orchestrators
{
    public static class ProcessImageTagsSubOrchestrator
    {
        [FunctionName(Constants.FunctionsNaming.ProcessImageTagsSubOrchestrator)]
        public static async Task<List<string>> ProcessImageTags(
            [OrchestrationTrigger] IDurableOrchestrationContext ctx,
            ILogger log)
        {
            var image = ctx.GetInput<string>();
            
            var tags = await ctx.CallActivityAsync<List<string>>(Constants.FunctionsNaming.GetTagsActivity, image);

            var indexTagTasks = new List<Task<string>>();
            foreach (var tag in tags)
            {
                var task = ctx.CallActivityAsync<string>(Constants.FunctionsNaming.UpdateTagsIndexAndAggregatesActivity, tag);
                indexTagTasks.Add(task);
            }

            await Task.WhenAll(indexTagTasks);
            
            return tags;
        }
    }
}
