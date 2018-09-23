using AutoMapper;
using SSKD.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSKD.AutoMap
{
    public class Be_Mapper : Profile
    {
        public Be_Mapper() {
            CreateMap<Category, DDLModel>()
                  .ForMember(dst => dst.ID, x => x.MapFrom(src => src.entryid))
                  .ForMember(dst => dst.Name, x => x.MapFrom(src => src.entryname))
                  ;

        }
    }
}