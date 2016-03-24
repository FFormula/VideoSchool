using System;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;
using VideoSchool.Models.Share;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {

        public ActionResult Menu (string SelectMenuId = "", string filter="")
        {
            try
            {
                Init("cabinet_menu");
                Menus menus = new Menus(this.shared);
                if (SelectMenuId != "")
                    menus.SelectMenuForMain(SelectMenuId);
                menus.SelectMenuMainForFilterMenus();
                return View(menus);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        [HttpGet]
        public ActionResult MenuEdit ()
        {
            try
            {
                Init("cabinet_menu");
                if (id == "")
                    return RedirectToAction("Menu", "Cabinet");
                Menus menus = new Menus(this.shared);
                if (id == "Add")
                    menus.SelectNew();
                else
                    menus.Select(id);
                menus.SelectMenuMainForFilterMenus(menus.main_id);
                return View(menus);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        [HttpPost]
        public ActionResult MenuEdit(Menus post)
        {
            try 
            {
                Init("cabinet_menu");
                if (id == "")
                    return RedirectToAction("Menu", "Cabinet");
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
                        return RedirectToAction("Menu", "Cabinet");
                    menus.Copy(post);
                    menus.Update();
                }
                return RedirectToAction("Menu", "Cabinet");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        public ActionResult MenuMoveUp(string SelectMenuId = "")
        {
            try
            {
                Init("cabinet_menu");
                if (id == "")
                    return RedirectToAction("Menu", "Cabinet");
                Menus menus = new Menus(this.shared);
                menus.MoveUp(id);
                return RedirectToAction("Menu", "Cabinet", new { SelectMenuId = SelectMenuId });
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        public ActionResult MenuMoveDn(string SelectMenuId = "")
        {
            try
            {
                Init("cabinet_menu");
                if (id == "")
                    return RedirectToAction("Menu", "Cabinet");
                Menus menus = new Menus(this.shared);
                menus.MoveDn(id);
                return RedirectToAction("Menu", "Cabinet", new { SelectMenuId = SelectMenuId });
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
         }

        public ActionResult MenuMain()
        {
            try
            {
                Init("cabinet_menumain");
                MenuMain menumain = new MenuMain(this.shared);
                menumain.SelectMenuMain();
                return View(menumain);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        [HttpGet]
        public ActionResult MenuMainEdit()
        {
            try
            {
                Init("cabinet_menumain");
                if (id == "")
                    return RedirectToAction("MenuMain", "Cabinet");
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
                Init("cabinet_menumain");
                if (id == "")
                    return RedirectToAction("MenuMain", "Cabinet");
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
                        return RedirectToAction("MenuMain", "Cabinet");
                    menumain.Copy(post);
                    menumain.Update();
                }
                return RedirectToAction("MenuMain", "Cabinet");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }



        }
    }
}