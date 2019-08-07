using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servicedemo.models.dto.response
{
    public class UserDetail
    {
        /// <summary>
        /// id
        /// </summary>
        public long userId { get; set; }

        /// <summary>
        ///  姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 手机:隐私字段
        /// </summary>
        private string phone { get; set; }
    }
}
