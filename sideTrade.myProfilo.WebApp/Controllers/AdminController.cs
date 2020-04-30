using sideTrade.myProfilo.webApp.Models;
using sideTrade.myProfilo.WebApp.Controllers;
using sideTrade.myProfilo.WebApp.Filter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace sideTrade.myProfilo.WebApp.Controllers
{
    [AuthLogin(AccountType = "administrator")]
    public class AdminController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetAllProfile()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/Profiles/GetAllProfiles");
                response.EnsureSuccessStatusCode();
                List<ProfileViewModel> profiles = response.Content.ReadAsAsync<List<ProfileViewModel>>().Result;
                ViewBag.Title = "All Profile";
                return View("UserProfile", profiles);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult GetAllFiles()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/FilesManager/GetAllFiles");
                response.EnsureSuccessStatusCode();
                List<FileManagerViewModel> files = response.Content.ReadAsAsync<List<FileManagerViewModel>>().Result;
                ViewBag.Title = "File(s)";
                return View("FileManager", files);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult GetAllNotification()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/Notifications/GetAllNotifications");
                response.EnsureSuccessStatusCode();
                List<NotificationViemModel> notifications = response.Content.ReadAsAsync<List<NotificationViemModel>>().Result;
                ViewBag.Title = "Notification(s)";
                return View("Notification", notifications);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult GetAllLogs()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/LogManager/GetAllLogs");
                response.EnsureSuccessStatusCode();
                List<SystemLogViewModel> notifications = response.Content.ReadAsAsync<List<SystemLogViewModel>>().Result;
                ViewBag.Title = "Log(s)";
                return View("SystemLog", notifications);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult GetAllSystemSettings()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/Settings/GetAllSettings");
                response.EnsureSuccessStatusCode();
                List<SettingsViewModel> settings = response.Content.ReadAsAsync<List<SettingsViewModel>>().Result;
                ViewBag.Title = "Settings(s)";
                return View("Settings", settings);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //[HttpGet]  
        public ActionResult EditProfile(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/Profiles/GetProduct?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            ProfileViewModel profile = response.Content.ReadAsAsync<ProfileViewModel>().Result;
            ViewBag.Title = "Edit Profile";
            return View(profile);
        }
        //[HttpPost]  
        public ActionResult Update(ProfileViewModel pro)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PutResponse("api/Profiles/UpdateProfile", pro);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllProfile");
        }
        public ActionResult Details(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/Profiles/GetProfile?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            ProfileViewModel p = response.Content.ReadAsAsync<ProfileViewModel>().Result;
            ViewBag.Title = "Profile Details";
            return View(p);
        }
        [HttpGet]
        public ActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProfileViewModel pro)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            pro.CreatedOn = DateTime.Now;
            HttpResponseMessage response = serviceObj.PostResponse("api/Profiles/InsertProfile", pro);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllProfile");
        }

        public ActionResult Delete(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.DeleteResponse("api/Profiles/DeleteProfile?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllProfile");
        }

        [HttpPost]
        public JsonResult DoesEmailExist(string EmailAddress)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/Profiles/GetProfileByEmail?emailAddress=" + EmailAddress);
            response.EnsureSuccessStatusCode();
            ProfileViewModel p = response.Content.ReadAsAsync<ProfileViewModel>().Result;
            return Json(p == null);
        }

        [HttpPost]
        public ActionResult SendInvitation(int profileId)
        {
            var msg = string.Empty;
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                //Get the Profile Data
                HttpResponseMessage response = serviceObj.GetResponse($"api/Profiles/GetProfile/{profileId}");
                response.EnsureSuccessStatusCode();
                ProfileViewModel profileData = response.Content.ReadAsAsync<ProfileViewModel>().Result;

                //Send Invitation Email
                string code = Encryption.EncryptString($"{profileId}myProfio{DateTime.Now.Ticks.ToString()}").Replace("/", "myProfilo");
                string invitationLink = string.Format("http://{0}/verify/{1}", System.Web.HttpContext.Current.Request.Url.Authority, code);
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("NAME", $"{profileData.FirstName}");
                param.Add("LINK", invitationLink);
                string html = emailService.GetHtml(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["mxTemplatePath"] + "tmpJoining.html", param);

                //Insert the Inviation Data
                var inv = new NotificationViemModel()
                {
                    SenderProfileId = sm.UserSession.ProfileId,
                    RecipientProfileId = profileId,
                    Content = html,
                    Link = code,
                    Subject = $"Welcome to myProfio- {profileData.FirstName}",
                    FromEmail = sm.UserSession.emailaddress,
                    IsHTML = true,
                    NotificationTypeId = (int)NotificationType.INVITE,
                    ToEmail = profileData.EmailAddress
                };

                emailService.EmailBySMTP(profileData.EmailAddress, ConfigurationManager.AppSettings["SMTP_FROM"], html, $"Welcome to myProfio- {profileData.FirstName}");
                HttpResponseMessage responseInvitation = serviceObj.PostResponse("api/Notifications/InsertNotification", inv);
                response.EnsureSuccessStatusCode();

                //Update the profile Invitatino email being sent

                profileData.IsInvited = true;
                HttpResponseMessage responseUpdate = serviceObj.PutResponse("api/Profiles/UpdateProfile", profileData);
                response.EnsureSuccessStatusCode();

                msg = $"You have invited {profileData.FirstName} succssfully and email as been sent to {profileData.EmailAddress} now..";
            }
            catch (Exception ex)
            {
                msg = ex.InnerException.Message;
            }

            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(msg), "application/json");
        }

        [HttpPost]
        public ActionResult PreviewInvitation(int profileId)
        {
            var msg = string.Empty;
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();


                //Get the Profile Data
                HttpResponseMessage response = serviceObj.GetResponse($"api/Profiles/GetProfile/{profileId}");
                response.EnsureSuccessStatusCode();
                ProfileViewModel profileData = response.Content.ReadAsAsync<ProfileViewModel>().Result;

                //Send Invitation Email
                string code = string.Empty;
                string invitationLink = string.Format("http://{0}/verify/{1}", System.Web.HttpContext.Current.Request.Url.Authority, code);
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("NAME", $"{profileData.FirstName}");
                param.Add("LINK", invitationLink);
                string html = emailService.GetHtml(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["mxTemplatePath"] + "tmpJoining.html", param);
                msg = html;
            }
            catch (Exception ex)
            {
                msg = ex.InnerException.Message;
            }

            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(msg), "application/json");
        }

        public ActionResult MyProfile()
        {
            myProfilo.webApp.Models.ProfileViewModel pVM = new webApp.Models.ProfileViewModel();
            pVM.FirstName = sm.UserSession.FirstName;
            pVM.LastName = sm.UserSession.LastName;
            pVM.EmailAddress = sm.UserSession.emailaddress;
            return View(pVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(ProfileViewModel pVM)
        {
            if (ModelState.IsValid)
            {
                pVM.ModifiedOn = DateTime.Now;
                pVM.IsActive = true;
                pVM.IsActive = true;
                HttpResponseMessage response = new ServiceRepository().PutResponse($"api/Profiles/UpdateProfile", pVM);
                response.EnsureSuccessStatusCode();
            }

            return RedirectToAction("MyProfile");
        }

    }

}