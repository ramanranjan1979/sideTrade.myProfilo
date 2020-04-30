using sideTrade.Dal.DAL;
using System.Web;
using System.Web.Mvc;


namespace sideTrade.myProfilo.WebApp.Filter
{
    public class AuthLogin : AuthorizeAttribute
    {
        private LogDal dal = new LogDal();
        
        public string AccountType { get; set; } = string.Empty;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            SessionManager sm = new SessionManager();

            if (sm.UserSession == null)
            {
                return false;
            }
            else
            {
                if (sm.UserSession.IsActive == true)
                {
                   
                        switch (AccountType.ToLower())
                        {
                            case "administrator":
                                if (sm.UserSession.RoleNameList.Contains(AccountType.ToLower()))
                                {
                                    return true;
                                }
                                else
                                {
                                    //TO DO:
                                    //var email = dal.GetMemberEmailByMemberId(sm.UserSession.Id);
                                    //_mDal.LogSocialAction(email, "LOG OUT", sm.UserSession.MemberId, $"Member Tried to access admin area");

                                    sm.UserSession = null;
                                    return false;
                                }
                            case "user":
                                if (sm.UserSession.RoleNameList.Contains(AccountType.ToLower()))
                                {
                                    return true;
                                }
                                else
                                {
                                   //TO DO
                                    //var email = dal.GetMemberEmailByMemberId(sm.UserSession.Id);
                                    //_mDal.LogSocialAction(email, "LOG OUT", sm.UserSession.MemberId, $"Member Tried to access admin area");

                                    sm.UserSession = null;
                                    return false;
                                }
                            default:
                                return true;
                        }
                   
                }
                else
                {
                    return false;
                }
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult("Login", new System.Web.Routing.RouteValueDictionary { });
        }
    }
}