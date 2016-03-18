using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSchool.Models;

namespace VideoSchool.Controllers
{
    public class HelpController : Controller
    {
        Shared shared;

        public HelpController()
        {
            shared = new Shared();
            shared.menu.Init("home");
        }

        // GET: Help
        public ActionResult Index()
        {
            return View(shared);
        }

        public ActionResult About()
        {
            return View(shared);
        }

    }
}