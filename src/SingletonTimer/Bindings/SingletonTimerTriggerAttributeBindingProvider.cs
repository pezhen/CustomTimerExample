// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Triggers;
using Microsoft.Extensions.Logging;
using SingletonTimer.Config;
using SingletonTimer.Schedulling;
using TimerInfo = Common.TimerInfo;

namespace SingletonTimer.Bindings
{
    internal class SingletonTimerTriggerAttributeBindingProvider : ITriggerBindingProvider
    {
        private readonly SingletonTimersOptions _options;
        private readonly INameResolver _nameResolver;
        private readonly ILogger _logger;
        private readonly ScheduleMonitor _scheduleMonitor;

        public SingletonTimerTriggerAttributeBindingProvider(SingletonTimersOptions options, INameResolver nameResolver, ILogger logger, ScheduleMonitor scheduleMonitor)
        {
            _options = options;
            _nameResolver = nameResolver;
            _logger = logger;
            _scheduleMonitor = scheduleMonitor;
        }

        public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            ParameterInfo parameter = context.Parameter;
            SingletonTimerTriggerAttribute timerTriggerAttribute = parameter.GetCustomAttribute<SingletonTimerTriggerAttribute>(inherit: false);

            if (timerTriggerAttribute == null)
            {
                return Task.FromResult<ITriggerBinding>(null);
            }

            if (parameter.ParameterType != typeof(TimerInfo))
            {
                throw new InvalidOperationException(string.Format("Can't bind SingletonTimerTriggerAttribute to type '{0}'.", parameter.ParameterType));
            }

            TimerSchedule schedule = TimerSchedule.Create(timerTriggerAttribute, _nameResolver, _logger);

            return Task.FromResult<ITriggerBinding>(new SingletonTimerTriggerBinding(parameter, timerTriggerAttribute, schedule, _options, _logger, _scheduleMonitor));
        }
    }
}
