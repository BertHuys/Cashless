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
    public class EmployeeDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"PCBERT\DATAMANAGEMENT", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Employee> GetEmployees(IEnumerable<Claim> claims)
        {
            List<Employee> list = new List<Employee>();

            string sql = "SELECT * FROM Employee";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);

            while (reader.Read())
            {
                Employee employee = new Employee();
                employee.ID = Convert.ToInt32(reader["ID"]);
                employee.Address = reader["Address"].ToString();
                employee.EmployeeName = reader["EmployeeName"].ToString();
                employee.Email = reader["Email"].ToString();
                employee.Phone = reader["Phone"].ToString();
                list.Add(employee);
            }

            reader.Close();
            return list;
        }

        public static Employee GetEmployeesById(int id, IEnumerable<Claim> claims)
        {
            string sql = "SELECT * FROM Employee WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@ID", id);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);

            Employee employee = new Employee();
            while (reader.Read())
            {
                employee.ID = Convert.ToInt32(reader["ID"]);
                employee.Address = reader["Address"].ToString();
                employee.EmployeeName = reader["EmployeeName"].ToString();
                employee.Email = reader["Email"].ToString();
                employee.Phone = reader["Phone"].ToString();
            }

            reader.Close();
            return employee;
        }

        public static int EmployeeExists(string employeename, string address, IEnumerable<Claim> claims)
        {
            string sql = "SELECT [ID] ,[EmployeeName] ,[Address],[Email],[Phone] FROM Employee WHERE EmployeeName = @EmployeeName AND Address = @Address";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@EmployeeName", employeename);
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

        public static int InsertEmployee(Employee e, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Employee VALUES (@EmployeeName, @Address, @Email, @Phone)";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@EmployeeName", e.EmployeeName);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@Address", e.Address);
            DbParameter par3 = Database.AddParameter(CreateConnectionString(claims), "@Email", e.Email);
            DbParameter par4 = Database.AddParameter(CreateConnectionString(claims), "@Phone", e.Phone);
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4);
        }

        public static int UpdateEmployee(Employee e, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Employee SET EmployeeName = @EmployeeName, Address = @Address, Email = @Email, Phone = @Phone WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@EmployeeName", e.EmployeeName);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@Address", e.Address);
            DbParameter par3 = Database.AddParameter(CreateConnectionString(claims), "@Email", e.Email);
            DbParameter par4 = Database.AddParameter(CreateConnectionString(claims), "@Phone", e.Phone);
            DbParameter par5 = Database.AddParameter(CreateConnectionString(claims), "@ID", e.ID);
            return Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);
        }

        public static int DeleteEmployee(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Employee WHERE ID = @ID";
            DbParameter par = Database.AddParameter(CreateConnectionString(claims), "@ID", id);
            return Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par);
        }
    }
}