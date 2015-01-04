using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using nmct.ba.cashlessproject.models.ClientUser;
using nmct.ba.cashlessproject.api.Models;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class ErrorLogController : ApiController
    {
        public HttpResponseMessage Post(ErrorLog e)
        {
            int rows = ErrorLogDA.CreateErrorLog(e);
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(rows.ToString());
            return message;
        }
    }
}