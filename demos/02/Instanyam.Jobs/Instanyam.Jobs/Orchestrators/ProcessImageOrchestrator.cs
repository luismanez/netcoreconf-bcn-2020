using System;
using Instanyam.Jobs.Common;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Instanyam.Jobs.Models;

namespace Instanyam.Jobs.Orchestrators
{
    public static class ProcessImageOrchestrator
    {
        [FunctionName(Constants.FunctionsNaming.ProcessImageOrchestrator)]
        public static async Task<ProcessImageResult> ProcessImage(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var image = context.GetInput<string>();

            // Sub Orchestrator with fan-out / fan-in pattern 
            // It gets the image tags and Update index info for each tag in parallel
            var indexTagResults = await context.CallSubOrchestratorAsync<List<string>>(
                    Constants.FunctionsNaming.ProcessImageTagsSubOrchestrator, 
                    image);

            var imageApproved = true;
            if (!indexTagResults.Contains("food"))
            {
                // Human interaction pattern: we wait for Image human review
                // usually, we here send first some kind of Notification that Human approval is required
                // and then we wait for an External Event (or timeout)
                using (var timeoutCts = new CancellationTokenSource())
                {
                    var dueTime = context.CurrentUtcDateTime.AddSeconds(30); // just for demo
                    var durableTimeout = context.CreateTimer(dueTime, timeoutCts.Token);

                    var approvalEvent = context.WaitForExternalEvent<bool>("ApprovalEvent");
                    if (approvalEvent == await Task.WhenAny(approvalEvent, durableTimeout))
                    {
                        timeoutCts.Cancel();
                        imageApproved = approvalEvent.Result;
                    }
                    else
                    {
                        imageApproved = false; // Monitor Timed out (we consider Image is not approved)
                    }
                }
            }

            if (!imageApproved)
            {
                var imageRejectedNotification = await context.CallActivityAsync<string>(
                    Constants.FunctionsNaming.SendRejectNotificationActivity, 
                    image);

                return new ProcessImageResult
                {
                    ImageName = image,
                    ImageSize = "0x0",
                    ImageTags = indexTagResults,
                    ErrorDetail = imageRejectedNotification,
                    ResultType = ProcessImageResultType.Rejected
                };
            }

            // At this point we´ve already processed Image tags, and the Image is validated (it´s a foodie Image!)
            // Next, we resize the image
            var resize = await context.CallActivityAsync<string>(Constants.FunctionsNaming.ResizeImageActivity, image);

            // Process is done. Send a notification and Orchestrator is done
            var notification = await context.CallActivityAsync<string>(Constants.FunctionsNaming.SendNotificationActivity, image);

            return new ProcessImageResult
            {
                ImageName = image,
                ImageTags = indexTagResults,
                ErrorDetail = string.Empty,
                ResultType = ProcessImageResultType.Processed,
                ImageSize = resize
            };
        }
    }
}