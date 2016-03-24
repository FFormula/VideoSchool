using System;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {
        public ActionResult Contact()
        {
            try
            {
                Init("cabinet_contact");
                UserContact contact = new Models.Units.UserContact(shared);
                return View(contact);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

    }
}