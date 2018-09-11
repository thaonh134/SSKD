﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSKD.Areas.Admin.Models
{
    public static class MasterData
    {
        public static string GetcurrentTimeSystem(){
            var js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(DateTime.Now);
        }

    }
    public static class ActiveStatus
    {
        public const bool Active = true;
        public const bool InActive = false;
        public static List<DDLModel> GetActiveStatus()
        {

            return new List<DDLModel>() { new DDLModel() { ID="true",Name= "Active" }, new DDLModel() { ID = "false", Name = "InActive" } };
        }
    }

    public static class LoginType
    {
      
        public static List<DDLModel> GetLoginType()
        {

            return new List<DDLModel>() { new DDLModel() { ID = "1", Name = "System" }, new DDLModel() { ID = "2", Name = "Web" } };
        }
    }
}