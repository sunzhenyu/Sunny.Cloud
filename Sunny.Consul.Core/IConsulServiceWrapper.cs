using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sunny.Consul.Core
{
    public interface IConsulServiceWrapper
    {
        string GetService(string serviceName);

        Task<string> GetServiceAsync(string serviceName);

        string Get(string url);

        T Get<T>(string url);
    }
}
