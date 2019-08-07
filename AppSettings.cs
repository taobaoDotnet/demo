using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servicedemo
{
    public class AppSettings
    {
        /// <summary>
        /// service 数据库连接
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; set; }
        /// <summary>
        /// client 服务发现
        /// </summary>
        public ServiceDiscovery ServiceDiscovery { get; set; }
    }

    /// <summary>
    /// service 数据库连接
    /// </summary>
    public class ConnectionStrings
    {
        public string MySqlDemo { get; set; }
        public string MsSqlDemo { get; set; }
        public string RedisDemo { get; set; }
    }

    /// <summary>
    /// client 服务发现
    /// </summary>
    public class ServiceDiscovery
    {
        public string Authentication { get; set; }
        public string Authorization { get; set; }
        public string BaseXxxWrite { get; set; }
        public string BaseXxxRead { get; set; }
    }


    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string System { get; set; }
        public string Microsoft { get; set; }
    }
}
