using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.helpr;
using nmct.ba.cashlessproject.models.ClientUser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class RegisterDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"PCBERT\DATAMANAGEMENT", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<RegisterSummary> GetRegisterSummary(IEnumerable<Claim> claims)
        {
            List<RegisterSummary> summary = new List<RegisterSummary>();

            string sql = "SELECT [Registers].ID, [Registers].RegisterName, [Registers].Device, [Register_Employee].EmployeeID, [Register_Employee].RegisterID ,[Register_Employee].[FromTime], [Register_Employee].[UntilTime], [Employee].[ID],[Employee].[EmployeeName], [Employee].Phone, [Employee].Address, [Employee].Email FROM [Registers] LEFT JOIN [Register_Employee] ON [Registers].ID = [Register_Employee].RegisterID LEFT JOIN [Employee] ON [Register_Employee].EmployeeID = [Employee].ID";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);

            while (reader.Read())
            {
                RegisterSummary rs = new RegisterSummary();

                Register kassa = new Register();
                kassa.ID = Convert.ToInt32(reader["ID"]);
                kassa.RegisterName = reader["RegisterName"].ToString();
                kassa.Device = reader["Device"].ToString();

                RegisterEmployee registeremployee = new RegisterEmployee();
                registeremployee.RegisterID = Convert.ToInt32(reader["RegisterID"]);
                registeremployee.EmployeeID = Convert.ToInt32(reader["EmployeeID"]);
                registeremployee.From = Convert.ToDateTime(reader["FromTime"]);
                registeremployee.Until = Convert.ToDateTime(reader["UntilTime"]);

                Employee medewerker = new Employee();
                medewerker.ID = Convert.ToInt32(reader["ID"]);
                medewerker.EmployeeName = reader["EmployeeName"].ToString();
                medewerker.Email = reader["Email"].ToString();
                medewerker.Phone = reader["Phone"].ToString();
                medewerker.Address = reader["Address"].ToString();

                rs.register = kassa;
                rs.employee = medewerker;
                rs.registeremployee = registeremployee;
                summary.Add(rs);
            }

            reader.Close();
            return summary;
        }

        public static RegisterSummary GetRegisterSummaryById(int id, IEnumerable<Claim> claims)
        {
            string sql = "SELECT [Registers].ID, [Registers].RegisterName, [Registers].Device, [Register_Employee].[From], [Register_Employee].Untill, [Employee].[ID],[Employee].[EmployeeName] FROM [Registers] LEFT JOIN [Register_Employee] ON [Register].ID = [Register_Employee].RegisterID LEFT JOIN [Employee] ON [Register_Employee].EmployeeID = [Employee].ID WHERE Register.ID = @RegisterID";
            DbParameter par = Database.AddParameter(CreateConnectionString(claims), "@RegisterId", id);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par);

            RegisterSummary rs = new RegisterSummary();
            while (reader.Read())
            {

                Register kassa = new Register();
                kassa.ID = Convert.ToInt32(reader["ID"]);
                kassa.RegisterName = reader["RegisterName"].ToString();
                kassa.Device = reader["Device"].ToString();

                RegisterEmployee registeremployee = new RegisterEmployee();
                registeremployee.From = Convert.ToDateTime(reader["FromTime"]);
                registeremployee.Until = Convert.ToDateTime(reader["UntilTime"]);

                Employee medewerker = new Employee();
                medewerker.ID = Convert.ToInt32(reader["ID"]);
                medewerker.EmployeeName = reader["EmployeeName"].ToString();

                rs.register = kassa;
                rs.employee = medewerker;
                rs.registeremployee = registeremployee;
            }

            reader.Close();
            return rs;
        }


    }
}