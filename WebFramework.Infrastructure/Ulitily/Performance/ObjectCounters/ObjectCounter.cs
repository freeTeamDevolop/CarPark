using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Infrastructure.Ulitily.Performance.ObjectCounters
{
    public class ObjectCounter
    {
        public static ConcurrentDictionary<int, DbContextData> Records = new ConcurrentDictionary<int, DbContextData>();

        public static void IncreseDbContextCount(int k, string v)
        {
            Records.TryAdd(k, new DbContextData() { ConnectionString = v, CreateDate = DateTime.Now });
        }

        public static void DecreseDbContextCount(int k)
        {
            DbContextData v = null;
            Records.TryRemove(k, out v);
        }
    }

    public class DbContextData
    {
        public string ConnectionString { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
