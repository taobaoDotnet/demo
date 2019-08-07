
using System.Collections.Generic;

namespace servicedemo.models.dto.wrapper
{
    using enums;
    using servicedemo.models.dto.comm;

    public class PageResponseT<TData>
    {
        /// <summary>
        /// 构造默认值
        /// </summary>
       public PageResponseT()
        {
            code = CodeEnum.Success;
            msg = CodeEnum.Success.Description();
        }

        /// <summary>
        /// 返回代码
        /// </summary>
        public CodeEnum code { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 通用返回数据
        /// </summary>
        public IEnumerable<TData> dataList { get; set; }

        /// <summary>
        /// 分页数据
        /// </summary>
        public PageInfoResponse pageData { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
