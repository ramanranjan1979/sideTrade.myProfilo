using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sideTrade.Dal.DAL
{
    public class LogDal
    {
        static SideTrade_DBEntities DbContext;
        static LogDal()
        {
            DbContext = new SideTrade_DBEntities();
        }

        public static List<Log> GetAllLog()
        {
            return DbContext.Log.ToList();
        }

        public static List<Log> GetAllLog(int profileId, int logTypeId)
        {
            return DbContext.Log.Where(x => x.ProfileId.Value == profileId && x.LogTypeId == logTypeId).ToList();
        }

        public static Log InsertLog(Log data)
        {
            bool status;
            try
            {
                DbContext.Log.Add(data);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                status = false;
            }
            return data;
        }

        public static LogType InsertLogType(LogType data)
        {
            bool status;
            try
            {
                DbContext.LogType.Add(data);
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
