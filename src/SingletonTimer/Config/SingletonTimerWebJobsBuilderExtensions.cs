// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using SingletonTimer.Schedulling;

//namespace Microsoft.Extensions.Hosting
namespace SingletonTimer.Config
{
    /// <summary>
    /// Extension methods for Timers integration
    /// </summary>
    public static class SingletonTimerWebJobsBuilderExtensions
    {
        /// <summary>
        /// Adds the Timer extension to the provided <see cref="IWebJobsBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IWebJobsBuilder"/> to configure.</param>
        public static IWebJobsBuilder AddSingletonTimer(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddExtension<SingletonTimersExtensionConfigProvider>()
                .BindOptions<SingletonTimersOptions>();
            builder.Services.AddSingleton<ScheduleMonitor>(serviceProvider => new FileSystemScheduleMonitor());
            return builder;
        }

        /// <summary>
        /// Adds the Timer extension to the provided <see cref="IWebJobsBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IWebJobsBuilder"/> to configure.</param>
        /// <param name="configure">An <see cref="Action{TimersOptions}"/> to configure the provided <see cref="SingletonTimersOptions"/>.</param>
        /// <remarks>Currently there are no configurable options on <see cref="SingletonTimersOptions"/> so this overload does not provide any utility.</remarks>
        public static IWebJobsBuilder AddSingletonTimer(this IWebJobsBuilder builder, Action<SingletonTimersOptions> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddSingletonTimer();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
