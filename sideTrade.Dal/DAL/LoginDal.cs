using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sideTrade.Dal.DAL
{
    public  class LoginDal
    {
        static SideTrade_DBEntities DbContext;
        static LoginDal()
        {
            DbContext = new SideTrade_DBEntities();
        }
        public static List<Login> GetAllLogin()
        {
            return DbContext.Login.ToList();
        }

        public static Login GetLogin(int login)
        {
            return DbContext.Login.Where(p => p.Id == login).FirstOrDefault();
        }

        public static Login GetLoginByProfileId(int profileId)
        {
            return DbContext.Login.Where(p => p.Profile_Id == profileId).FirstOrDefault();
        }


        public static Login InsertLogin(Login data)
        {
            bool status;
            try
            {
                DbContext.Login.Add(data);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return data;
        }

        public static bool UpdateLogin(Login item)
        {
            bool status;
            try
            {
                Login data = DbContext.Login.Where(p => p.Id == item.Id).FirstOrDefault();
                if (data != null)
                {
                    data.Id = item.Id;
                    data.IsActive = item.IsActive;
                    data.Password = item.Password;
                    data.ModifiedOn = item.ModifiedOn;
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
        public static bool DeleteLogin(int id)
        {
            bool status;
            try
            {
                Login l = DbContext.Login.Where(p => p.Id == id).FirstOrDefault();
                if (l != null)
                {
                    DbContext.Login.Remove(l);
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
    }
}
