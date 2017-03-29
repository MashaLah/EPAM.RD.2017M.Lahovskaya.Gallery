using Gallery.Models;
using Gallery.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        private DbContext db = new GalleryEntities();
        private readonly Random _random = new Random(DateTime.Now.Millisecond);

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetImages()
        {
            var images = db.Set<Picture>().Select(image => new Image
            {
                Id = image.Id,
                Url = image.Image,
                Date = image.Date,
                AlbumId = image.AlbumId
            }).ToList();
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult AddImage(string fileName, string data)
         { 
             var dataIndex = data.IndexOf("base64", StringComparison.Ordinal) + 7; 
             var cleareData = data.Substring(dataIndex); 
             var fileData = Convert.FromBase64String(cleareData); 
             var bytes = fileData.ToArray(); 
 
 
             var path = GetPathToImg(fileName); 
             using (var fileStream = System.IO.File.Create(path)) 
             { 
                 fileStream.Write(bytes, 0, bytes.Length); 
                 fileStream.Close(); 
             } 
 
 
             return Json(true, JsonRequestBehavior.AllowGet); 
         }

        private string GetPathToImg(string fileName)
         { 
             var serverPath = Server.MapPath("~"); 
             return Path.Combine(serverPath, "Content", "images", fileName); 
         }


}
}