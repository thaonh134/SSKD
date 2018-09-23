using SSKD.Areas.Admin.Models;
using SSKD.Resources;
using SSKD.Service;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using System.Text;

namespace SSKD.Areas.Admin.Controllers
{
    public class MasterHomePageManagement2Controller : CustomController
    {
        #region Tree
        public ActionResult Index(string redirectbyajax, string entryid, string actiontype, string fa)
        {
            //fa: form another - default form popup
            IDbConnection dbConn = new OrmliteConnection().openConn();
            var dict = new Dictionary<string, object>();
            dict["activestatus"] = CustomModel.GetActiveStatus();
            dict["listlanguage"] = CustomModel.GetLanguage();
            dict["areasname"] = "Admin";
            dict["redirectbyajax"] = string.IsNullOrEmpty(redirectbyajax) ? "0" : "1";
            dbConn.Close();


            var dataModel = HomePage.GetLast();
            if (dataModel == null) { dataModel = new HomePage(); }
            dict["dataModel"] = dataModel;
            dbConn.Close();

            //param cho form
            ViewBag.entryid = dataModel.entryid==0 ? "0" : dataModel.entryid.ToString();
            ViewBag.actiontype = string.IsNullOrEmpty(actiontype) ? "" : actiontype;
            ViewBag.fa = string.IsNullOrEmpty(fa) ? "" : fa;
            return View("HomePageManagementTree", dict);

        }
        

        //===============================================Import export==================================================

        //===============================================Openpopup======================================================        


        #endregion
        #region Form
        [HttpPost]
        public ActionResult Create(HomePage item)
        {
            IDbConnection db = new OrmliteConnection().openConn();
            try
            {
                if (string.IsNullOrEmpty(item.entryname) || string.IsNullOrEmpty(item.entrycode)) return Json(new { success = false, message = tw_Lang.Common_ActionResult_MissingInfo });
                var isExist = HomePage.GetById(item.entryid, null, false) ;

                //Validate

                //insert / update
                if (item.entryid == 0)
                {
                    //insert
                    item.createdat = DateTime.Now;
                    item.createdby = currentUser.entryid;
                    item.updatedat = DateTime.Now;
                    item.updatedby = currentUser.entryid;
                    item.isactive = true;

                }
                else
                {
                    //update
                    item.createdby = isExist.createdby;
                    item.updatedat = DateTime.Now;
                    item.updatedby = currentUser.entryid;

                }
                item.AddOrUpdate(currentUser.entryid, null, false);
                return Json(new { success = true, data = item });

            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
            finally { db.Close(); }

        }
      
        [HttpPost]
        public ActionResult GetByID(int entryid)
        {
            IDbConnection dbConn = new OrmliteConnection().openConn();
            try
            {
                var data =HomePage.GetById(entryid);
                return Json(new
                {
                    success = true,
                    data = data
                   
                });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
            finally { dbConn.Close(); }
        }
        #endregion
       
    }
}