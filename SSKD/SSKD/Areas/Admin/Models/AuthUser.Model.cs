using SSKD.Areas.Admin.Models.AutoGen;
using SSKD.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using System.Linq;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ServiceStack.DataAnnotations;
namespace SSKD.Areas.Admin.Models
{
    public class AuthUser: AuthUserBase<AuthUser>
    {
      
[Ignore]
public string birthday_str  { get; set; } 
[Ignore]
public string lastlogin_str  { get; set; } 	
		#region AutoGen
public int AddOrUpdate(int curruserid, IDbConnection dbConn, bool isTrans)
{
    if (dbConn == null) dbConn = new OrmliteConnection().openConn();
    try
    {
        var isexist = dbConn.GetByIdOrDefault <AuthUser> (this.entryid);
        if (isexist == null)
        {

            this.isactive = true;
            this.createdat = DateTime.Now;
            this.createdby = curruserid;
            this.updatedat = DateTime.Now;
            this.updatedby = curruserid;
            dbConn.Insert<AuthUser>(this);
            long lastInsertId = dbConn.GetLastInsertId();
            if (!isTrans) dbConn.Close();
            this.entryid = Convert.ToInt32(lastInsertId);
            return this.entryid;
        }
        else if (isexist != null)
        {
            this.isactive = isexist.isactive;
            this.createdat = isexist.createdat;
            this.createdby = isexist.createdby;
            this.updatedat = DateTime.Now;
            this.updatedby = curruserid;
            dbConn.Update<AuthUser>(this);
            if (!isTrans) dbConn.Close();
            return this.entryid;
        }
        else
        {
            if (!isTrans) dbConn.Close();
            return 0;
        }
          
    }
    catch (Exception ex)
    {
        if (!isTrans)
        {
            dbConn.Close();
            return 0;
        }

        throw new System.ArgumentException("data error", ex);
    }
}
        #endregion
        #region MyCode
        public static AuthUser GetByCode(string entrycode, string typelogin)
        {
            IDbConnection dbConn = new OrmliteConnection().openConn();
            try
            {
                var data = dbConn.FirstOrDefault<AuthUser>("entrycode={0} and loginprovider = {1}", entrycode, typelogin);
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
            finally { dbConn.Close(); }
        }
        #endregion
    }
}