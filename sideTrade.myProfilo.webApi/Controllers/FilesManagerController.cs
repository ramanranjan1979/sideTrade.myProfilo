using sideTrade.Dal;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using sideTrade.Dal.DAL;
using System.Web;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace sideTrade.myProfilo.webApi.Controllers
{
    public class FilesManagerController : ApiController
    {
        [HttpGet]
        public JsonResult<List<Models.FileManager>> GetAllFiles()
        {
            EntityMapperFileManager<FileManager, Models.FileManager> mapObj = new EntityMapperFileManager<FileManager, Models.FileManager>();
            List<FileManager> fileList = FileManagerDal.GetAllFile();
            List<Models.FileManager> fList = new List<Models.FileManager>();
            foreach (var item in fileList)
            {
                fList.Add(mapObj.Translate(item));
            }
            return Json(fList);
        }

        [HttpGet]
        public JsonResult<List<Models.FileManager>> GetAllFilesByProfileId(int profileId,int? fileTypeId)
        {
            EntityMapperFileManager<FileManager, Models.FileManager> mapObj = new EntityMapperFileManager<FileManager, Models.FileManager>();
            List<FileManager> fileList = FileManagerDal.GetAllFileByProfileId(profileId, fileTypeId);
            List<Models.FileManager> fList = new List<Models.FileManager>();
            foreach (var item in fileList)
            {
                fList.Add(mapObj.Translate(item));
            }
            return Json(fList);
        }


        [HttpGet]
        public JsonResult<Models.FileManager> GetFile(int fileId)
        {
            EntityMapper<FileManager, Models.FileManager> mapObj = new EntityMapper<FileManager, Models.FileManager>();
            FileManager dalFile = FileManagerDal.GetFile(fileId);
            Models.FileManager file = new Models.FileManager();
            file = mapObj.Translate(dalFile);
            return Json(file);
        }

        [HttpPost]
        public Models.FileManager InsertFile(Models.FileManager p)
        {
            if (ModelState.IsValid)
            {
                EntityMapperFileManager<Models.FileManager, FileManager> mapObj = new EntityMapperFileManager<Models.FileManager, FileManager>();
                FileManager fileObj = new FileManager();
                fileObj = mapObj.Translate(p);
                FileManagerDal.InsertFile(fileObj);
            }
            return p;
        }

        [HttpPost]
        public Models.FileManagerType InsertFileType(Models.FileManagerType p)
        {
            if (ModelState.IsValid)
            {
                EntityMapperFileManagerType<Models.FileManagerType, FileManagerType> mapObj = new EntityMapperFileManagerType<Models.FileManagerType, FileManagerType>();
                FileManagerType fileObj = new FileManagerType();
                fileObj = mapObj.Translate(p);
                FileManagerDal.InsertFileManagerType(fileObj);
            }
            return p;
        }

        [HttpPut]
        public bool UpdateFile(Models.FileManager f)
        {
            EntityMapperFileManager<Models.FileManager, FileManager> mapObj = new EntityMapperFileManager<Models.FileManager, FileManager>();
            FileManager fileObj = new FileManager();
            fileObj = mapObj.Translate(f);
            var status = FileManagerDal.UpdateFile(fileObj);
            return status;

        }
        [HttpDelete]
        public bool DeleteFile(int id)
        {
            var status = FileManagerDal.DeleteFile(id);
            return status;
        }

    }
}