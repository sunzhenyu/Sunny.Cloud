using Microsoft.Extensions.Configuration;
using Sunny.Consul.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Consul.Core
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder builder, string key) {
            return builder.AddConsul(key, options => { });
        }

        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder builder, string key, Action<IConsulConfigurationSource> options) {
            var consulConfigSource = new ConsulConfigurationSource(key);
            options(consulConfigSource);
            return builder.Add(consulConfigSource);
        }
    }
}
