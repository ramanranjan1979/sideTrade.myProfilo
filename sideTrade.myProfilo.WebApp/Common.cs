using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace sideTrade.myProfilo.WebApp
{
    public static class Encryption
    {
        public static string ComputeHash(string str)
        {
            SHA1Managed sha1m = new SHA1Managed();
            var temp = sha1m.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            string passwordHash = "";
            foreach (var _byte in temp)
            {
                passwordHash = passwordHash + _byte.ToString("X2");
            }

            return passwordHash;
        }

        public static string EncryptString(string textToEncrypt)
        {
            try
            {
                string ToReturn = "";
                string _key = "ay$a5%&jwrtmnh;lasjdf98787";
                string _iv = "abc@98797hjkas$&asd(*$%";
                byte[] _ivByte = { };
                _ivByte = Encoding.UTF8.GetBytes(_iv.Substring(0, 8));
                byte[] _keybyte = { };
                _keybyte = Encoding.UTF8.GetBytes(_key.Substring(0, 8));
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = Encoding.UTF8.GetBytes(textToEncrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(_keybyte, _ivByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ae)
            {
                throw new Exception(ae.Message, ae.InnerException);
            }
        }

        public static string DecryptString(string textToDecrypt)
        {
            try
            {
                string ToReturn = "";
                string _key = "ay$a5%&jwrtmnh;lasjdf98787";
                string _iv = "abc@98797hjkas$&asd(*$%";
                byte[] _ivByte = { };
                _ivByte = Encoding.UTF8.GetBytes(_iv.Substring(0, 8));
                byte[] _keybyte = { };
                _keybyte = Encoding.UTF8.GetBytes(_key.Substring(0, 8));
                MemoryStream ms = null; CryptoStream cs = null;
                byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(_keybyte, _ivByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ae)
            {
                throw new Exception(ae.Message, ae.InnerException);
            }
        }
    }

    public sealed class PasswordAdvisor
    {
        public static PasswordScore CheckStrength(string password)
        {
            int score = 1;

            if (password.Length < 1)
            {
                return PasswordScore.Blank;
            }
            if (password.Length < 4)
            {
                return PasswordScore.VeryWeak;
            }

            if (password.Length >= 8)
            {
                score++;
            }

            if (password.Length >= 12)
            {
                score++;
            }

            if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
            {
                score++;
            }
            if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success && Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
            {
                score++;
            }
            if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            return (PasswordScore)score;
        }
    }

    public class EmailResponse
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class EmailService
    {
        private bool _testMode;
        private HttpContext _currentContext;


        public EmailService(bool TestMode, HttpContext Context)
        {
            _testMode = TestMode;
            _currentContext = Context;
        }

        public EmailResponse EmailBySMTP(string toEmailAddress, string fromAddress, string body, string subject)
        {
            EmailResponse res = new EmailResponse() { HasError = false, ErrorMessage = string.Empty };

            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            m.From = new MailAddress(fromAddress);
            m.To.Add(_testMode ? System.Configuration.ConfigurationManager.AppSettings["SMTP_TO"] : toEmailAddress);
            m.Subject = subject;
            m.Body = body;
            m.IsBodyHtml = true;
            sc.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
            string str1 = "gmail.com";
            if (fromAddress.ToLower().Contains(str1))
            {
                try
                {
                    sc.Port = 587;
                    sc.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTP_FROM"], ConfigurationManager.AppSettings["SMTP_Password"]);
                    sc.EnableSsl = true;
                    m.IsBodyHtml = true;

                    if (!_testMode)
                        sc.Send(m);
                }
                catch (Exception ex)
                {
                    res.HasError = true;
                    res.ErrorMessage = ex.InnerException.Message;
                }
            }
            else
            {
                try
                {
                    sc.Port = 25;
                    sc.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTP_FROM"], ConfigurationManager.AppSettings["SMTP_Password"]);
                    sc.EnableSsl = false;
                    m.IsBodyHtml = true;
                    if (!_testMode)
                        sc.Send(m);

                }
                catch (Exception ex)
                {
                    res.HasError = true;
                    res.ErrorMessage = ex.InnerException.Message;
                }
            }

            return res;
        }

        public string GetHtml(string templatePath, Dictionary<string, string> Parameters)
        {
            var rawString = System.IO.File.ReadAllText(templatePath);
            foreach (var parameter in Parameters)
            {
                if (rawString.Contains("{#" + parameter.Key.ToUpper() + "#}"))
                {
                    rawString = rawString.Replace("{#" + parameter.Key.ToUpper() + "#}", parameter.Value);
                }
            }

            return rawString;
        }
    }

    public class SessionManager
    {

        public Security UserSession
        {
            get
            {
                return (Security)HttpContext.Current.Session["Security"];
            }
            set
            {
                HttpContext.Current.Session["Security"] = value;

            }
        }
    }

    public class Security
    {
        public int LoginId { get; set; }
        public string emailaddress { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsLock { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; } = null;
        public DateTime? LockedOn { get; set; } = null;
        public List<string> RoleNameList { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ProfileId { get; set; }

        public LoginStatus ValidateUser(string emailAddress, string password, bool isPasswordHashed = false)
        {
            LoginStatus status = new LoginStatus();
            SHA1Managed sha1m = new SHA1Managed();
            var temp = sha1m.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            string passwordHash = "";
            if (!isPasswordHashed)
            {
                foreach (var _byte in temp)
                {
                    passwordHash = passwordHash + _byte.ToString("X2");
                }
            }
            else
            {
                passwordHash = password;
            }
            var profile = Dal.DAL.ProfileDal.GetProfileByEmail(emailAddress);

            if (profile == null)
            {
                status.Success = false;
                status.Message = $"{emailAddress } is not registered";
            }
            else
            {
                var login = Dal.DAL.LoginDal.GetAllLogin().ToList().Where(x => x.Profile_Id == profile.Id && x.Password.Equals(passwordHash, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();


                if (login == null)
                {
                    status.Success = false;
                    status.Message = "Password is not valid";
                }
                else
                {
                    status.Success = true;
                    status.Message = "Login attempt successful!";
                    status.LoggedInPerson = new Security()
                    {
                        LoginId = login.Id,
                        IsActive = login.IsActive,
                        Password = login.Password,
                        emailaddress = profile.EmailAddress,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        ProfileId = profile.Id
                    };
                    status.LoggedInPerson.RoleNameList = new List<string>();

                    foreach (var role in profile.ProfileMapping)
                    {
                        status.LoggedInPerson.RoleNameList.Add(role.ProfileType.Name.ToLower());
                    }
                }
            }

            return status;
        }
    }

    public class LoginStatus
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TargetURL { get; set; }
        public int memberId { get; set; }
        public Security LoggedInPerson { get; set; }
    }

    public enum PasswordScore
    {
        Blank = 0,
        VeryWeak = 1,
        Weak = 2,
        Medium = 3,
        Strong = 4,
        VeryStrong = 5
    }

    public enum LogType
    {
        APP_EXCEPTION = 1,
        APP_INFORMATION = 2,
        APP_DEBUGG = 3,
        APP_USERTRACE = 4,
    }

    public enum NotificationType
    {
        INVITE = 1,
        RESETPASSWORD = 2,
        FILEUPLOAD = 3,
        UPLOADREJECT = 4
    }

    public enum FileManagerType
    {
        CS = 1,
        GIF = 2,
        JPG = 3,
        VB = 4
    }

    public enum FileManagerMode
    {
        UPLOAD = 1,
        DOWNLOAD = 2
    }

    public enum FileManagerStatus
    {
        CREATED = 1,
        DELETED = 2
    }




}