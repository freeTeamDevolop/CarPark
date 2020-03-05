using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFramework.Data.Models
{
    [Table("ThurSetting")]
    public partial class ThurSetting
    {
        public long id { get; set; }
        public string qrInfo { get; set; }
        public System.TimeSpan timeStart { get; set; }
        public System.TimeSpan timeEnd { get; set; }
        public decimal price { get; set; }
    }
}
