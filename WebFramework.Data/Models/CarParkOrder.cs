using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Data.Models
{
    [Table("CarParkOrder")]
    public partial class CarParkOrder
    {
        public long id { get; set; }
        public string qrInfo { get; set; }
        public long userId { get; set; }
        public string carParkName { get; set; }
        public decimal price { get; set; }
        public string carNumber { get; set; }
        public System.DateTime dateStart { get; set; }
        public Nullable<System.DateTime> dateEnd { get; set; }
        public string times { get; set; }
        public Nullable<decimal> totalAmount { get; set; }
        public bool isPaid { get; set; }
    }
}
