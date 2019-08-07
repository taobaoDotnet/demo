using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servicedemo.models.dto.response
{
    public class UserList
    {
        /// <summary>
        /// id
        /// </summary>
        public long userId { get; set; }

        /// <summary>
        ///  姓名
        /// </summary>
        public string name { get; set; }

    }
}
