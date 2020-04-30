using sideTrade.Dal;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using sideTrade.Dal.DAL;
namespace sideTrade.myProfilo.webApi.Controllers
{
    public class NotificationsController : ApiController
    {
        [HttpGet]
        public JsonResult<List<Models.Notification>> GetAllNotifications()
        {
            EntityMapperNotification<Notification, Models.Notification> mapObj = new EntityMapperNotification<Notification, Models.Notification>();
            List<Notification> invitationList = NotificationDal.GetAllNotification();
            List<Models.Notification> dataList = new List<Models.Notification>();
            foreach (var item in invitationList)
            {
                dataList.Add(mapObj.Translate(item));
            }
            return Json(dataList);
        }
        [HttpGet]
        public JsonResult<Models.Notification> GetNotification(int id)
        {
            EntityMapperNotification<Notification, Models.Notification> mapObj = new EntityMapperNotification<Notification, Models.Notification>();
            Notification dalInvitation = NotificationDal.GetNotification(id);
            Models.Notification inv = new Models.Notification();
            inv = mapObj.Translate(dalInvitation);
            return Json(inv);
        }

        [HttpGet]
        public JsonResult<Models.Notification> GetNotificationByLink(string link)
        {
            EntityMapperNotification<Notification, Models.Notification> mapObj = new EntityMapperNotification<Notification, Models.Notification>();
            Notification dalInvitation = NotificationDal.GetNotification(link);
            Models.Notification inv = new Models.Notification();
            inv = mapObj.Translate(dalInvitation);
            return Json(inv);
        }

        [HttpGet]
        public JsonResult<List<Models.Notification>> GetRecipientNotifications(int recipientId, int? notificationTypeId)
        {
            EntityMapperNotification<Notification, Models.Notification> mapObj = new EntityMapperNotification<Notification, Models.Notification>();
            List<Notification> invitationList = NotificationDal.GetRecipientNotification(recipientId, notificationTypeId);
            List<Models.Notification> dataList = new List<Models.Notification>();
            foreach (var item in invitationList)
            {
                dataList.Add(mapObj.Translate(item));
            }
            return Json(dataList);
        }

        [HttpGet]
        public JsonResult<List<Models.Notification>> GetSenderNotifications(int senderProfileId, int? notificationTypeId)
        {
            EntityMapperNotification<Notification, Models.Notification> mapObj = new EntityMapperNotification<Notification, Models.Notification>();
            List<Notification> invitationList = NotificationDal.GetSenderNotification(senderProfileId, notificationTypeId);
            List<Models.Notification> dataList = new List<Models.Notification>();
            foreach (var item in invitationList)
            {
                dataList.Add(mapObj.Translate(item));
            }
            return Json(dataList);
        }


        [HttpPost]
        public Models.Notification InsertNotification(Models.Notification p)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                EntityMapperNotification<Models.Notification, Notification> mapObj = new EntityMapperNotification<Models.Notification, Notification>();
                Notification obj = new Notification();
                obj = mapObj.Translate(p);
                NotificationDal.InsertNotification(obj);
            }
            return p;

        }
        [HttpPut]
        public bool UpdateNotification(Models.Notification data)
        {
            EntityMapperNotification<Models.Notification, Notification> mapObj = new EntityMapperNotification<Models.Notification, Notification>();
            Notification obj = new Notification();
            obj = mapObj.Translate(data);
            var status = NotificationDal.UpdateNotification(obj);
            return status;
        }


        [HttpDelete]
        public bool DeleteNotification(int id)
        {
            var status = NotificationDal.DeleteNotification(id);
            return status;
        }

        [HttpPost]
        public Models.NotificationType InsertNotificationType(Models.NotificationType p)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                EntityMapperNotificationType<Models.NotificationType, NotificationType> mapObj = new EntityMapperNotificationType<Models.NotificationType, NotificationType>();
                NotificationType obj = new NotificationType();
                obj = mapObj.Translate(p);
                NotificationDal.InsertNotificationType(obj);
            }
            return p;
        }
    }
}