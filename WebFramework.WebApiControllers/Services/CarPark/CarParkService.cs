using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Data.Infrastructure;
using WebFramework.Data.Models;
using WebFramework.Framework.Data;
using WebFramework.WebApiControllers.ViewModels.CarPark;

namespace WebFramework.WebApiControllers.Services.CarPark
{
    public class CarParkService:ICarParkService
    {
        private readonly IRepository<CarParkSetting> _carParkSettingRepo;
        private readonly IRepository<MonSetting> _monSettingRepo;
        private readonly IRepository<TuesSetting> _tuesSettingRepo;
        private readonly IRepository<WedSetting> _wedSettingRepo;
        private readonly IRepository<ThurSetting> _thurSettingRepo;
        private readonly IRepository<FriSetting> _friSettingRepo;
        private readonly IRepository<SatSetting> _satSettingRepo;
        private readonly IRepository<SunSetting> _sunSettingRepo;

        public CarParkService(IRepository<CarParkSetting> _carParkSettingRepo, IRepository<MonSetting> _monSettingRepo, IRepository<TuesSetting> _tuesSettingRepo, IRepository<WedSetting> _wedSettingRepo, IRepository<ThurSetting> _thurSettingRepo,
            IRepository<FriSetting> _friSettingRepo, IRepository<SatSetting> _satSettingRepo, IRepository<SunSetting> _sunSettingRepo)
        {
            this._carParkSettingRepo = _carParkSettingRepo;
            this._monSettingRepo = _monSettingRepo;
            this._tuesSettingRepo = _tuesSettingRepo;
            this._wedSettingRepo = _wedSettingRepo;
            this._thurSettingRepo = _thurSettingRepo;
            this._friSettingRepo = _friSettingRepo;
            this._satSettingRepo = _satSettingRepo;
            this._sunSettingRepo = _sunSettingRepo;
        }

        /// <summary>
        /// 设置停车场信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RequestResult<bool> CarParkSetting(CarParkSettingModel model)
        {
            var result = RequestResult<bool>.Get();

            if (model.areaSettings.Count != 6)
            {
                result.result = false;
                result.success = false;
                result.error = "时间范围设置不正确！";
                return result;
            }

            CarParkSetting setting = new CarParkSetting();

            var qrInfo = Guid.NewGuid().ToString("N");

            setting.carParkName = model.carParkName;
            setting.qrInfo = qrInfo;

            _carParkSettingRepo.Insert(setting);

            switch (model.week)
            {
                case "Monday":

                    List<MonSetting> monList = new List<MonSetting>();
                    foreach (var item in model.areaSettings)
                    {
                        MonSetting mon = new MonSetting();
                        mon.qrInfo = qrInfo;
                        mon.timeEnd = item.timeEnd;
                        mon.timeStart = item.timeStart;
                        mon.price = item.price;
                        monList.Add(mon);
                    }
                    _monSettingRepo.AddRange(monList);
                    break;

                case "Tuesday":
                    List<TuesSetting> tuesList = new List<TuesSetting>();
                    foreach (var item in model.areaSettings)
                    {
                        TuesSetting tues = new TuesSetting();
                        tues.qrInfo = qrInfo;
                        tues.timeEnd = item.timeEnd;
                        tues.timeStart = item.timeStart;
                        tues.price = item.price;
                        tuesList.Add(tues);
                    }
                    _tuesSettingRepo.AddRange(tuesList);
                    break;

                case "Wednesday":
                    List<WedSetting> wedList = new List<WedSetting>();
                    foreach (var item in model.areaSettings)
                    {
                        WedSetting wed = new WedSetting();
                        wed.qrInfo = qrInfo;
                        wed.timeEnd = item.timeEnd;
                        wed.timeStart = item.timeStart;
                        wed.price = item.price;
                        wedList.Add(wed);
                    }
                    _wedSettingRepo.AddRange(wedList);
                    break;

                case "Thursday":
                    List<ThurSetting> thurList = new List<ThurSetting>();
                    foreach (var item in model.areaSettings)
                    {
                        ThurSetting thur = new ThurSetting();
                        thur.qrInfo = qrInfo;
                        thur.timeEnd = item.timeEnd;
                        thur.timeStart = item.timeStart;
                        thur.price = item.price;
                        thurList.Add(thur);
                    }
                    _thurSettingRepo.AddRange(thurList);
                    break;

                case "Friday":
                    List<FriSetting> friList = new List<FriSetting>();
                    foreach (var item in model.areaSettings)
                    {
                        FriSetting fri = new FriSetting();
                        fri.qrInfo = qrInfo;
                        fri.timeEnd = item.timeEnd;
                        fri.timeStart = item.timeStart;
                        fri.price = item.price;
                        friList.Add(fri);
                    }
                    _friSettingRepo.AddRange(friList);
                    break;

                case "Saturday":
                    List<SatSetting> satList = new List<SatSetting>();
                    foreach (var item in model.areaSettings)
                    {
                        SatSetting sat = new SatSetting();
                        sat.qrInfo = qrInfo;
                        sat.timeEnd = item.timeEnd;
                        sat.timeStart = item.timeStart;
                        sat.price = item.price;
                        satList.Add(sat);
                    }
                    _satSettingRepo.AddRange(satList);
                    break;

                case "Sunday":
                    List<SunSetting> sunList = new List<SunSetting>();
                    foreach (var item in model.areaSettings)
                    {
                        SunSetting sun = new SunSetting();
                        sun.qrInfo = qrInfo;
                        sun.timeEnd = item.timeEnd;
                        sun.timeStart = item.timeStart;
                        sun.price = item.price;
                        sunList.Add(sun);
                    }
                    _sunSettingRepo.AddRange(sunList);
                    break;
            }
            _carParkSettingRepo.Flush();


            result.result = true;
            return result;
        }

        /// <summary>
        /// 获取停车场设置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RequestResult<List<CarParkSettingListResult>> GetCarParkSettingList(CarParkSettingListViewModel model)
        {
            var result = RequestResult<List<CarParkSettingListResult>>.Get();



            return result;
        }


        public RequestResult<ScanCodeResult> GetScanCodeResult(CarParkGetViewModel model)
        {
            var result = RequestResult<ScanCodeResult>.Get();

            var week = DateTime.Now.DayOfWeek.ToString();

            TimeSpan timeSpanNow = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));

            switch (week)
            {
                case "Monday":
                    var monlist = (from cps in _carParkSettingRepo.Table
                                  join set in _monSettingRepo.Table
                                  on cps.qrInfo equals set.qrInfo
                                  where (timeSpanNow > set.timeStart && timeSpanNow < set.timeEnd && set.qrInfo == model.qrInfo)
                                  select new ScanCodeResult
                                  {
                                      carParkName = cps.carParkName,
                                      price = set.price,
                                      qrInfo = cps.qrInfo,
                                  }).FirstOrDefault();
                    result.result = monlist;
                    break;
                case "Tuesday":
                    var tueslist = (from cps in _carParkSettingRepo.Table
                                   join set in _tuesSettingRepo.Table
                                   on cps.qrInfo equals set.qrInfo
                                   where (timeSpanNow > set.timeStart && timeSpanNow < set.timeEnd && set.qrInfo == model.qrInfo)
                                   select new ScanCodeResult
                                   {
                                       carParkName = cps.carParkName,
                                       price = set.price,
                                       qrInfo = cps.qrInfo,
                                   }).FirstOrDefault();
                    result.result = tueslist;
                    break;
                case "Wednesday":
                    var wedlist = (from cps in _carParkSettingRepo.Table
                                  join set in _wedSettingRepo.Table
                                  on cps.qrInfo equals set.qrInfo
                                  where (timeSpanNow > set.timeStart && timeSpanNow < set.timeEnd && set.qrInfo == model.qrInfo)
                                  select new ScanCodeResult
                                  {
                                      carParkName = cps.carParkName,
                                      price = set.price,
                                      qrInfo = cps.qrInfo,
                                  }).FirstOrDefault();
                    result.result = wedlist;
                    break;
                case "Thursday":
                    var thurlist = from cps in _carParkSettingRepo.Table
                                   join set in _thurSettingRepo.Table
                                   on cps.qrInfo equals set.qrInfo
                                   where (timeSpanNow > set.timeStart && timeSpanNow < set.timeEnd && set.qrInfo == model.qrInfo)
                                   select new ScanCodeResult
                                   {
                                       carParkName = cps.carParkName,
                                       price = set.price,
                                       qrInfo = cps.qrInfo,
                                   };
                    result.result = thurlist.FirstOrDefault();
                    break;
                case "Friday":
                    var frilist = from cps in _carParkSettingRepo.Table
                                  join set in _friSettingRepo.Table
                                  on cps.qrInfo equals set.qrInfo
                                  where (timeSpanNow > set.timeStart && timeSpanNow < set.timeEnd && set.qrInfo == model.qrInfo)
                                  select new ScanCodeResult
                                  {
                                      carParkName = cps.carParkName,
                                      price = set.price,
                                      qrInfo = cps.qrInfo,
                                  };
                    result.result = frilist.FirstOrDefault();
                    break;
                case "Saturday":
                    var satlist = from cps in _carParkSettingRepo.Table
                                  join set in _satSettingRepo.Table
                                  on cps.qrInfo equals set.qrInfo
                                  where (timeSpanNow > set.timeStart && timeSpanNow < set.timeEnd && set.qrInfo == model.qrInfo)
                                  select new ScanCodeResult
                                  {
                                      carParkName = cps.carParkName,
                                      price = set.price,
                                      qrInfo = cps.qrInfo,
                                  };
                    result.result = satlist.FirstOrDefault();
                    break;
                case "Sunday":
                    var sunlist = from cps in _carParkSettingRepo.Table
                                  join set in _sunSettingRepo.Table
                                  on cps.qrInfo equals set.qrInfo
                                  where (timeSpanNow > set.timeStart && timeSpanNow < set.timeEnd && set.qrInfo == model.qrInfo)
                                  select new ScanCodeResult
                                  {
                                      carParkName = cps.carParkName,
                                      price = set.price,
                                      qrInfo = cps.qrInfo,
                                  };
                    result.result = sunlist.FirstOrDefault();
                    break;
            }

            return result;
        }
    }
}
