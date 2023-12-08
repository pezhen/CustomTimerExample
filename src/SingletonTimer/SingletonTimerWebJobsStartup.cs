// <copyright file="ExtensionsWebJobsStartup.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using SingletonTimer;
using SingletonTimer.Config;

//new add class
[assembly: WebJobsStartup(typeof(SingletonTimerWebJobsStartup))]

namespace SingletonTimer
{
    public class SingletonTimerWebJobsStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddSingletonTimer();
        }
    }
}
