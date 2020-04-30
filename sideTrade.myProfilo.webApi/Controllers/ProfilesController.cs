using sideTrade.Dal;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using sideTrade.Dal.DAL;
namespace sideTrade.myProfilo.webApi.Controllers
{
    public class ProfilesController : ApiController
    {
        [HttpGet]
        public JsonResult<List<Models.Profile>> GetAllProfiles()
        {
            EntityMapper<Profile, Models.Profile> mapObj = new EntityMapper<Profile, Models.Profile>();
            List<Profile> profileList = ProfileDal.GetAllProfiles();
            List<Models.Profile> profile = new List<Models.Profile>();
            foreach (var item in profileList)
            {
                profile.Add(mapObj.Translate(item));
            }
            return Json(profile);
        }
        [HttpGet]
        public JsonResult<Models.Profile> GetProfile(int id)
        {
            EntityMapper<Profile, Models.Profile> mapObj = new EntityMapper<Profile, Models.Profile>();
            Profile dalProfile = ProfileDal.GetProfile(id);
            Models.Profile profile = new Models.Profile();
            profile = mapObj.Translate(dalProfile);
            return Json(profile);
        }

        [HttpGet]
        public JsonResult<Models.Profile> GetProfileByEmail(string emailAddress)
        {
            EntityMapper<Profile, Models.Profile> mapObj = new EntityMapper<Profile, Models.Profile>();
            Profile dalProfile = ProfileDal.GetProfileByEmail(emailAddress);
            Models.Profile profile = new Models.Profile();
            profile = mapObj.Translate(dalProfile);
            return Json(profile);
        }


        [HttpPost]
        public Models.Profile InsertProfile(Models.Profile p)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                EntityMapper<Models.Profile, Profile> mapObj = new EntityMapper<Models.Profile, Profile>();
                Profile profileObj = new Profile();
                profileObj = mapObj.Translate(p);
                ProfileDal.InsertProfile(profileObj);
            }
            return p;

        }
        [HttpPut]
        public bool UpdateProfile(Models.Profile profile)
        {
            EntityMapper<Models.Profile, Profile> mapObj = new EntityMapper<Models.Profile, Profile>();
            Profile productObj = new Profile();
            productObj = mapObj.Translate(profile);
            var status = ProfileDal.UpdateProfile(productObj);
            return status;

        }
        [HttpDelete]
        public bool DeleteProfile(int id)
        {
            var status = ProfileDal.DeleteProfile(id);
            return status;
        }

        [HttpPost]
        public bool InsertProfileRole(Models.ProfileMapping p)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                EntityMapperProfileRole<Models.ProfileMapping, ProfileMapping> mapObj = new EntityMapperProfileRole<Models.ProfileMapping, ProfileMapping>();
                ProfileMapping obj = new ProfileMapping();
                obj = mapObj.Translate(p);
                status = ProfileMappingDal.InsertProfileMapping(obj);
            }
            return status;
        }

    }
}