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

            //mapp ddl
            CreateMap<Category, DDLModel>()
             .ForMember(dst => dst.ID, x => x.MapFrom(src => src.entryid))
             .ForMember(dst => dst.Name, x => x.MapFrom(src => src.entryname))
             ;
            CreateMap<SubCategory, DDLModel>()
             .ForMember(dst => dst.ID, x => x.MapFrom(src => src.entryid))
             .ForMember(dst => dst.Name, x => x.MapFrom(src => src.entryname))
             ;
        }
        
    }
}