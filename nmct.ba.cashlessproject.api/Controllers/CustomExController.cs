﻿using nmct.ba.cashlessproject.api.Models;
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
    public class CustomExController : ApiController
    {
        // GET: CustomEx
        public HttpResponseMessage Post(Customer c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int row = CustomerDA.CustomerExist(c.CustomerName, c.Address, p.Claims);
            if (row == 1)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}