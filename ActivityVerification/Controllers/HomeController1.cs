using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActivityVerification.Models;

namespace ActivityVerification.Controllers
{
    [Authorize]
    public class HomeController1 : Controller
    {
        public ActionResult Index(string search, int? pageNumber)
        {
            ViewBag.Title = "Activities Dashboard";
            var actvities = new Entities();
            if (String.IsNullOrEmpty(search))
            {
                return View(actvities.Activities.OrderByDescending(s => s.Id).ToList());
            }
            else
            {
                return View(actvities.Activities.Where(x => x.Description.Contains(search) || search == null).OrderByDescending(s => s.Id).ToList());
            }
        }
    }
}