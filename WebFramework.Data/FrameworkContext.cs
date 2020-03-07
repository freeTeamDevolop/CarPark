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

        public virtual DbSet<Users> Users { get; set; }
    }
}
