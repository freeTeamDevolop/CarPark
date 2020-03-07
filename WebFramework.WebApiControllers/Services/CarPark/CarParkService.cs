using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.WebApiControllers.Services.CarPark
{
    public class CarParkService:ICarParkService
    {
        public CarParkService()
        {

        }

        public string CarPark()
        {
            return "CarPark";
        }

    }
}
