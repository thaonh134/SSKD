using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Linq;
using SSKD.Service;
using ServiceStack.OrmLite;
using SSKD.Areas.Admin.Models;
using AutoMapper;
using System.Data.SqlClient;
using System.Dynamic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace SSKD.Models
{
    public class DefaultView
    {
        #region Capcha Session
        public static string RandomCapcha()
        {
            HttpContext.Current.Session["CaptchaText"] = new Random().Next(1000, 9999);
            return HttpContext.Current.Session["CaptchaText"].ToString();
        }
        public static string GetRandomCapcha()
        {
            if (HttpContext.Current.Session["CaptchaText"] == null)
                HttpContext.Current.Session["CaptchaText"] = new Random().Next(1000, 9999);
            return HttpContext.Current.Session["CaptchaText"].ToString();
        }
        private static Random Randomizer = new Random(DateTime.Now.Second);
        public static string CreateImgCaptcha(string text)
        {
            byte[] byteArray = null;

            Font[] fonts = {
            new Font("Arial", 24, FontStyle.Bold),
            new Font("Courier New", 22, FontStyle.Bold),
            new Font("Calibri", 20, FontStyle.Bold),
            new Font("Tahoma", 24, FontStyle.Italic | FontStyle.Bold) };

            using (var bmp = new Bitmap(170, 50))
            {
                using (var graphic = Graphics.FromImage(bmp))
                {
                    using (var hb = new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.Silver, Color.White)) graphic.FillRectangle(hb, 0, 0, bmp.Width, bmp.Height);
                    for (int i = 0; i < text.Length; i++)
                    {
                        var point = new PointF(10 + (i * 35), 25);
                        graphic.DrawString(text.Substring(i, 1), fonts[Randomizer.Next(0, 4)], Brushes.Gray, point, new StringFormat { LineAlignment = StringAlignment.Center });
                    }
                }
                using (var stream = new MemoryStream())
                {
                    bmp.Save(stream, ImageFormat.Png);
                    byteArray = stream.ToArray();
                }
            }

            // Cleanup Fonts (they are disposable)
            foreach (var font in fonts) font.Dispose();

            string imageBase64Data = Convert.ToBase64String(byteArray);
            return string.Format("data:image/png;base64,{0}", imageBase64Data);
            //return byteArray;

        }
        #endregion
        #region getMenu Data 

        #endregion


        #region Menu front end

        public class FE_MenuSubItem
        {
            public string menuid { get; set; }
            public string submenuid { get; set; }
            public string submenuname { get; set; }

            public string controller { get; set; }
            public string action { get; set; }
            public string parameter { get; set; }

            public static FE_MenuSubItem GenMenu(int menuid, int submenuid, string submenuname, string controller, string action, string parameter)
            {
                var menu = new FE_MenuSubItem();
                menu = new FE_MenuSubItem()
                {
                    menuid = menuid.ToString(),
                    submenuid = submenuid.ToString(),
                    submenuname = submenuname,
                    controller = controller,
                    action = action,
                    parameter = parameter

                };


                return menu;
            }
        }

        public class FE_MenuItem
        {
            public string menuid { get; set; }
            public string menuName { get; set; }
            public string controller { get; set; }
            public string action { get; set; }
            public string parameter { get; set; }
            public List<FE_MenuSubItem> menusubitems { get; set; }


            public static FE_MenuItem GenMenu(int menuid,string menuname,string controller, string action, string parameter)
            {
                var menu = new FE_MenuItem();
                menu = new FE_MenuItem()
                {
                    menuid = menuid.ToString(),
                    menuName = menuname,
                    controller = controller,
                    action = action,
                    parameter = parameter,
                    menusubitems = new List<FE_MenuSubItem>()

                };
               
                
                return menu;
            }
            public static List<FE_MenuItem> GetHomeItem()
            {
                try
                {
                    var result = new List<FE_MenuItem>();
                    for(var i = 1; i <= 5; i++)
                    {
                        var menu =  new FE_MenuItem();
                        var controller = "";
                        if (i == 1) {
                            controller = "Company";
                            menu = FE_MenuItem.GenMenu(i, "Công ty", controller, "Index", "");
                            //menu.menusubitems.Add(FE_MenuSubItem.GenMenu(i,1, "Company Info", controller, "Info", ""));
                            //menu.menusubitems.Add(FE_MenuSubItem.GenMenu(i, 2, "Location", "Home", "Location", ""));
                        }
                        else if (i == 2)
                        {
                            controller = "Product";
                            menu = FE_MenuItem.GenMenu(i, "Sản phẩm", "Product", "Index", "");
                            int subid = 0;
                            foreach (var cate in FE_Category.GetTop(5))
                            {
                                subid++;
                                menu.menusubitems.Add(FE_MenuSubItem.GenMenu(i, subid, cate.CategoryName, controller, "Index", cate.CategoryCode));

                            }

                        }
                        else if (i == 3)
                        {

                            controller = "GALLERY";
                            menu = FE_MenuItem.GenMenu(i, "Hệ Thống Phân Phối", controller, "Index", "");
                            menu.menusubitems.Add(FE_MenuSubItem.GenMenu(i, 1, "Phổ biến", controller, "Index", ""));
                        }
                        else if (i == 4)
                        {

                            controller = "ORDER";
                            menu = FE_MenuItem.GenMenu(i, "Đơn hàng", controller, "Index", "");
                            menu.menusubitems.Add(FE_MenuSubItem.GenMenu(i, 1, "Đơn hàng Online ", controller, "Index", ""));
                        }
                        else if (i == 5)
                        {

                            controller = "COMMUNITY";
                            menu = FE_MenuItem.GenMenu(i, "Liên hệ", controller, "Faq", "");
                            menu.menusubitems.Add(FE_MenuSubItem.GenMenu(i, 1, "FAQ", controller, "Faq", ""));
                            menu.menusubitems.Add(FE_MenuSubItem.GenMenu(i, 2, "Thông báo", controller, "Notice", ""));
                            menu.menusubitems.Add(FE_MenuSubItem.GenMenu(i, 3, "Qna", controller, "Qna", ""));
                        }
                      result.Add(menu);

                    }

                    return result;
                }
                catch (Exception ex)
                {

                    return new List<FE_MenuItem>();
                }
            }
        }


        #endregion

        public static List<DDLModel> ListOrderProduct()
        {
            var lstResult = new List<DDLModel>();
            lstResult.Add(new DDLModel() { ID = "CreatedAt", Name = "Thứ tự theo sản phẩm mới" });
            lstResult.Add(new DDLModel() { ID = "Price", Name = "Thứ tự theo giá: thấp đến cao" });
            lstResult.Add(new DDLModel() { ID = "Price desc", Name = "Thứ tự theo giá: cao xuống thấp" });
            lstResult.Add(new DDLModel() { ID = "Viewcount", Name = "Thứ tự theo mức độ phổ biến" });
            lstResult.Add(new DDLModel() { ID = "Sellcount", Name = "Thứ tự theo điểm đánh giá" });
            return lstResult;
        }

        #region get list fe data
        public class FE_Product
        {

            public int ProductId { get; set; }
            public int CategoryId { get; set; }
            public int SubCategoryId { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public string SubCategoryName { get; set; }
            public string ProductImg { get; set; }
            public string ProductImgMore { get; set; }

            public double Discount { get; set; }
            public double PriceAmount { get; set; }
            public double Price { get; set; }



            public string Brand { get; set; }
            public string Origin { get; set; }
            public string Unit { get; set; }
            public double Unitquantity { get; set; }
            public int Viewcount { get; set; }
            public int Sellcount { get; set; }
            public string Contents { get; set; }
            public string Comments { get; set; }
            public static List<FE_Product> GetByGroupCategory(int itemsPerGroup)
            {
                try
                {
                    IDbConnection dbConn = new OrmliteConnection().openConn();
                    var lst_item = dbConn.Select<Product>("isactive={0} ", 1).GroupBy(u => u.categoryid).Select(grp => grp.ToList()).ToList();
                    //var xx=lst_item.Zip(Enumerable.Range(0, lst_item.Count()),(s, r) => new { Group = r / itemsPerGroup, Item = s }).GroupBy(i => i.Group, g => g.Item).ToList(); 
                    //var lst_gettop = lst_item.Select((x, idx) => new { x, idx }).GroupBy(x => x.idx / itemsPerGroup).Select(g => g.Select(a => a.x).ToList()).ToList();
                    var lst_result = new List<Product>();
                    foreach (var item in lst_item)
                    {
                        lst_result.AddRange(item.Take(8));
                    }
                    var mapped = Mapper.Map<List<FE_Product>>(lst_result);
                    var result = mapped;
                    return result;

                }
                catch (Exception ex)
                {
                    return new List<FE_Product>();
                }
            }
            public static List<FE_Product> GetByCategory(string categoryid)
            {
                try
                {
                    IDbConnection dbConn = new OrmliteConnection().openConn();
                    var lst_item = dbConn.Select<Product>("isactive={0} and categoryid = {1}", 1, categoryid);
                    var mapped = Mapper.Map<List<FE_Product>>(lst_item);
                    var result = mapped;
                    return result;

                }
                catch (Exception ex)
                {
                    return new List<FE_Product>();
                }
            }
            public static SearchResult SearchByCategory(SearchRequest request, string categoryid)
            {
                var result = new SearchResult();
                try
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@page", request.pagenum));
                    param.Add(new SqlParameter("@pagesize", request.pagesize));
                    param.Add(new SqlParameter("@categoryid", categoryid));
                    param.Add(new SqlParameter("@wherecondition", ""));
                    param.Add(new SqlParameter("@curruserid", 0));
                    param.Add(new SqlParameter("@sort", GetSortString(request)));

                    var data = new SqlHelper().ExecuteQuery("p_Product_FESearchByCategory", param);
                    result.pagenum = request.pagenum;
                    result.dataresult = Mapper.Map<List<FE_Product>>(CustomModel.ConvertDataTable<Product>(data));
                    result.total = data.Rows.Count > 0 ? Convert.ToInt32(data.Rows[0]["rowcount"]) : 0;
                    return result;

                }
                catch (Exception ex)
                {
                    result.pagenum = request.pagenum;
                    result.dataresult = null;
                    result.total = 0;
                    return result;
                }
            }
            public static SearchResult SearchBySubCategory(SearchRequest request, string subcategoryid)
            {
                var result = new SearchResult();
                try
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@page", request.pagenum));
                    param.Add(new SqlParameter("@pagesize", request.pagesize));
                    param.Add(new SqlParameter("@subcategoryid", subcategoryid));
                    param.Add(new SqlParameter("@wherecondition", ""));
                    param.Add(new SqlParameter("@curruserid", 0));
                    param.Add(new SqlParameter("@sort", GetSortString(request)));

                    var data = new SqlHelper().ExecuteQuery("p_Product_FESearchBySubcategory", param);
                    result.pagenum = request.pagenum;
                    result.dataresult = Mapper.Map<List<FE_Product>>(CustomModel.ConvertDataTable<Product>(data));
                    result.total = data.Rows.Count > 0 ? Convert.ToInt32(data.Rows[0]["rowcount"]) : 0;
                    return result;

                }
                catch (Exception ex)
                {
                    result.pagenum = request.pagenum;
                    result.dataresult = null;
                    result.total = 0;
                    return result;
                }
            }
            public static SearchResult SearchByKeyWord(SearchRequest request, string keyword)
            {
                var result = new SearchResult();
                try
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@page", request.pagenum));
                    param.Add(new SqlParameter("@pagesize", request.pagesize));
                    param.Add(new SqlParameter("@wherecondition", keyword));
                    param.Add(new SqlParameter("@curruserid", 0));
                    param.Add(new SqlParameter("@sort", GetSortString(request)));

                    var data = new SqlHelper().ExecuteQuery("p_Product_FESearchByKeyWord", param);
                    result.pagenum = request.pagenum;
                    result.dataresult = Mapper.Map<List<FE_Product>>(CustomModel.ConvertDataTable<Product>(data));
                    result.total = data.Rows.Count > 0 ? Convert.ToInt32(data.Rows[0]["rowcount"]) : 0;
                    return result;

                }
                catch (Exception ex)
                {
                    result.pagenum = request.pagenum;
                    result.dataresult = null;
                    result.total = 0;
                    return result;
                }
            }

            public static string GetSortString(SearchRequest request)
            {
                var sortby = string.IsNullOrEmpty(request.sorttype) ? "" : (request.sorttype == "-1" ? "ASC" : "DESC");
                var orderby = request.orderby;
                return " ORDER BY " + orderby + " " + sortby;
            }
            public static List<FE_Product> GetBySubCategory(string subcategoryid)
            {
                try
                {
                    IDbConnection dbConn = new OrmliteConnection().openConn();
                    var lst_item = dbConn.Select<Product>("isactive={0} and subcategoryid = {1}", 1, subcategoryid);
                    var mapped = Mapper.Map<List<FE_Product>>(lst_item);
                    var result = mapped;
                    return result;

                }
                catch (Exception ex)
                {
                    return new List<FE_Product>();
                }
            }

            public static FE_Product GetDetail(string productid)
            {
                try
                {
                    IDbConnection dbConn = new OrmliteConnection().openConn();
                    var item = dbConn.FirstOrDefault<Product>("isactive={0} and entryid = {1}", 1, productid);
                    var mapped = Mapper.Map<FE_Product>(item);
                    var result = mapped;
                    return result;

                }
                catch (Exception ex)
                {
                    return new FE_Product();
                }
            }

            public static List<FE_Product> GetBestSell()
            {
                try
                {
                    IDbConnection dbConn = new OrmliteConnection().openConn();
                    var lst_item = dbConn.Select<Product>("isactive={0}", 1).OrderByDescending(x => x.sellcount).Take(6).ToList();
                    var mapped = Mapper.Map<List<FE_Product>>(lst_item);
                    var result = mapped;
                    return result;

                }
                catch (Exception ex)
                {
                    return new List<FE_Product>();
                }
            }
        }

        public class FE_Category
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public string CategoryCode { get; set; }
            public static List<FE_Category> GetTop(int num)
            {
                try
                {
                    IDbConnection dbConn = new OrmliteConnection().openConn();
                    var item = dbConn.Where<Category>(x=>x.isactive==true);
                    if (item == null ) return new List<FE_Category>();
                    item= item.Take(num).ToList();
                    return Mapper.Map<List<FE_Category>>(item);

                }
                catch (Exception ex)
                {
                    return new List<FE_Category>();
                }
            }
            public static FE_Category GetDetail(string categoryid)
            {
                try
                {
                    IDbConnection dbConn = new OrmliteConnection().openConn();
                    var item = dbConn.FirstOrDefault<Category>("isactive={0} and entryid = {1}", 1, categoryid);
                    if (item == null) return new FE_Category();
                    return Mapper.Map<FE_Category>(item);

                }
                catch (Exception ex)
                {
                    return new FE_Category();
                }
            }
            public static FE_Category GetDetailByCode(string categorycode)
            {
                try
                {
                    IDbConnection dbConn = new OrmliteConnection().openConn();
                    var item = dbConn.FirstOrDefault<Category>("isactive={0} and entrycode = {1}", 1, categorycode);
                    if (item == null) return new FE_Category();
                    return Mapper.Map<FE_Category>(item);

                }
                catch (Exception ex)
                {
                    return new FE_Category();
                }
            }
        }

        public class FE_ContactRequest
        {
            public int ContactRequestId { get; set; }
            public int UserId { get; set; }
            public string FullName { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Comments { get; set; }
            public string CaptchaCode { get; set; }

            public static FE_ContactRequest GetDefaultContactRequest(int UserId)
            {
                try
                {
                    IDbConnection dbConn = new OrmliteConnection().openConn();
                    var item = dbConn.SingleOrDefault<ContactRequest>("isactive={0} and userid = {1} and userid <> 0", 1, UserId);
                    if (item == null) return new FE_ContactRequest();
                    return Mapper.Map<FE_ContactRequest>(item);

                }
                catch (Exception ex)
                {
                    return new FE_ContactRequest();
                }
            }


            public static int SaveContactRequest(int UserId, FE_ContactRequest contactrequest)
            {
               var entryItem = Mapper.Map<ContactRequest>(contactrequest);
                var dbConn = new OrmliteConnection().openConn();
                var entryid = entryItem.AddOrUpdate(UserId, dbConn, false);
                return entryid;

            }
        }
        #endregion

    }

}