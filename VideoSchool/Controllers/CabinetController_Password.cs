using System;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {
        [HttpGet]
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



        [HttpPost]
        public ActionResult Password(UserPassw post)
        {
            try
            {
                UserPassw passw = new UserPassw(shared);

                passw.Copy(post);
                passw.ChangePassword();
                Init("cabinet_password");
                return View(passw);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }


    }
}