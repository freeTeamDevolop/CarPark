using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Framework.Types.Models
{
    public class WfRequestContext
    {
        public string ClientIp { get; set; }
        public string AppIp { get; set; }
        public string EntryDomain { get; set; }
        public string Context { get; set; }
        public DateTime CallStart { get; set; }
        public double RequestTimeCast { get; set; }
        public int ClientTimeZone { get; set; }
        public string ClientVer { get; set; }
        public string AppKey { get; set; }
        public string UserAgent { get; set; }
        public string Terminal { get; set; }
        public string ApiRole { get; set; }
        public string RequestController { get; set; }
        public string RequestAction { get; set; }
        public string RequestResult { get; set; }
        public bool AppendBlockLog { get; set; }
        public string Lang { get; set; }
        public string Body { get; set; }

        public string redisLockKey { get; set; }

        public string token { get; set; }
    }
}
