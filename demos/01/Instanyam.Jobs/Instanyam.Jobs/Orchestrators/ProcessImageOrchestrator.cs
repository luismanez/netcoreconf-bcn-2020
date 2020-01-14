using Instanyam.Jobs.Common;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Instanyam.Jobs.Orchestrators
{
    public static class ProcessImageOrchestrator
    {
        [FunctionName(Constants.FunctionsNaming.ProcessImageOrchestrator)]
        public static async Task<List<string>> ProcessImage(
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