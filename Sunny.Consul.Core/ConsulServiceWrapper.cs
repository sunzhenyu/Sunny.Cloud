using Consul;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sunny.Consul.Core
{
    public class ConsulServiceWrapper : IConsulServiceWrapper
    {
        private readonly ConsulOptions _options;

        public ConsulServiceWrapper(IOptions<ConsulOptions> options)
        {
            _options = options.Value;
        }

        public string Get(string url)
        {
            using (HttpClient client = new HttpClient()) {
                return client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            }
        }

        public T Get<T>(string url)
        {
            return JsonConvert.DeserializeObject<T>(Get(url));
        }

        public string GetService(string serviceName)
        {
            var client = new ConsulClient(x => x.Address = new Uri(_options.Address));
            var response = client.Agent.Services().Result.Response;

            //服务名称区分大小写
            var services = response.Where(x => x.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).Select(x => x.Value);

            if (services.Count() < 1) {
                return string.Empty;
            }
            //进行取模，随机取得一个服务器，或者使用其它负载均衡策略
            var service = services.ElementAt(Environment.TickCount % services.Count());

            return $"{service.Address}:{service.Port}";
        }

        public async Task<string> GetServiceAsync(string serviceName)
        {
            var client = new ConsulClient(x => x.Address = new Uri(_options.Address));
            var response = (await client.Agent.Services()).Response;

            //服务名称区分大小写
            var services = response.Where(x => x.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).Select(x => x.Value);

            if (services.Count() < 1)
            {
                return string.Empty;
            }
            //进行取模，随机取得一个服务器，或者使用其它负载均衡策略
            var service = services.ElementAt(Environment.TickCount % services.Count());

            return $"{service.Address}:{service.Port}";
        }
    }
}
