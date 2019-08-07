using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servicedemo.models.dto.request
{
    public class UserDelete
    {
        public List<long> ids { get; set; }
    }
}
