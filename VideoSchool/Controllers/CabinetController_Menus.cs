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

        [HttpGet]
        public ActionResult MenusEdit ()
        {
            try
            {
                string id = GetRouteID();
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

        [HttpPost]
        public ActionResult MenusEdit(Menus post)
        {
            try 
            {
                string id = GetRouteID();
                if (id == "")
                    return RedirectToAction("MenusList", "Cabinet");
                Menus menus = new Menus(this.shared);
                if (id == "Add")
                {
                    menus.SelectNew();
                    menus.Copy(post);
                    menus.Insert();
                }
                else
                {
                    menus.Select(id);
                    if (shared.error.AnyError())
                        return RedirectToAction("MenusList", "Cabinet");
                    menus.Copy(post);
                    menus.Update();
                }
                return RedirectToAction("MenusList", "Cabinet");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        public ActionResult MenusMoveUp()
        {
            try
            {
                string id = GetRouteID();
                if (id == "")
                    return RedirectToAction("MenusList", "Cabinet");
                Menus menus = new Menus(this.shared);
                menus.MoveUp(id);
                return RedirectToAction("MenusList", "Cabinet");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        public ActionResult MenusMoveDn()
        {
            try
            {
                string id = GetRouteID();
                if (id == "")
                    return RedirectToAction("MenusList", "Cabinet");
                Menus menus = new Menus(this.shared);
                menus.MoveDn(id);
                return RedirectToAction("MenusList", "Cabinet");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }
    }
}