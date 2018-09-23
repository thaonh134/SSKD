using SSKD.Models;
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
            var category = DefaultView.FE_Category.GetTop(1);
            if (string.IsNullOrEmpty(Type)) Type = category[0].CategoryCode;
            dict["data_Type"] =Type.ToLower();
            return View(dict);
        }
        public ActionResult Detail(string Id)
        {
            var dict = new Dictionary<string, object>();
            dict["data_product"] = DefaultView.FE_Product.GetDetail(Id);
            return View(dict);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Read(SearchRequest request, string id)
        {
            var category= DefaultView.FE_Category.GetDetailByCode(id.ToUpper());
            var data = DefaultView.FE_Product.SearchByCategory(request, category.CategoryId.ToString());
            return Json(data);
        }
    }
}