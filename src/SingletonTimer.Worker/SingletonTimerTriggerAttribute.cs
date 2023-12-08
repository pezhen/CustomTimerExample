﻿// ----------------------------------------------------------------
// <copyright file="SingletonTimerTriggerAttribute.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------

namespace SingletonTimer.Worker
{
    using System;
    using Microsoft.Azure.Functions.Worker.Converters;
    using Microsoft.Azure.Functions.Worker.Extensions.Abstractions;

    /// <summary>
    /// Attribute used to mark a job function that should be invoked periodically based on
    /// a timer schedule. The trigger parameter type must be <see cref="TimerInfo"/>.
    /// </summary>
    //[InputConverter(typeof(TimerInfoConverter))]
    //[ConverterFallbackBehavior(ConverterFallbackBehavior.Default)]
    [BindingCapabilities(KnownBindingCapabilities.FunctionLevelRetry)]
    public sealed class SingletonTimerTriggerAttribute : TriggerBindingAttribute
    {
        /// <summary>
        /// Constructs a new instance based on the schedule expression passed in.
        /// </summary>
        /// <param name="scheduleExpression">A schedule expression. This can either be a 6 field crontab expression
        /// <a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer#cron-expressions"/> or a <see cref="TimeSpan"/>
        /// string (e.g. "00:30:00"). On Azure Functions, a TimeSpan string is only supported 
        /// when running on an App Service Plan.</param>
        public SingletonTimerTriggerAttribute(string scheduleExpression)
        {
            ScheduleExpression = scheduleExpression;
            UseMonitor = true;
        }

        /// <summary>
        /// Constructs a new instance using the specified <see cref="TimerSchedule"/> type.
        /// </summary>
        /// <param name="scheduleType">The type of schedule to use.</param>
        public SingletonTimerTriggerAttribute(Type scheduleType)
        {
            ScheduleType = scheduleType;
            UseMonitor = true;
        }

        /// <summary>
        /// Gets the schedule expression.
        /// </summary>
        public string ScheduleExpression { get; private set; }

        /// <summary>
        /// Gets the <see cref="TimerSchedule"/> type.
        /// </summary>
        public Type ScheduleType { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the schedule should be monitored.
        /// Schedule monitoring persists schedule occurrences to aid in ensuring the
        /// schedule is maintained correctly even when roles restart.
        /// If not set explicitly, this will default to true for schedules that have a recurrence
        /// interval greater than 1 minute (i.e., for schedules that occur more than once
        /// per minute, persistence will be disabled).
        /// </summary>
        public bool UseMonitor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the function should be invoked
        /// immediately on startup. After the initial startup run, the function will
        /// be run on schedule thereafter.
        /// </summary>
        public bool RunOnStartup { get; set; }
    }
}
