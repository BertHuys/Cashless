using nmct.ba.cashlessproject.ITWeb.DA;
using nmct.ba.cashlessproject.ITWeb.ViewModels;
using nmct.ba.cashlessproject.models.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.ITWeb.Controllers
{
        [Authorize]
        public class LogController : Controller
        {
            [HttpGet]
            public ActionResult Index()
            {
                List<Register> registers = new List<Register>();
                registers = RegisterDA.GetRegisters();
                ViewBag.Registers = registers;

                List<LogVM> log = new List<LogVM>();
                log = LogDA.GetLog();

                return View(log);
            }

            [HttpGet]
            public ActionResult Search(int register)
            {
                ViewBag.Titel = register + " Log";

                List<LogVM> log = new List<LogVM>();
                log = LogDA.GetLogById(register);

                return View(log);
            }

        }
    }