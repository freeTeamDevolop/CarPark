using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebFramework.Framework.Api.Controllers;
using WebFramework.Framework.Data;
using WebFramework.WebApiControllers.Services.CarPark;

namespace WebFramework.WebApiControllers.Controllers
{
    public class CarParkController:ApiCoreController
    {
        public ICarParkService _CarParkService;
        public CarParkController(ICarParkService carParkService)
        {
            _CarParkService = carParkService;
        }

        [HttpPost]
        public RequestResult<string> CarPark()
        {
            return F(() => _CarParkService.CarPark());
        }
    }
}
