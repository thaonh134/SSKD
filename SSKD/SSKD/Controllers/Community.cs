using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSKD.Controllers
{
    public class CommunityController : Controller
    {
        public ActionResult Index()
        {
            var dict = new Dictionary<string, object>();
            return View(dict);
        }
        public ActionResult Notice()
        {
            var dict = new Dictionary<string, object>();
            return View(dict);
        }
        public ActionResult Faq()
        {
            var dict = new Dictionary<string, object>();
            return View(dict);
        }
        public ActionResult Qna()
        {
            var dict = new Dictionary<string, object>();
            return View(dict);
        }

    }
}