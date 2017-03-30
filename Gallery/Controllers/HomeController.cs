using Gallery.Models;
using Gallery.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        private DbContext db = new GalleryEntities1();
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
                Url = image.ImagePath,
                Date = image.Date,
                AlbumId = image.AlbumId,
                Extension=image.ImageMimeType,
                Name=image.Name
            }).ToList();
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAlbums()
        {
            var albums = db.Set<ORM.Album>().Select(album => new Models.Album
            {
                Id = album.Id,
                Name = album.Name
            }).ToList();
            return Json(albums, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveImage(string title, string src, int albumId)
        {
            var dataIndex = src.IndexOf("base64", StringComparison.Ordinal) + 7;
            var cleareData = src.Substring(dataIndex);
            var fileData = Convert.FromBase64String(cleareData);
            var bytes = fileData.ToArray();

            string[] dataSegments = src.Split(',');
            string extension = dataSegments[0].Split('/', ';')[1];

            var serverPath = Server.MapPath("~");
            var path = Path.Combine(serverPath, "Content","images", String.Format("{0}.{1}", title, extension));
            using (var fileStream = System.IO.File.Create(path))
            {
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Close();
            }
            var image = new Picture()
            {
                Name = title,
                AlbumId = albumId,
                ImageMimeType = extension,
                Date = DateTime.Now,
                ImagePath = String.Format("/Content/images/{0}.{1}", title, extension)
            };
            this.db.Set<Picture>().Add(image);
            //this.db.SaveChanges();
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


}
}