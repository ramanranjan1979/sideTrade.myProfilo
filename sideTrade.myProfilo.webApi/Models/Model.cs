using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sideTrade.myProfilo.webApi.Models
{
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

    public class Notification
    {
        public int Id { get; set; }
        public int SenderProfileId { get; set; }
        public Nullable<int> RecipientProfileId { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public Nullable<System.DateTime> ReadOn { get; set; }
        public int NotificationTypeId { get; set; }
        public Nullable<bool> IsHTML { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public NotificationType NotificationType { get; set; }
    }

    public class NotificationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TemplateName { get; set; }        
    }

    public class FileManager
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string Path { get; set; }
        public string Mode { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int FileManagerTypeId { get; set; }
        public Profile Profile { get; set; }
        public FileManagerType FileManagerType { get; set; }
    }

    public class FileManagerType
    {
        public int Id { get; set; }
        public string FileType { get; set; }
        public string MaxMBSize { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }        
    }

    public class ProfileMapping
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int ProfileTypeId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }        
    }

    public class ProfileType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }        
    }

    public class Login
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public System.DateTime CreatedOn { get; set; } = DateTime.Now;
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<bool> IsLock { get; set; }
        public bool IsActive { get; set; }
        public int Profile_Id { get; set; }        
    }

    public class Settings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public partial class Log
    {
        public int Id { get; set; }
        public int LogTypeId { get; set; }
        public string Value { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> ProfileId { get; set; }
        public LogType LogType { get; set; }
    }

    public class LogType
    {        
        public int Id { get; set; }
        public string Name { get; set; }               
    }

   
}