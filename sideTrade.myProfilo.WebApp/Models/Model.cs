using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sideTrade.myProfilo.webApp.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is mandatory")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Length of First name should be 3 to 100 characters")]
        [DataType(DataType.Text)]
       // [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only be alphabets(a to z)")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is mandatory")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Length of Last name should be 3 to 100 characters")]
        [DataType(DataType.Text)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name can only be alphabets(a to z)")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Length of email address must be 10 to 50 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your email address")]
        [Remote("DoesEmailExist", "Admin", HttpMethod = "POST", ErrorMessage = "Email already exists so please use another email address.")]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }
        public bool? IsInvited { get; set; } = false;
    }

    public class FileManagerViewModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string Path { get; set; }
        public string Mode { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int FileManagerTypeId { get; set; }
        public Profile Profile { get; set; }
        //public HttpPostedFileBase FileToUpload { get; set; }
    }

    public class Profile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsInvited { get; set; }
    }


    public class UploadFileViewModel
    {
        public FileManagerViewModel FileViewModel { get; set; }

        public HttpPostedFileBase FileToUpload { get; set; }
    }

    public class ProfileMappingViewModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int ProfileTypeId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
    }

    public class ProfileTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }

    public class LoginViewModel
    {
        public int Profile_Id { get; set; }
        public int InvitationId { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is mandatory")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Length of First name should be 3 to 100 characters")]
        [DataType(DataType.Text)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only be alphabets(a to z)")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is mandatory")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Length of Last name should be 3 to 100 characters")]
        [DataType(DataType.Text)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name can only be alphabets(a to z)")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        [MinLength(9, ErrorMessage = "Password must be 8 or more characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your password")]
        public string Password { get; set; }

        public bool IsActive { get; set; } = false;

        public bool PasswordReset { get; set; } = false;

    }

    public class CredentialViewModel
    {
       
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        [MinLength(9, ErrorMessage = "Password must be 8 or more characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your password")]
        public string Password { get; set; }

    }


    public class NotificationViemModel
    {
        public int Id { get; set; }
        public int SenderProfileId { get; set; }
        public int? RecipientProfileId { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public DateTime? ReadOn { get; set; }
        public int NotificationTypeId { get; set; }
        public Nullable<bool> IsHTML { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public NotificationType NotificationType { get; set; }
    }

    public class NotificationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TemplateName { get; set; }
    }


    public class SystemLogViewModel
    {
        public int Id { get; set; }
        public int LogTypeId { get; set; }
        public string Value { get; set; }
        public System.DateTime CreatedOn { get; set; } = DateTime.Now;        
        public LogType LogType { get; set; }
        public Nullable<int> ProfileId { get; set; }
    }

    public class LogType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SettingsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ForgotPassword
    {
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your email address")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Length of email address must be 10 to 50 characters")]        
        [Remote("DoesEmailExist", "Home", HttpMethod = "POST", ErrorMessage = "This email address does not exists in system so please use corrrect email address.")]
        public string EmailAddress { get; set; }        
    }
}