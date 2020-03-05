using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFramework.Data.Models
{
    [Table("CarParkSetting")]
    public partial class CarParkSetting
    {
        public long id { get; set; }
        public string carParkName { get; set; }
        public string qrInfo { get; set; }
    }
}
