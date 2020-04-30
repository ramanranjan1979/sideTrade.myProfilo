using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sideTrade.Dal.DAL
{
    public class ProfileMappingDal
    {
        static SideTrade_DBEntities DbContext;
        static ProfileMappingDal()
        {
            DbContext = new SideTrade_DBEntities();
        }

        public static List<ProfileMapping> GetAllProfileMapping()
        {
            return DbContext.ProfileMapping.ToList();
        }

        public static List<ProfileMapping> GetProfileMapping(int profileId)
        {
            return DbContext.ProfileMapping.Where(p => p.ProfileId == profileId).ToList();

        }

        public static bool InsertProfileMapping(ProfileMapping map)
        {
            bool status;
            try
            {
                DbContext.ProfileMapping.Add(map);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public static bool UpdateProfileMapping(ProfileMapping updateMapping)
        {
            bool status;
            try
            {
                ProfileMapping item = DbContext.ProfileMapping.Where(p => p.Id == updateMapping.Id).FirstOrDefault();
                if (item != null)
                {
                    item.ProfileTypeId = updateMapping.ProfileTypeId;
                    item.ProfileId = updateMapping.ProfileId;
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
        public static bool DeleteProfileMapping(int id)
        {
            bool status;
            try
            {
                ProfileMapping map = DbContext.ProfileMapping.Where(p => p.Id == id).FirstOrDefault();
                if (map != null)
                {
                    DbContext.ProfileMapping.Remove(map);
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
