using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.models.ClientUser
{
    public class Customer
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public byte[] Picture { get; set; }
        public int Balance { get; set; }
        public override string ToString()
        {
            return this.CustomerName;
        }
    }
}
