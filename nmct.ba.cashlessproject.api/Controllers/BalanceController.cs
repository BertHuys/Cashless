using nmct.ba.cashlessproject.api.Models;
using nmct.ba.cashlessproject.models.ClientUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class BalanceController : ApiController
    {
        public HttpResponseMessage Post(Customer c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int balance = CustomerDA.GetBalanceByCustomer(c.CustomerName, c.Address, p.Claims);
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(balance.ToString());
            return message;
        }

        public HttpResponseMessage Put(Customer c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            CustomerDA.UpdateCustomer(c.Balance, c.CustomerName, c.Address, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}