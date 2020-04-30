using sideTrade.myProfilo.webApp.Models;
using sideTrade.myProfilo.WebApp.Filter;
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
    [AuthLogin(AccountType = "user")]
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyFiles()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse($"api/FilesManager/GetAllFilesByProfileId?ProfileId={sm.UserSession.ProfileId}&fileTypeId={null}");
                response.EnsureSuccessStatusCode();
                List<FileManagerViewModel> files = response.Content.ReadAsAsync<List<FileManagerViewModel>>().Result;
                ViewBag.Title = "My Files";
                return View(files);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult MyProfile()
        {
            myProfilo.webApp.Models.ProfileViewModel pVM = new webApp.Models.ProfileViewModel();
            pVM.FirstName = sm.UserSession.FirstName;
            pVM.LastName = sm.UserSession.LastName;
            pVM.EmailAddress = sm.UserSession.emailaddress;
            return View(pVM);
        }

        public ActionResult MyNotification()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse($"api/Notifications/GetRecipientNotifications?recipientId={sm.UserSession.ProfileId}&notificationTypeId={null}");
                response.EnsureSuccessStatusCode();
                List<NotificationViemModel> files = response.Content.ReadAsAsync<List<NotificationViemModel>>().Result;
                ViewBag.Title = "My Notification";
                return View(files);
            }
            catch (Exception)
            {
                throw;
            }
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
