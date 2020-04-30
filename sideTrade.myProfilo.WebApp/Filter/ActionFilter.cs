using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace sideTrade.myProfilo.WebApp.Filter
{
    public class ActionFilter : ActionFilterAttribute
    {
        //private MemberDal _mDal = new MemberDal();
        private SessionManager sm = new SessionManager();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!sm.UserSession.IsActive)
            {
                sm.UserSession = null;
                filterContext.Result = new RedirectToRouteResult("Login", new RouteValueDictionary { });
             
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }


        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            //_mDal.LogMe("Exception", message, null);
        }
    }
}