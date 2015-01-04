using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.ITWeb.Models
{
    public class RegisterLog
    {
        public int ID { get; set; }
        public string RegisterName { get; set; }
        public string Device { get; set; }
        public int PurchaseDate { get; set; }
        public int ExpiresDate { get; set; }
        public int RegisterID { get; set; }
        public int Timestamp { get; set; }
        public string Message { get; set; }
        public string Stacktrace { get; set; }
        public string OrganisationName { get; set; }
    }
}