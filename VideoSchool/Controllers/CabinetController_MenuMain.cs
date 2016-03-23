using System;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;
using VideoSchool.Models.Share;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {
        public ActionResult MenuMainList ()
        {
            try
            { 
               MenuMain menumain = new MenuMain (this.shared);
               menumain.SelectMenuMain();
               return View(menumain);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        [HttpGet]
        public ActionResult MenuMainEdit ()
        {
            try
            {
                string id = GetRouteID();
                if (id == "")
                    return RedirectToAction("MenuMainList", "Cabinet");
                MenuMain menumain = new MenuMain(this.shared);
                if (id == "Add")
                    menumain.SelectNew();
                else
                    menumain.Select(id);
                return View(menumain);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        [HttpPost]
        public ActionResult MenuMainEdit(MenuMain post)
        {
            try 
            {
                string id = GetRouteID();
                if (id == "")
                    return RedirectToAction("MenuMainList", "Cabinet");
                MenuMain menumain = new MenuMain(this.shared);
                if (id == "Add")
                {
                    menumain.SelectNew();
                    menumain.Copy(post);
                    menumain.Insert();
                }
                else
                {
                    menumain.Select(id);
                    if (shared.error.AnyError())
                        return RedirectToAction("MenuMainList", "Cabinet");
                    menumain.Copy(post);
                    menumain.Update();
                }
                return RedirectToAction("MenuMainList", "Cabinet");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

    
    }
}