using nmct.ba.cashlessproject.ITWeb.DA;
using nmct.ba.cashlessproject.models.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.ITWeb.Controllers
{
    [Authorize]
    public class OrganisationController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<Organisation> organisations = new List<Organisation>();
            organisations = OrganisationDA.GetOrganisations();
            return View(organisations);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Organisation organisation)
        {
            int rowsaffected = OrganisationDA.NewOrganisation(organisation);
            if (rowsaffected == 0)
                return View("Error");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Organisation organisation = new Organisation();
            organisation = OrganisationDA.GetOrganisationById(id.Value);
            
            return View(organisation);
        }

        [HttpPost]
        public ActionResult Edit(Organisation organisation)
        {
            int rowsaffected = OrganisationDA.UpdateOrganisation(organisation);
            if (rowsaffected == 0)
                return View("Error");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Info(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Organisation organisation = new Organisation();
            organisation = OrganisationDA.GetOrganisationById(id.Value);

            return View(organisation);
        }

    }
}