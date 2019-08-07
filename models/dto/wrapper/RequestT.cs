using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace servicedemo.models.dto.wrapper
{
    public class RequestT<TData>
    {
        /// <summary>
        /// 区域编码
        /// </summary>
        [Required(ErrorMessage = "请输入区域编码")]
        // [RegularExpression(@"^1[3458][0-9]{9}$", ErrorMessage = "手机号格式不正确")]
        public string areaCode { get; set; }

        /// <summary>
        /// 服务消费者名称
        /// </summary>
        public string clientName { get; set; }

        /// <summary>
        /// 筛选条件
        /// </summary>
        public TData data { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}