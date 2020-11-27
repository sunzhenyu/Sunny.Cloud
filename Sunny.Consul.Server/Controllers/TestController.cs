using Microsoft.AspNetCore.Mvc;
using Sunny.Consul.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunny.Consul.Server.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly IConsulServiceWrapper _consulServiceWrapper;

        public TestController(IConsulServiceWrapper consulServiceWrapper) {
            _consulServiceWrapper = consulServiceWrapper;
        }

        [HttpGet("traffer")]
        public string Traffer(string serviceName) {
            var service = _consulServiceWrapper.GetService(serviceName);

            return _consulServiceWrapper.Get($"http://{service}/WeatherForecast");
        }
    }
}
