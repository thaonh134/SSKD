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
    public class AuthMenu: AuthMenuBase<AuthMenu>
    {
      	
		#region AutoGen
public int AddOrUpdate(int curruserid, IDbConnection dbConn, bool isTrans)
{
    if (dbConn == null) dbConn = new OrmliteConnection().openConn();
    try
    {
        var isexist = dbConn.GetByIdOrDefault <AuthMenu> (this.entryid);
        if (isexist == null)
        {

            this.isactive = true;
            this.createdat = DateTime.Now;
            this.createdby = curruserid;
            this.updatedat = DateTime.Now;
            this.updatedby = curruserid;
            dbConn.Insert<AuthMenu>(this);
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
            dbConn.Update<AuthMenu>(this);
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
        public List<AuthMenu> GetAllMenu(string lang)
        {
            IDbConnection dbConn = new OrmliteConnection().openConn();
            try
            {
                return dbConn.Select<AuthMenu>(x=>x.isactive==ActiveStatus.Active);
            }
            catch (Exception ex)
            {
                return new List<AuthMenu>();
            }

        }
        #endregion
    }
}