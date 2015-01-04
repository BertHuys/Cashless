using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.helpr;
using nmct.ba.cashlessproject.models.Web;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class OrganisationDA
    {
            public static Organisation CheckCredentials(string username, string password)
            {
                string sql = "SELECT * FROM Organisations WHERE Login=@Login AND Password=@Password";
                DbParameter par1 = Database.AddParameter("CashlessPayment", "@Login", Cryptography.Encrypt(username));
                DbParameter par2 = Database.AddParameter("CashlessPayment", "@Password", Cryptography.Encrypt(password));
                try
                {
                    DbDataReader reader = Database.GetData(Database.GetConnection("CashlessAdmin"), sql, par1, par2);
                    reader.Read();
                    return new Organisation()
                    {
                        ID = Int32.Parse(reader["ID"].ToString()),
                        Login = reader["Login"].ToString(),
                        Password = reader["Password"].ToString(),
                        DbName = reader["DbName"].ToString(),
                        DbLogin = reader["DbLogin"].ToString(),
                        DbPassword = reader["DbPassword"].ToString(),
                        OrganisationName = reader["OrganisationName"].ToString(),
                        Address = reader["Address"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString()
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

            }

        }
    }
