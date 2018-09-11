using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSKD.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(string Type)
        {
            var dict = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(Type)) Type = "DESKPAD";
            dict["data_Type"] =Type.ToLower();
            return View(dict);
        }
      
       
    }
}