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
    public class RegisterController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<RegisterVM> allRegisters = new List<RegisterVM>();
            allRegisters = RegisterDA.GetAllRegisters();

            List<RegisterVM> ToegekendeRegisters = new List<RegisterVM>();
            List<RegisterVM> BeschikbareRegisters = new List<RegisterVM>();

            foreach (RegisterVM item in allRegisters)
            {
                if (item.organisation.OrganisationName != "")
                    ToegekendeRegisters.Add(item);
                else
                    BeschikbareRegisters.Add(item);
            }

            ViewBag.Registers = allRegisters;
            ViewBag.BeschikbareRegisters = BeschikbareRegisters;
            ViewBag.ToegekendeRegisters = ToegekendeRegisters;

            List<Organisation> organisations = new List<Organisation>();
            organisations = OrganisationDA.GetOrganisations();
            ViewBag.organisations = organisations;

            return View();
        }

        [HttpGet]
        public ActionResult Search(int organisation)
        {
            List<RegisterVM> registers = new List<RegisterVM>();
            registers = RegisterDA.GetRegisterByOrganisation(organisation);
            if (registers.Count != 0)
                ViewBag.Titel = registers.Count + " found " + registers[0].organisation.OrganisationName;
            else
                ViewBag.Titel = "no results.";

            return View(registers);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Register register)
        {
            int rowsaffected = RegisterDA.NewRegister(register);
            if (rowsaffected == 0)
                return View("Error");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            List<Register> registers = new List<Register>();
            registers = RegisterDA.GetRegisters();
            ViewBag.registers = registers;

            List<Organisation> organisations = new List<Organisation>();
            organisations = OrganisationDA.GetOrganisations();
            ViewBag.Organisations = organisations;

            return View();
        }

        [HttpPost]
        public ActionResult Edit(int register, int organisation)
        {
            List<RegisterVM> asign = new List<RegisterVM>();
           asign = RegisterDA.CheckRegister(register, organisation);

            if (asign.Count == 0) 
            {
                int rowsaffected = RegisterDA.AddRegisterOrganisation(register, organisation);
            }
            else if (asign.Count == 1)
            {
                int rowsaffected = RegisterDA.UpdateOrganisationRegister(register, organisation);
            }

            return RedirectToAction("Index", "Register");
        }

    }
}