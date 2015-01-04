using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.models.ClientUser
{
    public class RegisterEmployee
    {
        public int RegisterID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
    }
}
