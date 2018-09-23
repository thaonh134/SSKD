using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSKD.Areas.Admin.Models;
using AutoMapper;
using static SSKD.Models.DefaultView;

namespace SSKD.AutoMap
{
   
    public class Fe_Mapper : Profile
    {
        public Fe_Mapper()
        {
            CreateMap<Product, FE_Product>()
               .ForMember(dst => dst.ProductId, x => x.MapFrom(src => src.entryid))
               .ForMember(dst => dst.CategoryId, x => x.MapFrom(src => src.categoryid))
               .ForMember(dst => dst.SubCategoryId, x => x.MapFrom(src => src.subcategoryid))
               .ForMember(dst => dst.ProductName, x => x.MapFrom(src => src.entryname))
               .ForMember(dst => dst.ProductImg, x => x.MapFrom(src => src.defautlimg))
               .ForMember(dst => dst.ProductImgMore, x => x.MapFrom(src => src.listimgs))
               
               .ForMember(dst => dst.Discount, x => x.MapFrom(src => src.discount != null ? src.discount * 100 : 0))
               .ForMember(dst => dst.Price, x => x.MapFrom(src => src.price))
               .ForMember(dst => dst.PriceAmount, x => x.MapFrom(src => src.priceamount))

               .ForMember(dst => dst.Brand, x => x.MapFrom(src => src.brand))
               .ForMember(dst => dst.Origin, x => x.MapFrom(src => src.origin))
               .ForMember(dst => dst.Unit, x => x.MapFrom(src => src.unit))
               .ForMember(dst => dst.Unitquantity, x => x.MapFrom(src => src.unitquantity))
               .ForMember(dst => dst.Viewcount, x => x.MapFrom(src => src.viewcount))
               .ForMember(dst => dst.Sellcount, x => x.MapFrom(src => src.sellcount))
               .ForMember(dst => dst.Contents, x => x.MapFrom(src => src.contents))
               .ForMember(dst => dst.Comments, x => x.MapFrom(src => src.comments))
               ;

            CreateMap<Category, FE_Category>()
               .ForMember(dst => dst.CategoryId, x => x.MapFrom(src => src.entryid))
               .ForMember(dst => dst.CategoryName, x => x.MapFrom(src => src.entryname))
               .ForMember(dst => dst.CategoryCode, x => x.MapFrom(src => src.entrycode))
               ;
            //mapp ddl
            CreateMap<Category, DDLModel>()
             .ForMember(dst => dst.ID, x => x.MapFrom(src => src.entryid))
             .ForMember(dst => dst.Name, x => x.MapFrom(src => src.entryname))
             ;
            CreateMap<SubCategory, DDLModel>()
             .ForMember(dst => dst.ID, x => x.MapFrom(src => src.entryid))
             .ForMember(dst => dst.Name, x => x.MapFrom(src => src.entryname))
             ;

            CreateMap<ContactRequest, FE_ContactRequest>()
             .ForMember(dst => dst.ContactRequestId, x => x.MapFrom(src => src.entryid))
             .ForMember(dst => dst.UserId, x => x.MapFrom(src => src.userid))
             .ForMember(dst => dst.FullName, x => x.MapFrom(src => src.fullname))
             .ForMember(dst => dst.Address, x => x.MapFrom(src => src.address))
             .ForMember(dst => dst.Phone, x => x.MapFrom(src => src.phone))
             .ForMember(dst => dst.Email, x => x.MapFrom(src => src.email))
             .ForMember(dst => dst.Comments, x => x.MapFrom(src => src.comments))
             ;

            CreateMap<FE_ContactRequest, ContactRequest>()
              .ForMember(dst => dst.entryid, x => x.MapFrom(src => src.ContactRequestId))
              .ForMember(dst => dst.entrycode, x => x.UseValue(""))
              .ForMember(dst => dst.entryname, x => x.UseValue(""))
              .ForMember(dst => dst.userid, x => x.MapFrom(src => src.UserId))
              .ForMember(dst => dst.fullname, x => x.MapFrom(src => src.FullName))
              .ForMember(dst => dst.address, x => x.MapFrom(src => src.Address))
              .ForMember(dst => dst.phone, x => x.MapFrom(src => src.Phone))
              .ForMember(dst => dst.email, x => x.MapFrom(src => src.Email))
              .ForMember(dst => dst.comments, x => x.MapFrom(src => src.Comments))
              .ForMember(dst => dst.isactive, x => x.UseValue(true))
              .ForMember(dst => dst.createdat, x => x.UseValue(DateTime.UtcNow))
              .ForMember(dst => dst.createdby, x => x.MapFrom(src => src.UserId))
              .ForMember(dst => dst.updatedat, x => x.UseValue(DateTime.UtcNow))
              .ForMember(dst => dst.updatedby, x => x.MapFrom(src => src.UserId))
              ;
        }
        
    }
}