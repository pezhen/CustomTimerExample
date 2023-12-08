using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SingletonTimer;
using TimerInfo = Common.TimerInfo;

namespace InProcessFunction
{
    public class InProcessFunction
    {
        [FunctionName("InProcessFunction")]
        public void Run([SingletonTimerTrigger("0 */5 * * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
