using sideTrade.myProfilo.webApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using sideTrade.Dal.DAL;
using sideTrade.myProfilo.WebApp.Filter;

namespace sideTrade.myProfilo.WebApp.Controllers
{
    //[AuthLogin(AccountType = "Adminstrator")]
    public class FileController : BaseController
    {
        // GET: File
        public ActionResult GetAllFiles()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/Files/GetAllFiles");
                response.EnsureSuccessStatusCode();
                List<FileManagerViewModel> files = response.Content.ReadAsAsync<List<FileManagerViewModel>>().Result;
                ViewBag.Title = "All Files";
                return View(files);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UploadFileViewModel pro)
        {
            string UploadResult = "Your file has been uploaded successfully";
            ServiceRepository serviceObj = new ServiceRepository();            
            pro.FileViewModel.ProfileId = sm.UserSession.ProfileId;
            pro.FileViewModel.Path = pro.FileToUpload.FileName;
            var pathToSave = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + System.Configuration.ConfigurationManager.AppSettings["upload"] + "FileRepository", $"ProfileID-{pro.FileViewModel.ProfileId}").Replace("\\", @"\");


            //Insert File Info
            FileManagerViewModel fMVM = new FileManagerViewModel()
            {
                Path = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["upload"] + "FileRepository", $"ProfileID-{pro.FileViewModel.ProfileId}"),
                ProfileId = sm.UserSession.ProfileId,
                FileName = pro.FileToUpload.FileName,
                Comment = $"{pro.FileToUpload.FileName} has upload a file",
                FileManagerTypeId = 1, // TO DO : Dynamically
                Mode = FileManagerMode.UPLOAD.ToString(),
                Status=FileManagerStatus.CREATED.ToString() 
            };
            HttpResponseMessage response = serviceObj.PostResponse("api/FilesManager/InsertFile", fMVM);
            var d = response.EnsureSuccessStatusCode();

            //Upload the File
            var f = FileManagerDal.GetLastFileUploaded(pro.FileViewModel.ProfileId, pro.FileToUpload.FileName);

            if (UploadIsSuccess(pro.FileViewModel.ProfileId, pro.FileToUpload, pathToSave))
            {
                //Update the fileInfo
                fMVM.Id = f == null ? 1 : f.Id;
                HttpResponseMessage response1 = serviceObj.PutResponse("api/FilesManager/UpdateFile", fMVM);
            }
            else
            {
                fMVM.Id = f.Id;
                HttpResponseMessage response1 = serviceObj.DeleteResponse($"api/FilesManager/DeleteFile/{fMVM.Id}");
                //System.IO.File.Delete(pathToSave);
                UploadResult = "Sorry! Please select only .cs file less than 3 MB in size";
            }

            ViewBag.Result = UploadResult;

            if (sm.UserSession.RoleNameList.Contains("administrator"))
            {
                return RedirectToAction("GetAllFiles","Admin");

            }
            else
            {
                return RedirectToAction("MyFiles", "Account");
            }
        }

        private bool UploadIsSuccess(int profileId, HttpPostedFileBase fileToUpload, string pathToSave)
        {
            bool isSuccess = true;
            try
            {
                if (ValidateFileBeforeUpload(fileToUpload, 1024 * 1024 * 3, ".cs")) // 3 MB MAX
                {
                    string memberFolder = pathToSave;
                    if (Directory.Exists(memberFolder) == false)
                    {
                        Directory.CreateDirectory(memberFolder);
                    }
                    var fullPath = Path.Combine(memberFolder, fileToUpload.FileName);
                    fileToUpload.SaveAs(fullPath);
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public static bool ValidateFileBeforeUpload(HttpPostedFileBase file, int maxFileLengthInMB, string fileExtensions)
        {
            bool okToUpload = true;
            if (file == null)
            {
                okToUpload = false;
            }

            if (file.ContentLength > 0)
            {
                string[] AllowedFileExtensions = fileExtensions.Split(',');
                if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    okToUpload = false;
                }

                else if (file.ContentLength > maxFileLengthInMB)
                {
                    okToUpload = false;
                }
                else
                {
                    okToUpload = true;
                }
            }
            else
            {
                okToUpload = false;
            }

            return okToUpload;
        }



        public ActionResult FileContentDetails(int fileId)
        {
            var msg = string.Empty;

            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse($"api/FilesManager/GetFile?fileId={fileId}");
                response.EnsureSuccessStatusCode();
                FileManagerViewModel file = response.Content.ReadAsAsync<FileManagerViewModel>().Result;
                //string rawText = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + file.Path + "\\" + file.FileName).Replace("\\", @"\"));

                int counter1 = 0, counter2 = 0, counter3 = 0;
                string line;

                StreamReader fileInput = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + file.Path + "\\" + file.FileName).Replace("\\", @"\"));

                while ((line = fileInput.ReadLine()) != null)
                {
                    if (line.Contains("()"))
                    {
                        counter1++;
                    }

                    if (line.Contains("[]"))
                    {
                        counter2++;
                    }

                    if (line.Contains("{}"))
                    {
                        counter3++;
                    }
                }

                fileInput.Close();


                //var string1 = rawText.Replace("()", "-)(-");
                //string[] separatingStrings1 = { "-)(-" };
                //string[] words1 = string1.Split(separatingStrings1, System.StringSplitOptions.RemoveEmptyEntries);

                //var string2 = rawText.Replace("[]", "-][-");
                //string[] separatingStrings2 = { "-][-" };
                //string[] words2 = string2.Split(separatingStrings2, System.StringSplitOptions.RemoveEmptyEntries);

                //var string3 = rawText.Replace("{}", "-}{-");
                //string[] separatingStrings3 = { "-}{-" };
                //string[] words3 = string2.Split(separatingStrings2, System.StringSplitOptions.RemoveEmptyEntries);

                //var count1 = words1.Count();
                //var count2 = words2.Count();
                //var count3 = words3.Count();
                //msg = $"This file contain {count1} '()' , {count2} '[]' and " + count3 + "'{}'";
                msg = $"This file contain {counter1} '()' , {counter2} '[]' and " + counter3 + "'{}'";
            }
            catch (Exception ex)
            {
                msg = ex.InnerException.Message;
            }


            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(msg), "application/json");
        }


    }
}