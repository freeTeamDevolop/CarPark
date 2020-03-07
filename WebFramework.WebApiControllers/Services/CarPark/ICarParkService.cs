using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Framework.Data;
using WebFramework.WebApiControllers.ViewModels.CarPark;

namespace WebFramework.WebApiControllers.Services.CarPark
{
    public interface ICarParkService
    {
        RequestResult<bool> CarParkSetting(CarParkSettingModel model);

        RequestResult<ScanCodeResult> GetScanCodeResult(CarParkGetViewModel model);

        RequestResult<List<CarParkSettingListResult>> GetCarParkSettingList(CarParkSettingListViewModel model);
    }
}
