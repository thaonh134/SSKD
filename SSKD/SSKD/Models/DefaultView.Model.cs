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
        #region getList Data 

        #endregion


        #region Menu front end
        public class FE_MenuSubItem
        {
            public string menuid { get; set; }
            public string submenuid { get; set; }
            public string submenuname { get; set; }
        }

        public class FE_MenuItem
        {
            public string menuid { get; set; }
            public string menuName { get; set; }
            public List<FE_MenuSubItem> menusubitems { get; set; }



            public static List<FE_MenuItem> GetHomeItem()
            {
                try
                {
                    var result = new List<FE_MenuItem>() {
                        new FE_MenuItem() {
                        menuid ="1",menuName="Company",menusubitems=new List<FE_MenuSubItem>() {
                            new FE_MenuSubItem() { submenuname="Company Info"}
                        , new FE_MenuSubItem() { submenuname = "Location" }
                        } }
                    ,new FE_MenuItem() {
                        menuid ="2",menuName="Product",menusubitems=new List<FE_MenuSubItem>() {
                            new FE_MenuSubItem() { submenuname="DESK PAD"}
                        , new FE_MenuSubItem() { submenuname = "DESK GOODS" }
                        , new FE_MenuSubItem() { submenuname = "MOUSE PAD" }
                        , new FE_MenuSubItem() { submenuname = "OFFICE SUPPLIES" }
                        , new FE_MenuSubItem() { submenuname = "ORDER PRODUCTS" }
                        } }
                    ,new FE_MenuItem() {
                        menuid ="3",menuName="GALLERY",menusubitems=new List<FE_MenuSubItem>() {
                            new FE_MenuSubItem() { submenuname="EXAMPLE OF USAGE"}
                        } }
                    ,new FE_MenuItem() {
                        menuid ="4",menuName="ORDER",menusubitems=new List<FE_MenuSubItem>() {
                            new FE_MenuSubItem() { submenuname="ONLINE ORDER"}
                        } }
                    ,new FE_MenuItem() {
                        menuid ="5",menuName="COMMUNITY",menusubitems=new List<FE_MenuSubItem>() {
                            new FE_MenuSubItem() { submenuname="NOTICE"}
                            ,            new FE_MenuSubItem() { submenuname="FAQ"}
                            ,            new FE_MenuSubItem() { submenuname="QNA"}
                        } }
                    };


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

    }

}