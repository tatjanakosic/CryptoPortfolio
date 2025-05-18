using Common;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortfolioService.Controllers
{
    public class HomeController : Controller
    {
        public static UserDataRepository repo = new UserDataRepository();
        //public static LogDataRepository repodata = new LogDataRepository();
        public ActionResult Index()
        {
            //repodata.AddLog(new Log("string"));
            return View();
        }
       

        [HttpPost]
        public ActionResult Register(String Name, String LastName, HttpPostedFileBase file, String Email, String Password, String Address, String City, String Country, String PhoneNumber)
        {
            try
            {
                if (repo.Exists(Email))
                {
                    return View("Error");
                }

                // kreiranje blob sadrzaja i kreiranje blob klijenta
                string uniqueBlobName = string.Format("image_{0}", Email);
                var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionStringBlob"));
                CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobStorage.GetContainerReference("vezba");
                CloudBlockBlob blob = container.GetBlockBlobReference(uniqueBlobName);
                blob.Properties.ContentType = file.ContentType;
                // postavljanje odabrane datoteke (slike) u blob servis koristeci blob klijent
                blob.UploadFromStream(file.InputStream);
                // upis studenta u table storage koristeci StudentDataRepository klasu
                User entry = new User(Email) { Name = Name, LastName = LastName, PhotoUrl = blob.Uri.ToString(), Password = Password, Address=Address, City=City, Country=Country, PhoneNumber=PhoneNumber   /*, ThumbnailUrl = blob.Uri.ToString() */};
                repo.AddUser(entry);
                ViewBag.user = entry;
                HttpContext.Application["user"] = entry;
                //CloudQueue queue = QueueHelper.GetQueueReference("vezba");
                //queue.AddMessage(new CloudQueueMessage(RowKey), null, TimeSpan.FromMilliseconds(30));

                //return RedirectToAction("ShowAllUsers");
                return View("MyProfile");
            }
            catch
            {
                return View("Index");
            }
        }

        public ActionResult ShowAllUsers()
        {
            return View(repo.RetrieveAllUsers());
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Application["user"] = null;
            return RedirectToAction("LogIn");
        }

        public ActionResult LogInWithParameters(string email, string password)
        {
            User user = repo.ExistsUser(email, password);
            if(user != null)
            {
                HttpContext.Application["user"] = user;
                ViewBag.user = user;
                return View("MyProfile");
            }
            else
            {
                ViewBag.errorLogIn = "Neispravni kredencijali";
                return View("LogIn");
            }
        }

        public ActionResult MyProfile()
        {
            ViewBag.user = (User)HttpContext.Application["user"];
            return View("MyProfile");
        }

       
    }
}