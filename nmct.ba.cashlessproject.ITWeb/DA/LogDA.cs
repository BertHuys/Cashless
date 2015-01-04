using nmct.ba.cashlessproject.ITWeb.ViewModels;
using nmct.ba.cashlessproject.models.Web;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.ITWeb.DA
{
    public class LogDA
    {
        public const string CONNECTIONSTRING = "Connection";

        public static List<LogVM> GetLog()
        {
            List<LogVM> list = new List<LogVM>();
            string sql = "SELECT [Registers].[ID],[Registers].[RegisterName],[Registers].[Device],[Registers].[PurchaseDate],[Registers].[ExpiresDate],[Errorlog].[RegisterID], [Errorlog].[Timestamp], [Errorlog].[Message], [Errorlog].[Stacktrace]  FROM [CashlessPayment].[dbo].[Registers] INNER JOIN [CashlessPayment].[dbo].[Errorlog] ON [Registers].[ID] = [Errorlog].[RegisterID]";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                Register reg = new Register()
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    RegisterName = reader["RegisterName"].ToString(),
                    Device = reader["Device"].ToString(),
                    PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]),
                    ExpiresDate = Convert.ToDateTime(reader["ExpiresDate"])
                };
                Log log = new Log()
                {
                    RegisterID = Convert.ToInt32(reader["RegisterID"]),
                    Timestamp = Convert.ToDateTime(reader["Timestamp"]),
                    Message = reader["Message"].ToString(),
                    Stacktrace = reader["Stacktrace"].ToString()
                };

                LogVM logs = new LogVM();
                logs.register = reg;
                logs.log = log;

                list.Add(logs);
            }
            reader.Close();
            return list;
        }

        public static List<LogVM> GetLogById(int register)
        {
            List<LogVM> list = new List<LogVM>();
            string sql = "SELECT [Registers].[ID],[Registers].[RegisterName],[Registers].[Device],[Registers].[PurchaseDate],[Registers].[ExpiresDate],[Errorlog].[RegisterID], [Errorlog].[Timestamp], [Errorlog].[Message], [Errorlog].[Stacktrace]  FROM [CashlessPayment].[dbo].[Registers] INNER JOIN [CashlessPayment].[dbo].[Errorlog] ON [Registers].[ID] = [Errorlog].[RegisterID] WHERE [Registers].[ID] = @RegisterId";
            DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@RegisterId", register);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par);

            while (reader.Read())
            {
                Register reg = new Register()
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    RegisterName = reader["RegisterName"].ToString(),
                    Device = reader["Device"].ToString(),
                    PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]),
                    ExpiresDate = Convert.ToDateTime(reader["ExpiresDate"])
                };
                Log log = new Log()
                {
                    RegisterID = Convert.ToInt32(reader["RegisterID"]),
                    Timestamp = Convert.ToDateTime(reader["Timestamp"]),
                    Message = reader["Message"].ToString(),
                    Stacktrace = reader["Stacktrace"].ToString()
                };

                LogVM logs = new LogVM();
                logs.register = reg;
                logs.log = log;

                list.Add(logs);
            }
            reader.Close();
            return list;
        }

    }
}