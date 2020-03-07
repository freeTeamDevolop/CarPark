using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebFramework.Framework.Api.Controllers;
using WebFramework.Framework.Data;
using WebFramework.WebApiControllers.Services.CarPark;
using WebFramework.WebApiControllers.ViewModels.CarPark;

namespace WebFramework.WebApiControllers.Controllers
{
    [RoutePrefix("api/CarParkProject/CarPark")]
    public class CarParkController:ApiCoreController
    {
        private readonly ICarParkService _carParkService;
        public CarParkController(ICarParkService carParkService)
        {
            _carParkService = carParkService;
        }

        [HttpPost]
        [Route("CarParkSetting")]
        public RequestResult<bool> CarParkSetting(CarParkSettingModel model)
        {
            return F(() => _carParkService.CarParkSetting(model));
        }

        [HttpPost]
        [Route("GetScanCodeResult")]
        public RequestResult<ScanCodeResult> GetScanCodeResult(CarParkGetViewModel model)
        {
            return F(() => _carParkService.GetScanCodeResult(model));
        }

        [HttpPost]
        [Route("GetCarParkSettingList")]
        public RequestResult<List<CarParkSettingListResult>> GetCarParkSettingList(CarParkSettingListViewModel model)
        {
            return F(() => _carParkService.GetCarParkSettingList(model));
        }
    }
}
