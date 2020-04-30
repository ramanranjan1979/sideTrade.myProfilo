using sideTrade.Dal;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using sideTrade.Dal.DAL;
namespace sideTrade.myProfilo.webApi.Controllers
{
    public class LoginsController : ApiController
    {
        


        [HttpPost]
        public Models.Login InsertLogin(Models.Login p)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                EntityMapperLogin<Models.Login, Login> mapObj = new EntityMapperLogin<Models.Login, Login>();
                Login obj = new Login();
                obj = mapObj.Translate(p);
                LoginDal.InsertLogin(obj);
            }
            return p;

        }
        [HttpPut]
        public bool UpdateLogin(Models.Login login)
        {
            EntityMapperLogin<Models.Login, Login> mapObj = new EntityMapperLogin<Models.Login, Login>();
            Login obj = new Login();
            obj = mapObj.Translate(login);
            var status = LoginDal.UpdateLogin(obj);
            return status;

        }
       
    }
}