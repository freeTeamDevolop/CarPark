using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Framework.Configuration
{
    public static class CultureConfig
    {
        public static readonly List<string> ValidCultures = new List<string> { "zh-tw", "zh-cn" };

        private static readonly List<string> Cultures = new List<string>
        {
            "zh-tw",
            "zh-cn"
        };
    }
}
