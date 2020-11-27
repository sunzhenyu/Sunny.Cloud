using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Consul.Core
{
    public interface IConsulConfigurationWrapper
    {
        bool Set(string key, string value);


        object Get(string key);
    }
}
