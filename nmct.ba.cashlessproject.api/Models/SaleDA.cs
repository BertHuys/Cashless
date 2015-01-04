using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.models.ClientUser;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class SaleDA
    {
        private const string CONNECTIONSTRING = "ConnectionString";

        public static int CreateSale(Sale s)
        {
            string sql = "INSERT INTO Sales VALUES (@TimeStamp, @CustomerID, @RegisterID, @ProductID, @Amount, @TotalPrice)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@TimeStamp", s.Timestamp);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@CustomerID", s.CustomerID);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID",s.RegisterID);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@ProductID", s.ProductID);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@Amount", s.Amount);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@TotalPrice", s.TotalPrice);
            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6);
        }
    }
}