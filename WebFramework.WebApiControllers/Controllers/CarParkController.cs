using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebFramework.Framework.Api.Controllers;
using WebFramework.Framework.Api.Filters;
using WebFramework.Framework.AspNet.Filters;
using WebFramework.Framework.Data;
using WebFramework.WebApiControllers.Extension;
using WebFramework.WebApiControllers.Services.CarPark;
using WebFramework.WebApiControllers.ViewModels.CarPark;

namespace WebFramework.WebApiControllers.Controllers
{
    [RoutePrefix("api/CarParkProject/CarPark")]
    public class CarParkController: WebAuthorityApiController
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
        [LocalAccess]
        [CheckInputModelNullFilter]
        public RequestResult<ScanCodeResult> GetScanCodeResult(CarParkGetViewModel model)
        {
            return F(() => _carParkService.GetScanCodeResult(model));
        }

        [HttpPost]
        [Route("GetCarParkSettingList")]
        [NonAuthority]
        //[LocalAccess]
        //[CheckInputModelNullFilter]
        public RequestResult<List<CarParkSettingListResult>> GetCarParkSettingList(CarParkSettingListViewModel model)
        {
            return F(() => _carParkService.GetCarParkSettingList(model));
        }
    }
}
