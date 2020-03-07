using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.WebApiControllers.ViewModels.CarPark
{
    public class CarParkSettingListResult
    {
        public long id { get; set; }

        public string qrInfo { get; set; }

        public TimeSpan timeStart { get; set; }

        public TimeSpan timeEnd { get; set; }

        public decimal price { get; set; }
    }
}
