using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Instanyam.Jobs.Common;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Instanyam.Jobs.Orchestrators
{
    public static class ProcessImageOrchestrator
    {
        [FunctionName("ProcessImageOrchestrator")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var image = context.GetInput<string>();

            var outputs = new List<string>
            {
                await context.CallActivityAsync<string>(Constants.FunctionsNaming.GetTagsActivity, image),
                await context.CallActivityAsync<string>(Constants.FunctionsNaming.ResizeImageActivity, image),
                await context.CallActivityAsync<string>(Constants.FunctionsNaming.SendNotificationActivity, image),
            };

            return outputs;
        }
    }
}