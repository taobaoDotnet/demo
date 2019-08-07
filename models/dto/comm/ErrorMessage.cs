using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servicedemo.models.dto.comm
{
    public class ErrorMessage
    {
        public int code { get; set; }

        public string msg { get; set; }

        public string requestMethod { get; set; }

        public string requestUrl { get; set; }

        public object requestData { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

