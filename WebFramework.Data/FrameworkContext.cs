using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Data.Models;

namespace WebFramework.Data
{
    public class FrameworkContext: DbContext
    {
        public FrameworkContext(string s)
            : base(s)
        {

        }

        public virtual DbSet<CarParkOrder> CarParkOrder { get; set; }
        public virtual DbSet<CarParkSetting> CarParkSetting { get; set; }
        public virtual DbSet<FriSetting> FriSetting { get; set; }
        public virtual DbSet<MonSetting> MonSetting { get; set; }
        public virtual DbSet<SatSetting> SatSetting { get; set; }
        public virtual DbSet<SunSetting> SunSetting { get; set; }
        public virtual DbSet<ThurSetting> ThurSetting { get; set; }
        public virtual DbSet<TuesSetting> TuesSetting { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WedSetting> WedSetting { get; set; }

        public virtual DbSet<WfAppKey> WfAppKey { get; set; }
    }
}
