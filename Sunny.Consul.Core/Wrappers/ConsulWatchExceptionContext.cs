﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Consul.Core.Wrappers
{
    /// <summary>
    ///     Contains information about a consul load exception.
    /// </summary>
    public sealed class ConsulWatchExceptionContext
    {
        internal ConsulWatchExceptionContext(
            Exception exception,
            int consecutiveFailureCount,
            IConsulConfigurationSource source)
        {
            Exception = exception;
            ConsecutiveFailureCount = consecutiveFailureCount;
            Source = source;
        }

        /// <summary>
        ///     Gets the number of consecutive failures that have occurred while watching for configuration changes.
        /// </summary>
        /// <remarks>
        ///     This can be used to vary the time between retries, for example to create an exponential back-off algorithm.
        /// </remarks>
        public int ConsecutiveFailureCount { get; }

        /// <summary>
        ///     Gets the <see cref="Exception" /> that occured.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        ///     Gets the <see cref="IConsulConfigurationSource" /> of the provider that caused the exception.
        /// </summary>
        public IConsulConfigurationSource Source { get; }
    }
}
