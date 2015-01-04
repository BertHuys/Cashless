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
    public class CustomerDA
    {
        //public static List<Customer> GetCustomers()
        //{
        //    List<Customer> list = new List<Customer>();

        //    string sql = "SELECT ID, CustomerName, Address, Picture, Balance FROM Customers";
        //    DbDataReader reader = Database.GetData(Database.GetConnection("CashlessConnectionKlant"), sql);

        //    while (reader.Read())
        //    {
        //        list.Add(Create(reader));
        //    }
        //    reader.Close();
        //    return list;
        //}

        //private static Customer Create(IDataRecord record)
        //{
        //    return new Customer()
        //    {
        //        ID = Int32.Parse(record["ID"].ToString()),
        //        CustomerName = record["CustomerName"].ToString(),
        //        Address = record["Address"].ToString(),
        //        Picture=record["Picture"].ToString(),
        //        Balance = Double.Parse(record["Balance"].ToString())
        //    };
        //}
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"PCBERT\DATAMANAGEMENT", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Customer> GetCustomers(IEnumerable<Claim> claims)
        {


            List<Customer> list = new List<Customer>();
            string sql = "SELECT * FROM Customers";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Customer c = new Customer();
                c.ID = Convert.ToInt32(reader["ID"]);
                c.CustomerName = reader["CustomerName"].ToString();
                c.Address = reader["Address"].ToString();
                if (!DBNull.Value.Equals(reader["Picture"]))
                    c.Picture = (byte[])reader["Picture"];
                else
                    c.Picture = new byte[0];
                c.Balance = Convert.ToInt32(reader["Balance"]);

                list.Add(c);
            }
            reader.Close();
            return list;
        }
        public static Customer GetCustomerById(int id, IEnumerable<Claim> claims)
        {
            string sql = "SELECT * FROM Customers WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@ID", id);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);

            Customer customer = new Customer();
            while (reader.Read())
            {
                customer.ID = Convert.ToInt32(reader["ID"]);
                customer.CustomerName = reader["CustomerName"].ToString();
                customer.Address = reader["Address"].ToString();
                if (!DBNull.Value.Equals(reader["Picture"]))
                    customer.Picture = (byte[])reader["Picture"];
                else
                    customer.Picture = new byte[0];
                customer.Balance = Convert.ToInt32(reader["Balance"]);
            }

            reader.Close();
            return customer;
        }
        public static int GetBalanceByCustomer(string customername, string address, IEnumerable<Claim> claims)
        {
            string sql = "SELECT Balance FROM Customers WHERE CustomerName = @CustomerName AND Address = @Address";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@CustomerName", customername);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@Address", address);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2);

            int balance = 0;
            while (reader.Read())
            {
                balance = Convert.ToInt32(reader["Balance"]);
            }

            return balance;
        }
        public static int CustomerExist(string customername, string address, IEnumerable<Claim> claims)
        {
            string sql = "SELECT ID, CustomerName , Address, Picture, Balance FROM Customers WHERE CustomerName = @CustomerName AND Address = @Address";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@CustomerName", customername);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@Address", address);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2);

            if (reader.Read())
            {
                reader.Close();
                return 1;
            }
            else
            {
                reader.Close();
                return -1;
            }
        }
        public static int InsertCustomer(Customer c, IEnumerable<Claim> claims)
        {
            int rows = 0;
            if (c.Picture != null)
            {
                string sql = "INSERT INTO Customers VALUES(@CustomerName,@Address,@Picture,@Balance)";
                DbParameter par1 = Database.AddParameter("CashlessAdmin", "@CustomerName", c.CustomerName);
                DbParameter par2 = Database.AddParameter("CashlessAdmin", "@Address", c.Address);
                DbParameter par3 = Database.AddParameter("CashlessAdmin", "@Picture", c.Picture);
                DbParameter par4 = Database.AddParameter("CashlessAdmin", "@Balance", c.Balance);
                rows = Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4);
            }
            else 
            {
                string sql = "INSERT INTO Customers (CustomerName, Address, Balance) VALUES (@CustomerName,@Address,@Picture,@Balance)";
                DbParameter par1 = Database.AddParameter("CashlessAdmin", "@CustomerName", c.CustomerName);
                DbParameter par2 = Database.AddParameter("CashlessAdmin", "@Address", c.Address);
                DbParameter par3 = Database.AddParameter("CashlessAdmin", "@Balance", c.Balance);
                rows = Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3);
            }
            return rows;
        }

        public static int UpdateCustomer(Customer c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Customers SET CustomerName = @CustomerName, Address = @Address, Picture = @Picture, Balance = @Balance WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@CustomerName", c.CustomerName);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@Address", c.Address);
            DbParameter par3 = Database.AddParameter(CreateConnectionString(claims), "@Picture", c.Picture);
            DbParameter par4 = Database.AddParameter(CreateConnectionString(claims), "@Balance", c.Balance);
            DbParameter par5 = Database.AddParameter(CreateConnectionString(claims), "@ID", c.ID);
            return Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);
        }

        public static int UpdateCustomer(int balance, string customername, string address, IEnumerable<Claim> claims)
        {
            int rows = 0;

            string sql = "UPDATE Customers SET Balance = @Balance WHERE CustomerName = @CustomerName AND Address = @Address";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@Balance", balance);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@CustomerName", customername);
            DbParameter par3 = Database.AddParameter(CreateConnectionString(claims), "@Address", address);
            rows = Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3);

            return rows;
        }

        public static int DeleteCustomer(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Customers WHERE ID = @ID";
            DbParameter par = Database.AddParameter(CreateConnectionString(claims), "@ID", id);
            return Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par);
        }
    }
}
