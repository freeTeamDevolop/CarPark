using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Infrastructure.Configuration;
using WebFramework.Infrastructure.Ulitily.Security;

namespace WebFramework.Data.Infrastructure
{
    public static class ConnectionString
    {
        public static string GetConnectionString(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            var obj = ConfigurationLoader.LoadByJson("db");
            if (obj == null)
                throw new Exception("db.json has error");
            var prex = "";
            if (appSettings["dev"] == "1")
            {
                prex = "dev_";
            }
            var connectionString = obj.GetValue(prex + key).ToString();
            connectionString = ReversibleAlgorihm.DecryptStringAes(connectionString);
            return connectionString;
        }
    }
}
