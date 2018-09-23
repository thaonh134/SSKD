using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SSKD.Areas.Admin.Models;
using SSKD.Models;
using SSKD.Service;

namespace SSKD.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            ViewBag.message = "";
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "" : returnUrl;

            if (DefaultView.GetRandomCapcha() != model.CaptchaCode) {
                ViewBag.message = "Mã xác minh không đúng.";
               return View();
            }


            if (ModelState.IsValid)
            {

                IDbConnection dbConn = new OrmliteConnection().openConn();
                var user = SSKD.Areas.Admin.Models.AuthUser.GetByCode(model.UserName, null, false);
                if (new AccountMembershipService().ValidateUser(model.UserName, model.Password))
                {
                    //FormsAuthentication.SetAuthCookie(model.UserName, true);

                    //FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);

                    var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name,user.entrycode),
                new Claim(ClaimTypes.Email,string.IsNullOrEmpty( user.email)?"":user.email),
                new Claim(ClaimTypes.PrimarySid,user.entryid.ToString())
            },
                    "ApplicationCookie");
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);

                    if (Url.IsLocalUrl(returnUrl) &&
                    returnUrl.Length > 1 &&
                    returnUrl.StartsWith("/") &&
                    !returnUrl.StartsWith("//") &&
                    !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");

                }
            }
            return View(model);
        }


        public ActionResult LogOut()
        {
            //FormsAuthentication.SignOut();
            var ctx = Request.GetOwinContext();
            ctx.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                    DefaultAuthenticationTypes.ExternalCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel item)
        {
            try
            {
                if (new ChangePasswordModel().GetUserByUserNameAndPassword(item.UserNameChange, SqlHelper.GetMd5Hash(item.OldPass)))
                {
                    item.RepeatNewPass = SqlHelper.GetMd5Hash(item.RepeatNewPass);
                    item.ChangePassword();
                    return Json(new { success = true, message = "Thành công" });
                }
                return Json(new { success = false, message = "Mật khẩu không đúng" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
            finally
            {
            }
        }

        public string RemoveSpecialCharacter(string str)
        {
            string str_rs = str;
            str_rs = str_rs.Replace("&", "&#38;");
            str_rs = str_rs.Replace("<", "&lt;");
            str_rs = str_rs.Replace(">", "&gt;");
            str_rs = str_rs.Replace("\"", "&#34;");
            str_rs = str_rs.Replace("'", "&#39;");
            str_rs = str_rs.Replace("\\", "&#92;");
            str_rs = str_rs.Replace("=", "&#61;");
            str_rs = str_rs.Replace("(", "&#40;");
            str_rs = str_rs.Replace(")", "&#41;");
            str_rs = str_rs.Replace("--", "&#45;&#45;");
            str_rs = str_rs.Replace("|", "&#124;");
            return str_rs;
        }
    }
}