using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.ITAdmin
{
    public class Register
    {
        public int ID { get; set; }
        public string RegisterName { get; set; }
        public string Device { get; set; }
        public Int32 PurchaseDate { get; set; }
        public Int32 ExpiresDate { get; set; }

    }
}
