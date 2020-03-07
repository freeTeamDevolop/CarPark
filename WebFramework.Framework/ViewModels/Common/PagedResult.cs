using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Framework.ViewModels.Common
{
    /// <summary>
    /// 带总条数返回列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        public int total { get; set; }
        public List<T> data { get; set; }
    }

    public class PagedResult<T, K>
    {
        public int total { get; set; }
        public List<T> data { get; set; }

        public K totalData { get; set; }
    }
}
