using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sideTrade.myProfilo.WebApp.Controllers
{
    public class ErrorController : Controller
    {
        
        public ActionResult Index()
        {
            var msg = ViewData["Error_Msg"].ToString();
            return View("Error");
        }
    }
}