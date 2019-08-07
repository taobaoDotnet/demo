using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servicedemo.clients
{
    /// <summary>
    /// 服务发现
    /// 服务的地址可以存于consul、appconfig、redis、configmap。
    /// </summary>
    public  class ServiceDiscoveryImpl: ServiceDiscovery
    {
        private readonly IConfiguration Configuration;
        ServiceDiscoveryImpl(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// koofang service host
        /// </summary>
        /// <returns></returns>
        public string GetKoofangService()
        {
            return getKoofangServiceByCfg();
        }
        /// <summary>
        /// kooboss server host
        /// </summary>
        /// <returns></returns>
        public string getKoobossService()
        {
            return getKoofangServiceByCfg();
        }

        private string getKoofangServiceByRedis()
        {
            // 用HASH存储
            return RedisHelper.Get("KoofangService");
        }
        private string getKoofangServiceByCfg()
        {
            return Configuration["ServiceDiscovery:Koofang"].ToString();
        }
    }
}
