﻿using Consul;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Consul.Core.Wrappers
{
    internal sealed class ConsulClientFactory : IConsulClientFactory
    {
        private readonly IConsulConfigurationSource _consulConfigSource;

        public ConsulClientFactory(IConsulConfigurationSource consulConfigSource)
        {
            _consulConfigSource = consulConfigSource;
        }

        public IConsulClient Create()
        {
            return new ConsulClient(
                _consulConfigSource.ConsulConfigurationOptions,
                _consulConfigSource.ConsulHttpClientOptions,
                _consulConfigSource.ConsulHttpClientHandlerOptions);
        }
    }
}
