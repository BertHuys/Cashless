﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.models.ITAdmin
{
    public class Errorlog
    {
        public int RegisterID { get; set; }
        public Int32 Timestamp { get; set; }
        public string Message { get; set; }
        public string Stacktrace { get; set; }
    }
}
