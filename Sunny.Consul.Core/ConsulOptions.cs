using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Consul.Core
{
    public class ConsulOptions
    {
        public string Address { get; set; }

        public string CheckUrl { get; set; }

        public string DataCenter { get; set; }

        public string ServiceIP { get; set; }

        public int ServicePort { get; set; }

        public string ServiceName { get; set; }
    }
}
