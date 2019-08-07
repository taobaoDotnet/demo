using servicedemo.models.dto.comm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace servicedemo.models.dto.wrapper
{
    public class PageRequestT<TData>
    {
        /// <summary>
        /// 区域编码
        /// </summary>
        [Required(ErrorMessage = "请输入区域编码")]
        public string areaCode { get; set; }

        /// <summary>
        /// 筛选条件
        /// </summary>
        public TData filterData { get; set; }

        /// <summary>
        /// 分页数据
        /// </summary>
        public PageInfoRequest pageData { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
