using sideTrade.myProfilo.webApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace sideTrade.myProfilo.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected EmailService emailService = new EmailService(bool.Parse(ConfigurationManager.AppSettings["TESTMODE"]), System.Web.HttpContext.Current);
        protected SessionManager sm = new SessionManager();

        protected override void OnException(ExceptionContext filterContext)
        {
            var exceptionDetails = filterContext.Exception.InnerException == null ? $"Exception:{filterContext.Exception.ToString()}" : $"Exception:{filterContext.Exception.ToString()}#InnerException{filterContext.Exception.InnerException.ToString()}";

            int? mid = null;
            string redirectTO = string.Empty;

            if (new SessionManager().UserSession != null)
            {
                mid = new int();
                mid = new SessionManager().UserSession.ProfileId;
            }

            LogMe((int)LogType.APP_EXCEPTION, exceptionDetails,mid);

            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Error/Error.cshtml",
                ViewData = new ViewDataDictionary(exceptionDetails)
            };
        }


        public ActionResult GetUpload()
        {
            UploadFileViewModel model = new UploadFileViewModel();
            return PartialView("_Upload", model);
        }

        [ChildActionOnly]
        public ActionResult DashBoardNewProfile()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/Profiles/GetAllProfiles");
            response.EnsureSuccessStatusCode();
            List<ProfileViewModel> pVMList = response.Content.ReadAsAsync<List<ProfileViewModel>>().Result;
            return PartialView("_newProfile", pVMList);
        }

        [ChildActionOnly]
        public ActionResult DashBoardNewVerification()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/Notifications/GetAllNotifications");
            response.EnsureSuccessStatusCode();
            List<NotificationViemModel> notifications = response.Content.ReadAsAsync<List<NotificationViemModel>>().Result;            
            return PartialView("_newVerification", notifications);
        }

        [ChildActionOnly]
        public ActionResult DashBoardNewFileManagement()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/FilesManager/GetAllFiles");
            response.EnsureSuccessStatusCode();
            List<FileManagerViewModel> files = response.Content.ReadAsAsync<List<FileManagerViewModel>>().Result;            
            return PartialView("_newFileManager", files);
        }

        [ChildActionOnly]
        public ActionResult DashBoardNewSysLog()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/LogManager/GetAllLogs");
            response.EnsureSuccessStatusCode();
            List<SystemLogViewModel> notifications = response.Content.ReadAsAsync<List<SystemLogViewModel>>().Result;            
            return PartialView("_newSysLog", notifications);
        }

        [HttpGet]
        public ActionResult Download(int fileId)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse($"api/FilesManager/GetFile?fileId={fileId}");
            response.EnsureSuccessStatusCode();
            FileManagerViewModel file = response.Content.ReadAsAsync<FileManagerViewModel>().Result;

            string rawText = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + file.Path + "\\" + file.FileName).Replace("\\", @"\"));
            if (Path.GetExtension(file.FileName.ToUpper()) == ".CS")
            {
                //Log that the user has downloaded this file
                FileManagerViewModel fMVM = new FileManagerViewModel()
                {
                    Mode = FileManagerMode.DOWNLOAD.ToString(),
                    Path = file.Path,
                    ProfileId = file.ProfileId,
                    FileName = file.FileName,
                    Comment = $"{file.ProfileId} has downloaded this file",
                    FileManagerTypeId = file.FileManagerTypeId,
                    Status = FileManagerStatus.CREATED.ToString()
                };
                HttpResponseMessage response1 = serviceObj.PostResponse("api/FilesManager/InsertFile", fMVM);
                var d = response.EnsureSuccessStatusCode();


                string fullpath = Path.Combine(Server.MapPath(file.Path), file.FileName);
                return File(fullpath, "text/plain", file.FileName);
            }
            else
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }

        }

        public void LogMe(int logTypeId, string logValue,int? profileId )
        {
            try
            {
                SystemLogViewModel sVM = new SystemLogViewModel()
                {
                    LogTypeId = logTypeId,
                    Value = logValue,
                    ProfileId = profileId
                };
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse("api/LogManager/InsertLog", sVM);                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}