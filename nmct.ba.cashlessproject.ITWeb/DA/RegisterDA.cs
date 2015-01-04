using nmct.ba.cashlessproject.ITWeb.ViewModels;
using nmct.ba.cashlessproject.models.Web;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.ITWeb.DA
{
    public class RegisterDA
    {
        public const string CONNECTIONSTRING = "Connection";

        public static List<Register> GetRegisters()
        {
            List<Register> list = new List<Register>();
            string sql = "SELECT DISTINCT [Registers].[ID], [RegisterName],[Device],[PurchaseDate],[ExpiresDate] FROM [CashlessPayment].[dbo].[Registers]";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                Register register = new Register()
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    RegisterName = reader["RegisterName"].ToString(),
                    Device = reader["Device"].ToString(),
                    PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]),
                    ExpiresDate = Convert.ToDateTime(reader["ExpiresDate"])
                };
                list.Add(register);
            }
            reader.Close();
            return list;
        }

        public static List<RegisterVM> GetAllRegisters()
        {
            List<RegisterVM> list = new List<RegisterVM>();
            string sql = "SELECT [Registers].[ID],[Registers].[RegisterName],[Registers].[Device],[Registers].[PurchaseDate],[Registers].[ExpiresDate],[Organisations].[ID],[Organisations].[Login],[Organisations].[Password],[Organisations].[DbName],[Organisations].[DbLogin],[Organisations].[DbPassword],[Organisations].[OrganisationName],[Organisations].[Address],[Organisations].[Email],[Organisations].[Phone] FROM [CashlessPayment].[dbo].[Registers] LEFT JOIN [CashlessPayment].[dbo].[Organisation_Register] ON [Registers].[ID] = [Organisation_Register].[RegisterID] LEFT JOIN [CashlessPayment].[dbo].[Organisations] ON [Organisation_Register].[OrganisationID] = [Organisations].[ID] ORDER BY OrganisationName";
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

                RegisterVM obj = new RegisterVM();
                obj.register = reg;
                obj.organisation = org;

                list.Add(obj);
            }
            reader.Close();
            return list;
        }

        public static List<RegisterVM> GetRegisterByOrganisation(int vereniging)
        {
            List<RegisterVM> list = new List<RegisterVM>();
            string sql = "SELECT [Registers].[ID],[Registers].[RegisterName],[Registers].[Device],[Registers].[PurchaseDate],[Registers].[ExpiresDate],[Organisations].[ID],[Organisations].[Login],[Organisations].[Password],[Organisations].[DbName],[Organisations].[DbLogin],[Organisations].[DbPassword],[Organisations].[OrganisationName],[Organisations].[Address],[Organisations].[Email],[Organisations].[Phone] FROM [admindb].[dbo].[Registers] LEFT JOIN [admindb].[dbo].Organisation_Register ON [Registers].[ID] = [Organisation_Register].[RegisterID] LEFT JOIN [admindb].[dbo].Organisations ON [Organisation_Register].[OrganisationID] = [Organisations].[ID] WHERE Organisations.Id = @OrganisationId ORDER BY OrganisationName";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationId", vereniging);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

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

                RegisterVM obj = new RegisterVM();
                obj.register = reg;
                obj.organisation = org;

                list.Add(obj);
            }
            reader.Close();
            return list;
        }

        public static int NewRegister(Register register)
        {
            string sql = "INSERT INTO [CashlessPayment].[dbo].Registers (RegisterName, Device, PurchaseDate, ExpiresDate) VALUES(@RegisterName, @Device, @PurchaseDate, @ExpiresDate)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterName", register.RegisterName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Device", register.Device);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@PurchaseDate", register.PurchaseDate);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@ExpiresDate", register.ExpiresDate);
            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4);
        }

        public static int AddRegisterOrganisation(int registerID, int organisationID)
        {
            string sql = "INSERT INTO [CashlessPayment].[dbo].[Organisation_Register] ([OrganisationID],[RegisterID],[FromDate],[UntillDate]) VALUES (@OrganisationID,@RegisterID,null,null)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationID", organisationID);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", registerID);
            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2);
        }

        public static List<RegisterVM> CheckRegister(int registerID, int verenigingId)
        {
            List<RegisterVM> list = new List<RegisterVM>();
            string sql = "SELECT [Registers].[ID],[Registers].[RegisterName],[Registers].[Device],[Registers].[PurchaseDate],[Registers].[ExpiresDate],[Organisations].[ID],[Organisations].[Login],[Organisations].[Password],[Organisations].[DbName],[Organisations].[DbLogin],[Organisations].[DbPassword],[Organisations].[OrganisationName],[Organisations].[Address],[Organisations].[Email],[Organisations].[Phone] FROM [CashlessPayment].[dbo].[Registers] LEFT JOIN [admindb].[dbo].Organisation_Register ON [Registers].[ID] = [Organisation_Register].[RegisterID] LEFT JOIN [CashlessPayment].[dbo].Organisations ON [Organisation_Register].[OrganisationID] = [Organisations].[ID] WHERE OrganisationID IS NOT NULL AND RegisterID = @RegisterId ORDER BY OrganisationName";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterId", registerID);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

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

                RegisterVM obj = new RegisterVM();
                obj.register = reg;
                obj.organisation = org;

                list.Add(obj);
            }
            reader.Close();
            return list;
        }

        public static int UpdateOrganisationRegister(int verenigingId, int kassaId)
        {
            string sql = "UPDATE [CashlessPayment].[dbo].[Organisation_Register] SET [OrganisationID] = @OrganisationId WHERE [RegisterID] = @RegisterId";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationId", verenigingId);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@RegisterId", kassaId);
            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2);
        }

    }
}