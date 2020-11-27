using Consul;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Consul.Core
{
    public class ConsulServiceRegister : IConsulServiceRegister
    {
        private readonly ConsulOptions _options;

        public ConsulServiceRegister(IOptions<ConsulOptions> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// 服务注册
        /// </summary>
        public void ConsulRegister()
        {
            ConsulClient client = new ConsulClient(
                (ConsulClientConfiguration c) =>
                {
                    c.Address = new Uri(_options.Address); //Consul 服务中心地址
                    c.Datacenter = _options.DataCenter; //指定数据中心，如果未提供，则默认为代理的数据中心
                }
            );

            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID=Guid.NewGuid().ToString(), //服务编号，不可重复
                Name=_options.ServiceName,  //服务名称
                Port=_options.ServicePort,  // 本程序的端口号
                Address=_options.ServiceIP, //本程序的IP地址
                Check=new AgentServiceCheck { 
                    DeregisterCriticalServiceAfter = TimeSpan.FromMilliseconds(1), //服务地址后多久注销
                    Interval = TimeSpan.FromSeconds(5), //服务健康检查间隔
                    Timeout = TimeSpan.FromSeconds(5), //检查超时的事件
                    HTTP = $"http://{_options.ServiceIP}:{_options.ServicePort}{_options.CheckUrl}" //检查的地址
                }
            });

        }
    }
}
