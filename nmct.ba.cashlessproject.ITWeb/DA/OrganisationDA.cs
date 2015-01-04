using nmct.ba.cashlessproject.models.Web;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace nmct.ba.cashlessproject.ITWeb.DA
{
    public class OrganisationDA
    {
    public const string CONNECTIONSTRING = "Connection";

        public static List<Organisation> GetOrganisations()
        {
            List<Organisation> list = new List<Organisation>();
            string sql = "SELECT * FROM Organisations";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                Organisation org = new Organisation()
                {
                    ID = Convert.ToInt32(reader["ID"]),
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
                list.Add(org);
            }
            reader.Close();
            return list;
        }

        public static Organisation GetOrganisationById(int id)
        {
            string sql = "SELECT * FROM Organisations WHERE ID = @ID";
            DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par);
            Organisation org = new Organisation();

            while (reader.Read())
            {
                org.ID = Convert.ToInt32(reader["ID"]);
                org.Login = reader["Login"].ToString();
                org.Password = reader["Password"].ToString();
                org.DbName = reader["DbName"].ToString();
                org.DbLogin = reader["DbLogin"].ToString();
                org.DbPassword = reader["DbPassword"].ToString();
                org.OrganisationName = reader["OrganisationName"].ToString();
                org.Address = reader["Address"].ToString();
                org.Email = reader["Email"].ToString();
                org.Phone = reader["Phone"].ToString();
            }
            reader.Close();
            return org;
        }

        public static int NewOrganisation(Organisation organisation)
        {
            string sql = "INSERT INTO Organisations VALUES(@Login, @Password, @DbName, @DbLogin, @DBPassword, @OrganisationName, @Address, @Email, @Phone)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", organisation.Login);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", organisation.Password);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", organisation.DbName);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", organisation.DbLogin);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@DbPassword", organisation.DbPassword);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", organisation.OrganisationName);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Address", organisation.Address);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Email", organisation.Email);
            DbParameter par9 = Database.AddParameter(CONNECTIONSTRING, "@Phone", organisation.Phone);
            int id = Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);
            CreateDatabase(organisation);
            return id;
        }

        public static int UpdateOrganisation(Organisation organisation)
        {
            string sql = "UPDATE Organisations SET Login = @Login, Password = @Password, DbName = @DbName, DbLogin = @DbLogin, DbPassword = @DbPassword, OrganisationName = @OrganisationName, Address = @Address, Email = @Email, Phone = @Phone WHERE ID = @ID;";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", organisation.Login);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", organisation.Password);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", organisation.DbName);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", organisation.DbLogin);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@DbPassword", organisation.DbPassword);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", organisation.OrganisationName);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Address", organisation.Address);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Email", organisation.Email);
            DbParameter par9 = Database.AddParameter(CONNECTIONSTRING, "@Phone", organisation.Phone);
            DbParameter par10 = Database.AddParameter(CONNECTIONSTRING, "@ID", organisation.ID);
            return Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6, par7, par8, par9, par10);
        }


        private static void CreateDatabase(Organisation o)
        {
            string create = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/create.txt"));
            string sql = create.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);
            foreach (string commandText in RemoveGo(sql))
            {
                Database.ModifyData(CONNECTIONSTRING, commandText);
            }

            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction(CONNECTIONSTRING);

                string fill = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/fill.txt"));
                string sql2 = fill.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);

                foreach (string commandText in RemoveGo(sql2))
                {
                    Database.ModifyData(trans, commandText);
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Console.WriteLine(ex.Message);
            }
        }

        private static string[] RemoveGo(string input)
        {
            string[] splitter = new string[] { "\r\nGO\r\n" };
            string[] commandTexts = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            return commandTexts;
        }


    }
}