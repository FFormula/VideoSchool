using System;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {

        public ActionResult Password()
        {
            try
            {
                UserPassw passw = new UserPassw (shared);
                Init("cabinet_password");
                return View(passw);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        public ActionResult EditPassword()
        {
            UserPassw passw = new UserPassw(shared);
            Init("cabinet_password");

            return View("PasswordNew", passw);
        }
    }
}