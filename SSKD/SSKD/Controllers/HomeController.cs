using SSKD.Models;
using SSKD.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSKD.Controllers
{
    public class HomeController : GuestController
    {
        public ActionResult Index()
        {
            var dict = new Dictionary<string, object>();
            return View(dict);
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

        public ActionResult GetCaptchaRandom()
        {
            ViewBag.ImageData = DefaultView.CreateImgCaptcha(DefaultView.RandomCapcha());
            return PartialView("_CaptchaRandomPartial");
        }

        //===============================================Cac ham khac======================================================        

        public ActionResult Download(string urlFolder, string file)
        {
            string fileName = urlFolder + file;
            string fullPath = Path.Combine(Server.MapPath("~/"), fileName);
            return File(fullPath, System.Net.Mime.MediaTypeNames.Application.Octet, file);
        }
    }
}