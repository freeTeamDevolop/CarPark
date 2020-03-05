using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.WebApiControllers.ViewModels.CarPark
{
    public class ScanCodeResult
    {
        public string carParkName { get; set; }

        public decimal price { get; set; }

        public string qrInfo { get; set; }
    }
}
