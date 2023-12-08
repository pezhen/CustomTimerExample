using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SingletonTimer.Worker;
using TimerInfo = Common.TimerInfo;

namespace WorkerFunction
{
    public class IsoLatedFunction
    {
        private readonly ILogger _logger;

        public IsoLatedFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<IsoLatedFunction>();
        }

        [Function("IsoLatedFunction")]
        public void Run([SingletonTimerTrigger("0 */5 * * * *", RunOnStartup = true)] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
