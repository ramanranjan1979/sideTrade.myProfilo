using sideTrade.Dal;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using sideTrade.Dal.DAL;
namespace sideTrade.myProfilo.webApi.Controllers
{
    public class SettingsController : ApiController
    {
        [HttpGet]
        public JsonResult<List<Models.Settings>> GetAllSettings()
        {
            EntityMapperSettings<Settings, Models.Settings> mapObj = new EntityMapperSettings<Settings, Models.Settings>();
            List<Settings> settingsList = SettingDal.GetAllSetting();
            List<Models.Settings> dataList = new List<Models.Settings>();
            foreach (var item in settingsList)
            {
                dataList.Add(mapObj.Translate(item));
            }
            return Json(dataList);
        }        
       

        [HttpPost]
        public Models.Settings InsertSettings(Models.Settings p)
        {
            if (ModelState.IsValid)
            {
                EntityMapperSettings<Models.Settings, Settings> mapObj = new EntityMapperSettings<Models.Settings,Settings>();
                Settings obj = new Settings();
                obj = mapObj.Translate(p);
                SettingDal.InsertSetting(obj);
            }
            return p;
        }    
       
    }
}