using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servicedemo.models.cache
{
    public class SessionInfo
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string CCCNCode { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Source { get; set; }
        public string AreaCode { get; set; }
        public int UserType { get; set; }
        public object LockDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int CredentialsType { get; set; }
        public string CredentialsCode { get; set; }
        public int UserStatus { get; set; }
    }
}
