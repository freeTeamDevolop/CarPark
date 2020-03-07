using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.WebApiControllers.ViewModels.CarPark
{
    public class CarParkSettingModel
    {
        public string week { get; set; }

        public string carParkName { get; set; }

        public string qrInfo { get; set; }


        public List<AreaSetting> areaSettings { get; set; }
    }

    public class AreaSetting
    {
        public long id { get; set; }

        public System.TimeSpan timeStart { get; set; }

        public System.TimeSpan timeEnd { get; set; }

        public decimal price { get; set; }
    }
}
