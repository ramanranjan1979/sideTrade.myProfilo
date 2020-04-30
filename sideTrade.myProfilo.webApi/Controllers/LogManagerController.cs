using sideTrade.Dal;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using sideTrade.Dal.DAL;
using System.Web;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace sideTrade.myProfilo.webApi.Controllers
{
    public class LogManagerController : ApiController
    {
        [HttpGet]
        public JsonResult<List<Models.Log>> GetAllLogs()
        {
            EntityMapperLogManager<Log, Models.Log> mapObj = new EntityMapperLogManager<Log, Models.Log>();
            List<Log> fileList = LogDal.GetAllLog();
            List<Models.Log> fList = new List<Models.Log>();
            foreach (var item in fileList)
            {
                fList.Add(mapObj.Translate(item));
            }
            return Json(fList);
        }

        [HttpGet]
        public JsonResult<List<Models.Log>> GetAllLogByProfileId(int profileId, int logTypeId)
        {
            EntityMapperLogManager<Log, Models.Log> mapObj = new EntityMapperLogManager<Log, Models.Log>();
            List<Log> fileList = LogDal.GetAllLog(profileId, logTypeId);
            List<Models.Log> fList = new List<Models.Log>();
            foreach (var item in fileList)
            {
                fList.Add(mapObj.Translate(item));
            }
            return Json(fList);
        }
       

        [HttpPost]
        public Models.Log InsertLog(Models.Log p)
        {
            if (ModelState.IsValid)
            {
                EntityMapperLogManager<Models.Log, Log> mapObj = new EntityMapperLogManager<Models.Log, Log>();
                Log fileObj = new Log();
                fileObj = mapObj.Translate(p);
                LogDal.InsertLog(fileObj);
            }
            return p;
        }

        [HttpPost]
        public Models.LogType InsertLogType(Models.LogType p)
        {
            if (ModelState.IsValid)
            {
                EntityMapperLogManagerType<Models.LogType, LogType> mapObj = new EntityMapperLogManagerType<Models.LogType, LogType>();
                LogType fileObj = new LogType();
                fileObj = mapObj.Translate(p);
                LogDal.InsertLogType(fileObj);
            }
            return p;
        }

    }
}