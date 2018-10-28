using SSKD.Areas.Admin.Models;
using SSKD.ConstantValue;
using SSKD.Models;
using SSKD.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> SaveContactRequest(FE_ContactRequest Item)
        {
            //valid
            if (DefaultView.GetRandomCapcha() != Item.CaptchaCode) return Json(new { success = false, message = "Mã xác minh không đúng." });
            var userID = ViewData["AuthUser"] == null ? 0 : ((AuthUser)ViewData["AuthUser"]).entryid;
           
            int resutl = FE_ContactRequest.SaveContactRequest(userID, Item);

            //send email
            string subject = "[sskd] - vừa có 1 đơn hàng mới";
            //send ContactEmail
            await sendContactEmail(new List<string>() { ConfigurationManager.AppSettings.Get("EmailContact").ToString() }, subject, Item);

            return Json(new { success = resutl > 0 });
        }

        public async Task sendContactEmail(List<string> emailsfrome, string subject, FE_ContactRequest item)
        {
            #region sent email 
            //begin sendemail
            using (var sr = System.IO.File.OpenText(System.Web.Hosting.HostingEnvironment.MapPath(@"~/EmailTemplates/ContactEmail.html")))
            {
                var defaulUrl = ConfigurationManager.AppSettings.Get("DefaultServerUrl");
                var emailContent = sr.ReadToEnd();
                emailContent = emailContent
                    .Replace(EmailKeyword.FULL_NAME, item.FullName)
                    .Replace(EmailKeyword.EMAIL_ADDRESS, item.Email)
                    .Replace(EmailKeyword.PHONE_NUMBER, item.Phone)
                    .Replace(EmailKeyword.DATE_SEND, DateTime.Now.ToString("dd/MM/yyyy") + "(dd/MM/yyyy)")
                    .Replace(EmailKeyword.CONTENT_STR, item.Comments);

                await new SendEmailService().SendEmail(emailsfrome, subject, emailContent);
            }
            #endregion
        }

    }
}