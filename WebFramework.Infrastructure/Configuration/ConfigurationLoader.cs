using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Infrastructure.Configuration
{
    public static class ConfigurationLoader
    {
        static ConcurrentDictionary<string, string> ConfigsCache = new ConcurrentDictionary<string, string>();

        public static string ReadConfiguration(string filename, string ext, bool cached = true)
        {
            Func<string, string> builder = s =>
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs", s + "." + ext);
                return File.ReadAllText(filePath);
            };
            string text = "";
            if (cached)
                text = ConfigsCache.GetOrAdd(filename, builder);
            else
                text = builder(filename);
            return text;
        }

        public static JObject LoadByJson(string filename, bool cached = true)
        {
            var json = ReadConfiguration(filename, "json", cached);
            var obj = JsonConvert.DeserializeObject(json) as JObject;
            return obj;
        }
    }
}
