using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sideTrade.Dal.DAL
{
    public class FileManagerDal
    {
        static SideTrade_DBEntities DbContext;
        static FileManagerDal()
        {
            DbContext = new SideTrade_DBEntities();
        }
        public static List<FileManager> GetAllFile()
        {
            return DbContext.FileManager.ToList();
        }

        public static List<FileManager> GetAllFileByProfileId(int profileId, int? fileTypeId)
        {
            return DbContext.FileManager.Where(x => x.ProfileId == profileId && (fileTypeId.HasValue ? fileTypeId.Value == x.FileManagerTypeId : 1 == 1)).ToList();
        }

        public static FileManager GetFile(int fileId)
        {
            return DbContext.FileManager.Where(p => p.Id == fileId).FirstOrDefault();
        }

        public static FileManager GetLastFileUploaded(int profileId, string fileName)
        {
            return DbContext.FileManager.Where(p => p.ProfileId == profileId && p.FileName.Equals(fileName, StringComparison.OrdinalIgnoreCase)).OrderByDescending(x => x.Id).FirstOrDefault();
        }

        public static FileManager InsertFile(FileManager fileManager)
        {
            bool status;
            try
            {
                DbContext.FileManager.Add(fileManager);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return fileManager;
        }

        public static FileManagerType InsertFileManagerType(FileManagerType type)
        {
            bool status;
            try
            {
                DbContext.FileManagerType.Add(type);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return type;
        }

        public static bool UpdateFile(FileManager file)
        {
            bool status;
            try
            {
                FileManager item = DbContext.FileManager.Where(p => p.Id == file.Id).FirstOrDefault();
                if (item != null)
                {
                    item.Mode = file.Mode;
                    item.CreatedOn = file.CreatedOn;
                    item.Path = file.Path;
                    item.ProfileId = file.ProfileId;
                    item.FileName = file.FileName;
                    item.Comment = file.Comment;
                    item.Status = file.Status;
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
        public static bool DeleteFile(int fileId)
        {
            bool status;
            try
            {
                FileManager file = DbContext.FileManager.Where(p => p.Id == fileId).FirstOrDefault();
                if (file != null)
                {
                    DbContext.FileManager.Remove(file);
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
