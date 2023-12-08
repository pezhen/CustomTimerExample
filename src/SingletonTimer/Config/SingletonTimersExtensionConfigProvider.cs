// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SingletonTimer.Bindings;
using SingletonTimer.Schedulling;

namespace SingletonTimer.Config
{
    [Extension("Timers")]
    internal class SingletonTimersExtensionConfigProvider : IExtensionConfigProvider
    {
        private readonly IOptions<SingletonTimersOptions> _options;
        private readonly ILoggerFactory _loggerFactory;
        private readonly INameResolver _nameResolver;
        private readonly ScheduleMonitor _scheduleMonitor;

        public SingletonTimersExtensionConfigProvider(IOptions<SingletonTimersOptions> options, ILoggerFactory loggerFactory,
            INameResolver nameResolver, ScheduleMonitor scheduleMonitor)
        {
            _options = options;
            _loggerFactory = loggerFactory;
            _nameResolver = nameResolver;
            _scheduleMonitor = scheduleMonitor;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            ILogger logger = _loggerFactory.CreateLogger(LogCategories.CreateTriggerCategory("Timer"));
            var bindingProvider = new SingletonTimerTriggerAttributeBindingProvider(_options.Value, _nameResolver, logger, _scheduleMonitor);

            context.AddBindingRule<SingletonTimerTriggerAttribute>()
                .BindToTrigger(bindingProvider);
        }
    }
}
