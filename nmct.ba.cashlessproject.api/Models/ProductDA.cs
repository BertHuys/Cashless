using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.helpr;
using nmct.ba.cashlessproject.models;
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
    public class ProductDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"PCBERT\DATAMANAGEMENT", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Product> GetProducts(IEnumerable<Claim> claims)
        {
            List<Product> list = new List<Product>();

            string sql = "SELECT * FROM Products";
            DbDataReader reader;

            if (claims == null)
            {
                reader = Database.GetData("ConnectionString", sql);
            }
            else
            {
                reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            }

            while (reader.Read())
            {
                Product product = new Product();
                product.ID = Convert.ToInt32(reader["ID"]);
                product.ProductName = reader["ProductName"].ToString();
                product.Price = Convert.ToDouble(reader["Price"]);
                list.Add(product);
            }

            reader.Close();
            return list;
        }

        public static Product GetProductById(int id, IEnumerable<Claim> claims)
        {
            string sql = "SELECT * FROM Products WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@ID", id);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);

            Product product = new Product();
            while (reader.Read())
            {
                product.ID = Convert.ToInt32(reader["ID"]);
                product.ProductName = reader["ProductName"].ToString();
                product.Price = Convert.ToDouble(reader["Price"]);
            }

            reader.Close();
            return product;
        }

        public static int CreateProduct(Product p, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Products VALUES (@ProductName, @Price)";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@ProductName", p.ProductName);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@Price", p.Price);
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2);
        }

        public static int UpdateProduct(Product p, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Products SET ProductName = @ProductName, Price = @Price WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@ProductName", p.ProductName);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@Price", p.Price);
            DbParameter par3 = Database.AddParameter(CreateConnectionString(claims), "@ID", p.ID);
            return Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3);
        }

        public static int DeleteProduct(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Products WHERE ID = @ID";
            DbParameter par = Database.AddParameter(CreateConnectionString(claims), "@ID", id);
            return Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par);
        }
    }
}
