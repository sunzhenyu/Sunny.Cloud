using Consul;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sunny.Consul.Configuration.Controllers
{
    [Route("[controller]")]
    public class ConfigController : Controller
    {
        private readonly IConfiguration _configuration;

        public ConfigController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet("get")]
        public string Get(string key) {
            return _configuration[key] ?? "";
        }

        [HttpPost("set")]
        public async Task<bool> Set([FromBody] SetConsul request) {
            var client = new ConsulClient(x => x.Address = new Uri(_configuration["ConsulOptions:Address"]));

            var result = await client.KV.Put(new KVPair(request.Key) {Value=Encoding.UTF8.GetBytes(request.Json) });

            return result.Response;
        }
    }

    public class SetConsul{
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("json")]
        public string Json { get; set; }
    }
}
