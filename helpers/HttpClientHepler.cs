using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace helper
{
    public class ClientRequestHepler
    {
        public static string PostJson(string url, object obj)
        {
            try
            {
                var msg = url.PostJsonAsync(obj);
                msg.Wait();
                var str = msg.Result.Content.ReadAsStringAsync();
                str.Wait();
                return str.Result;
            }
#pragma warning disable CS0168 // 声明了变量“ex”，但从未使用过
            catch(Exception ex)
#pragma warning restore CS0168 // 声明了变量“ex”，但从未使用过
            {
                // “url”和“obj”无法返回“服务消费者”
                return null;
            }
        }
    }
}
