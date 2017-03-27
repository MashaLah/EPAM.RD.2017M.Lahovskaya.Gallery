using Gallery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {

        private readonly Random _random = new Random(DateTime.Now.Millisecond);
        private static int MaxStar = 5;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetImages()
        {
            var serverPath = Server.MapPath("~");
            var pathToImageFolder = Path.Combine(serverPath, "Content", "images");
            var imageFiles = Directory.GetFiles(pathToImageFolder);
            var imges = imageFiles.Select(BuildImage);
            return Json(imges, JsonRequestBehavior.AllowGet);
        }

        private Image BuildImage(string path)
        {
            var fileName = Path.GetFileName(path);
            var image = new Image
            {
                Url = Url.Content("~/Content/images/" + fileName),
                Name = Path.GetFileNameWithoutExtension(path),
                Extension = Path.GetExtension(path),
                Star = _random.Next(MaxStar)
            };

            return image;
        }
    }
}