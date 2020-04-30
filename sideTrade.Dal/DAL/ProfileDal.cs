using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sideTrade.Dal.DAL
{
    public class ProfileDal
    {
        static SideTrade_DBEntities DbContext;
        static ProfileDal()
        {
            DbContext = new SideTrade_DBEntities();
        }
        public static List<Profile> GetAllProfiles()
        {
            return DbContext.Profile.ToList();
        }

        public static Profile GetProfile(int profileId)
        {
            return DbContext.Profile.Where(p => p.Id == profileId).FirstOrDefault();
        }


        public static Profile InsertProfile(Profile newProfile)
        {
            bool status;
            try
            {
                DbContext.Profile.Add(newProfile);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return newProfile;
        }

        public static bool UpdateProfile(Profile profileItem)
        {
            bool status;
            try
            {
                Profile item = DbContext.Profile.Where(p => p.Id == profileItem.Id).FirstOrDefault();
                if (item != null)
                {
                    item.FirstName = profileItem.FirstName;
                    item.LastName = profileItem.LastName;
                    item.IsActive = profileItem.IsActive;
                    item.CreatedOn = profileItem.CreatedOn;
                    item.ModifiedOn = profileItem.ModifiedOn;
                    item.EmailAddress = profileItem.EmailAddress;
                    item.IsInvited = profileItem.IsInvited;
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
        public static bool DeleteProfile(int id)
        {
            bool status;
            try
            {
                Profile prodItem = DbContext.Profile.Where(p => p.Id == id).FirstOrDefault();
                if (prodItem != null)
                {
                    DbContext.Profile.Remove(prodItem);
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

        public static Profile GetProfileByEmail(string emailAddress)
        {
            return DbContext.Profile.Where(p => p.EmailAddress.Equals(emailAddress, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public static ProfileType InsertProfileType(ProfileType type)
        {
            bool status;
            try
            {
                DbContext.ProfileType.Add(type);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return type;
        }
    }
}
