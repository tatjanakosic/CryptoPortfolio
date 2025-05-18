using Common;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Web;
using System.Web.Mvc;

namespace PortfolioService.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update(String Name, String LastName, HttpPostedFileBase file, String RowKey, String Password, String Address, String City, String Country, String PhoneNumber)
        {

            string uniqueBlobName = string.Format("image_{0}", file);
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionStringBlob"));
            CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobStorage.GetContainerReference("vezba");
            CloudBlockBlob blob = container.GetBlockBlobReference(uniqueBlobName);
            if (file != null)
            {
                blob.Properties.ContentType = file.ContentType;
                // postavljanje odabrane datoteke (slike) u blob servis koristeci blob klijent

                blob.UploadFromStream(file.InputStream);
                // upis studenta u table storage koristeci StudentDataRepository klasu

                HomeController.repo.UpdateUser(new Common.User(RowKey) { Name = Name, LastName = LastName, PhotoUrl = blob.Uri.ToString(), Password = Password, Address = Address, City = City, Country = Country, PhoneNumber = PhoneNumber, ETag = "*" });
            }
            else
            {
                User userold = (User)HttpContext.Application["user"];
                HomeController.repo.UpdateUser(new Common.User(RowKey) { Name = Name, LastName = LastName, PhotoUrl=userold.PhotoUrl, Password = Password, Address = Address, City = City, Country = Country, PhoneNumber = PhoneNumber, ETag = "*" });
            }

           
           
            User u = (User)HttpContext.Application["user"];
            if (u != null)
            {
                HttpContext.Application["user"] = HomeController.repo.ExistsUser(RowKey, u.Password);
                ViewBag.user = (User)HttpContext.Application["user"];
            }
            else
            {
                ViewBag.user = new User();
            }
            return RedirectToAction("MyProfile", "Home");
        }
    }
}