using System;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {
        public ActionResult Passport ()
        {
            try
            {
                Init("cabinet_passport");
                UserPassport passport = new Models.Units.UserPassport(shared);
                return View(passport);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }
    
    }
}