using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Instanyam.Jobs.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Instanyam.Jobs.Starters
{
    public static class ProcessImageStarter
    {
        [FunctionName(Constants.FunctionsNaming.ProcessImageStarter)]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] 
            HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            log.LogInformation("Process Image starting...");

            string image = req.Query["image"];

            if (string.IsNullOrEmpty(image))
            {
                return new BadRequestResult();
            }

            log.LogInformation($"About to start orchestration for {image}");

            const string instanceId = "975c3b80-b0c2-47cc-b045-388a0dc2a7f1";
            
            await starter.StartNewAsync(
                Constants.FunctionsNaming.ProcessImageOrchestrator, 
                instanceId, 
                image);

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
