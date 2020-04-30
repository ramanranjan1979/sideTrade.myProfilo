using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sideTrade.Dal.DAL
{
    public class NotificationDal
    {
        static SideTrade_DBEntities DbContext;
        static NotificationDal()
        {
            DbContext = new SideTrade_DBEntities();
        }
        public static List<Notification> GetAllNotification()
        {
            return DbContext.Notification.ToList();
        }

        public static Notification GetNotification(int notificationId)
        {
            return DbContext.Notification.Where(p => p.Id == notificationId).FirstOrDefault();
        }

        public static Notification GetNotification(string link)
        {
            return DbContext.Notification.Where(p => p.Link.ToLower().EndsWith(link.ToLower())).FirstOrDefault();
        }


        public static List<Notification> GetSenderNotification(int senderProfileId, int? notificationTypeId)
        {
            return DbContext.Notification.Where(p => p.RecipientProfileId == senderProfileId && (notificationTypeId.HasValue ? p.NotificationTypeId == notificationTypeId.Value : 1 == 1)).ToList();
        }

        public static List<Notification> GetRecipientNotification(int recipientProfileId, int? notificationTypeId)
        {
            return DbContext.Notification.Where(p => p.RecipientProfileId.Value == recipientProfileId && (notificationTypeId.HasValue ? p.NotificationTypeId == notificationTypeId : 1 == 1)).ToList();
        }

        public static Notification InsertNotification(Notification data)
        {
            bool status;
            try
            {
                DbContext.Notification.Add(data);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return data;
        }

        public static bool UpdateNotification(Notification notification)
        {
            bool status;
            try
            {
                Notification item = DbContext.Notification.Where(p => p.Id == notification.Id).FirstOrDefault();
                if (item != null)
                {
                    item.Content = notification.Content;
                    item.CreatedOn = notification.CreatedOn;
                    item.ReadOn = notification.ReadOn;
                    item.SenderProfileId = notification.SenderProfileId;
                    item.RecipientProfileId = notification.RecipientProfileId;
                    item.Link = notification.Link;
                    item.Subject = notification.Subject;
                    item.IsHTML = notification.IsHTML;
                    DbContext.SaveChanges();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public static bool DeleteNotification(int id)
        {
            bool status;
            try
            {
                Notification n = DbContext.Notification.Where(p => p.Id == id).FirstOrDefault();
                if (n != null)
                {
                    DbContext.Notification.Remove(n);
                    DbContext.SaveChanges();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public static NotificationType InsertNotificationType(NotificationType data)
        {
            bool status;
            try
            {
                DbContext.NotificationType.Add(data);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return data;
        }
    }
}
