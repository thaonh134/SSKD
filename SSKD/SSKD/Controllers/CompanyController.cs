using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSKD.Controllers
{
    public class CompanyController : Controller
    {
        public ActionResult Index()
        {
            var dict = new Dictionary<string, object>();
            return View(dict);
        }
        public ActionResult Info()
        {
            var dict = new Dictionary<string, object>();
            return View(dict);
        }
        public ActionResult Location()
        {
            var dict = new Dictionary<string, object>();
            return View(dict);
        }
    }
}