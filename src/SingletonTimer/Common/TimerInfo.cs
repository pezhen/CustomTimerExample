// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.


namespace Common
{
    /// <summary>
    /// Provides access to timer schedule information for jobs triggered 
    /// by <see cref="SingletonTimerTriggerAttribute"/>.
    /// </summary>
    public class TimerInfo
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="status">The current schedule status.</param>
        /// <param name="isPastDue">True if the schedule is past due, false otherwise.</param>
        public TimerInfo(ScheduleStatus status, bool isPastDue = false)
        {
            ScheduleStatus = status;
            IsPastDue = isPastDue;
        }

        /// <summary>
        /// Gets the current schedule status for this timer.
        /// If schedule monitoring is not enabled for this timer (see <see cref="SingletonTimerTriggerAttribute.UseMonitor"/>)
        /// this property will return null.
        /// </summary>
        public ScheduleStatus ScheduleStatus { get; set; }

        /// <summary>
        /// Gets a value indicating whether this timer invocation
        /// is due to a missed schedule occurrence.
        /// </summary>
        public bool IsPastDue { get; set; }
    }
}
