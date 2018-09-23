using SSKD.Areas.Admin.Models;
using SSKD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SSKD.Models.DefaultView;

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

        //Bill checkout
        public ActionResult SaveContactRequest(FE_ContactRequest Item)
        {
            //valid
            if (DefaultView.GetRandomCapcha() != Item.CaptchaCode) return Json(new { success = false, message = "Mã xác minh không đúng." });
            var userID = ViewData["AuthUser"] == null ? 0 : ((AuthUser)ViewData["AuthUser"]).entryid;
           
            int resutl = FE_ContactRequest.SaveContactRequest(userID, Item);
            return Json(new { success = resutl > 0 });
        }

    }
}