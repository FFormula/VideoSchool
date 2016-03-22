using System;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;
using VideoSchool.Models.Share;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {
        public ActionResult MenusList ()
        {
            Menus menus = new Menus (this.shared);
            menus.SelectMenus();
            return View(menus);
        }

        public ActionResult MenusEdit ()
        {
            Menus menus = new Menus(this.shared);
            return View(menus);
        }
    }
}