using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.models.ClientUser;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class ErrorLogDA
    {
        private const string CONNECTIONSTRING = "ConnectionString";

        public static int CreateErrorLog(ErrorLog e)
        {
            string sql = "INSERT INTO Errorlog VALUES(@RegisterID, @Timestamp, @Message, @Stacktrace)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", 1); //temp
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Timestamp", e.Timestamp);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Message", e.Message);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Stacktrace", e.Stacktrace);
            int rows = Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4);
            return rows;
        }
    }
}