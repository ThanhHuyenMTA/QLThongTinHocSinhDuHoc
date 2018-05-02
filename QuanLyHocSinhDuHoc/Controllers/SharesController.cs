using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaymentSystem.Controllers
{
    public class SharesController : BaseController
    {
        //
        // GET: /Partials/Banner
        public ActionResult Banner()
        {
            return View();
        }

        //
        // GET: /Partials/Submenu
        public ActionResult Submenu()
        {
            return View();
        }

        //
        // GET: /Partials/Banner
        public ActionResult Footer()
        {
            return View();
        }
	}
}