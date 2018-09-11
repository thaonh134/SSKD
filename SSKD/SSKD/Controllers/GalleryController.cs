using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSKD.Controllers
{
    public class GalleryController : Controller
    {
        public ActionResult Index()
        {
            var dict = new Dictionary<string, object>();
            return View(dict);
        }
      
       
    }
}