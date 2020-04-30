using sideTrade.Dal.DAL;
using sideTrade.myProfilo.webApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace sideTrade.myProfilo.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
            if (sm.UserSession != null)
            {
                var msg = $"A logged in session has been detected for {sm.UserSession.ProfileId}";
                LogMe((int)LogType.APP_USERTRACE, msg, sm.UserSession.ProfileId);
            }

        }

        public ActionResult Index()
        {
            sm.UserSession = null;
            return RedirectToAction("Login", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            CredentialViewModel lVM = new CredentialViewModel();
            return View(lVM);
        }

        [HttpPost]
        public ActionResult ValidateUser(CredentialViewModel cVM)
        {
            LoginStatus status = new Security().ValidateUser(cVM.EmailAddress, cVM.Password);
            if (status.Success)
            {
                sm.UserSession = status.LoggedInPerson;
                LogMe((int)LogType.APP_USERTRACE, "LOGGED IN SYSTEM", sm.UserSession.ProfileId);
                if (sm.UserSession.RoleNameList.Contains("administrator"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    if (sm.UserSession.RoleNameList.Contains("user"))
                    {
                        return RedirectToAction("MyProfile", "Account");
                    }
                }
            }

            ViewBag.Message = "Invalid Username or password";
            return View("login", new CredentialViewModel());
        }


        public ActionResult LogOut()
        {
            //sDal.LogMe("TRACKING", "LOGGED OUT FROM SYSTEM", sm.UserSession.Person.Id);
            sm.UserSession = null;
            return RedirectToAction("Login", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View(new ForgotPassword());
        }

        [HttpGet]
        public ActionResult VerifyProfile(string Code)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            var invitation = NotificationDal.GetNotification(Code);
            var DecryptCode = Encryption.DecryptString(Code.Replace("myProfilo", "/"));

            if (!DecryptCode.Contains("myProfio") || invitation.ReadOn.HasValue)
            {
                return RedirectToAction("Login");
            }

            string[] separatingStrings = { "myProfio" };
            string[] words = DecryptCode.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            if (words.Count() != 2)
            {
                return RedirectToAction("Login");
            }

            var profileId = int.Parse(words[0]);

            //Get the Profile Data

            HttpResponseMessage response1 = serviceObj.GetResponse($"api/Profiles/GetProfile/{profileId}");
            response1.EnsureSuccessStatusCode();
            ProfileViewModel profileData = response1.Content.ReadAsAsync<ProfileViewModel>().Result;

            if (profileData == null)
            {
                return RedirectToAction("Login");
            }

            LoginViewModel rVM = new LoginViewModel()
            {
                FirstName = profileData.FirstName,
                LastName = profileData.LastName,
                EmailAddress = profileData.EmailAddress,
                Profile_Id = profileId,
                InvitationId = invitation.Id
            };

            return View(rVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteRegistration(LoginViewModel pVM)
        {
            if (ModelState.IsValid)
            {
                pVM.Password = Encryption.ComputeHash(pVM.Password);
                pVM.IsActive = true;
                ServiceRepository serviceObj = new ServiceRepository();

                if (!pVM.PasswordReset)
                {
                    HttpResponseMessage response = serviceObj.PostResponse("api/Logins/InsertLogin", pVM);
                    response.EnsureSuccessStatusCode();
                }


                //Get the Invitation and then Expire the Invitation Link
                HttpResponseMessage response1 = serviceObj.GetResponse($"api/Notifications/GetNotification/{pVM.InvitationId}");
                response1.EnsureSuccessStatusCode();
                NotificationViemModel noti = response1.Content.ReadAsAsync<NotificationViemModel>().Result;

                noti.UpdatedOn = DateTime.Now;
                noti.ReadOn = DateTime.Now;
                HttpResponseMessage response2 = serviceObj.PutResponse($"api/Notifications/UpdateNotification", noti);
                response2.EnsureSuccessStatusCode();

                if (!pVM.PasswordReset)
                {
                    //Role Mapping
                    var proRole = new ProfileMappingViewModel() { IsActive = true, ProfileId = pVM.Profile_Id, ProfileTypeId = 2 };
                    HttpResponseMessage response3 = serviceObj.PostResponse("api/Profiles/InsertProfileRole", proRole);
                    response3.EnsureSuccessStatusCode();
                }

                Dal.Login l = LoginDal.GetLoginByProfileId(pVM.Profile_Id);

                if (pVM.PasswordReset)
                {
                    //Update the new password

                    l.Password = pVM.Password;
                    l.ModifiedOn = DateTime.Now;
                    l.IsLock = false;
                    l.IsActive = true;
                    LoginDal.UpdateLogin(l);
                }
                else
                {
                    //Let them login
                    LoginStatus status = new Security().ValidateUser(l.Profile.EmailAddress, pVM.Password, true);
                    if (status.Success)
                    {
                        sm.UserSession = status.LoggedInPerson;
                        LogMe((int)LogType.APP_USERTRACE, "LOGGED IN SYSTEM", sm.UserSession.ProfileId);
                        if (sm.UserSession.RoleNameList.Contains("administrator"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            if (sm.UserSession.RoleNameList.Contains("user"))
                            {
                                return RedirectToAction("Index", "Account");
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public JsonResult DoesEmailExist(string EmailAddress)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/Profiles/GetProfileByEmail?emailAddress=" + EmailAddress);
            response.EnsureSuccessStatusCode();
            ProfileViewModel p = response.Content.ReadAsAsync<ProfileViewModel>().Result;
            return Json(p != null);
        }

        [HttpPost]
        public ActionResult ResetPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                bool canRequest = true;

                ServiceRepository serviceObj = new ServiceRepository();
                //Get the Profile Data
                HttpResponseMessage response = serviceObj.GetResponse($"api/Profiles/GetProfileByEmail?EMailAddress={model.EmailAddress}");
                response.EnsureSuccessStatusCode();
                ProfileViewModel profileData = response.Content.ReadAsAsync<ProfileViewModel>().Result;


                if (profileData.Id == 0 || !profileData.IsActive)
                {
                    ViewBag.Message = "Email Address is invalid";
                    canRequest = false;
                }

                if (canRequest) 
                {
                    var d = NotificationDal.GetRecipientNotification(profileData.Id, (int)NotificationType.RESETPASSWORD).ToList();

                    if (d.Where(x => x.ReadOn.HasValue == false).ToList().Count() >= 2)
                    {
                        TempData["ERR_Message"] = "Looks like, We had already sent you the password reset instructions.If you haven't received an email from us, please check your spam or junk mail folder.";
                        canRequest = false;
                    }
                }

                if (canRequest)
                {


                    //Send Invitation Email
                    string code = Encryption.EncryptString($"{profileData.Id}myProfio{DateTime.Now.Ticks.ToString()}").Replace("/", "myProfilo");
                    string resetLink = string.Format("http://{0}/verifypassword/{1}", System.Web.HttpContext.Current.Request.Url.Authority, code);
                    Dictionary<string, string> param = new Dictionary<string, string>();
                    param.Add("NAME", $"{profileData.FirstName}");
                    param.Add("RESETLINK", resetLink);
                    string html = emailService.GetHtml(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["mxTemplatePath"] + "tmpForgotPassword.html", param);

                    //Insert the Inviation Data
                    var inv = new NotificationViemModel()
                    {
                        SenderProfileId = 1,
                        RecipientProfileId = profileData.Id,
                        Content = html,
                        Link = code,
                        Subject = $"Forgot your password {profileData.FirstName }?",
                        FromEmail = SettingDal.GetSettingByName("SYS_ADMIN_EMAIL").Value,
                        IsHTML = true,
                        NotificationTypeId = (int)NotificationType.RESETPASSWORD,
                        ToEmail = profileData.EmailAddress
                    };

                    emailService.EmailBySMTP(profileData.EmailAddress, ConfigurationManager.AppSettings["SMTP_FROM"], html, $"Did you forgot password? - {profileData.FirstName}");
                    HttpResponseMessage responseInvitation = serviceObj.PostResponse("api/Notifications/InsertNotification", inv);
                    response.EnsureSuccessStatusCode();

                    LogMe((int)LogType.APP_INFORMATION, "RESET PASSWORD REQUEST HAS BEEN REQUESTED", profileData.Id);

                }

                TempData["PasswordReset"] = canRequest;

                return RedirectToAction("Login", "Home");
            }
            else
            {
                return RedirectToAction("ForgotPassword", "Home", new ForgotPassword());
            }
        }

        [HttpGet]
        public ActionResult verifypassword(string Code)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            var invitation = NotificationDal.GetNotification(Code);
            var DecryptCode = Encryption.DecryptString(Code.Replace("myProfilo", "/"));

            if (!DecryptCode.Contains("myProfio"))
            {
                return RedirectToAction("Login");
            }

            string[] separatingStrings = { "myProfio" };
            string[] words = DecryptCode.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            if (words.Count() != 2)
            {
                return RedirectToAction("Login");
            }

            var profileId = int.Parse(words[0]);

            //Get the Profile Data

            HttpResponseMessage response1 = serviceObj.GetResponse($"api/Profiles/GetProfile/{profileId}");
            response1.EnsureSuccessStatusCode();
            ProfileViewModel profileData = response1.Content.ReadAsAsync<ProfileViewModel>().Result;

            if (profileData == null)
            {
                return RedirectToAction("Login");
            }

            LoginViewModel rVM = new LoginViewModel()
            {
                FirstName = profileData.FirstName,
                LastName = profileData.LastName,
                EmailAddress = profileData.EmailAddress,
                Profile_Id = profileId,
                InvitationId = invitation.Id,
                PasswordReset = true
            };

            return View("VerifyProfile", rVM);
        }
    }
}