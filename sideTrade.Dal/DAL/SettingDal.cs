using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sideTrade.Dal.DAL
{
    public class SettingDal
    {
        static SideTrade_DBEntities DbContext;
        static SettingDal()
        {
            DbContext = new SideTrade_DBEntities();
        }

        public static List<Settings> GetAllSetting()
        {
            return DbContext.Settings.ToList();
        }

        public static Settings GetSettingByName(string settingName)
        {
            return DbContext.Settings.Where(x => x.Name.Equals(settingName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public static Settings InsertSetting(Settings data)
        {
            bool status;
            try
            {
                DbContext.Settings.Add(data);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                status = false;
            }
            return data;
        }

    }
}
