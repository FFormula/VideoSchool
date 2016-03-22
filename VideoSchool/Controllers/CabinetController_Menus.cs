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
            try
            { 
               Menus menus = new Menus (this.shared);
               menus.SelectMenus();
               return View(menus);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        public ActionResult MenusEdit ()
        {
            try
            {
                string id = (RouteData.Values["id"] ?? "").ToString();
                if (id == "")
                    return RedirectToAction("MenusList", "Cabinet");
                Menus menus = new Menus(this.shared);
                if (id == "Add")
                    menus.SelectNew();
                else
                {
                   
                    menus.Select(id);
                }
                return View(menus);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }
    }
}