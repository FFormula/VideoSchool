using System;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;
using VideoSchool.Models.Share;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {

        public ActionResult Menu ()
        {
            try
            {
                Init("cabinet_menu");
                Menus menus = new Menus(shared);
                if (id != "")
                    menus.SelectMenuInMenuMain(id);
                menus.SelectMenuMain();
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
                    menus.SetDefaults();
                else
                    menus.Select(id);
                menus.SelectMenuMain();
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
                    return RedirectToAction("MainMenu", "Cabinet");
                Menus menus = new Menus(this.shared);
                if (id == "Add")
                {
                    menus.SetDefaults();
                    menus.Copy(post);
                    menus.Insert();
                }
                else
                {
                    menus.Select(id);
                    if (shared.error.AnyError())
                        return RedirectToAction("MainMenu", "Cabinet");
                    menus.Copy(post);
                    menus.Update();
                }
                return RedirectToAction("Menu", "Cabinet", new { id = menus.main_id } );
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        public ActionResult MenuMoveUp()
        {
            try
            {
                Init("cabinet_menu");
                if (id == "")
                    return RedirectToAction("Menu", "Cabinet");
                Menus menus = new Menus(this.shared);
                menus.MoveUp(id);
                return RedirectToAction("Menu", "Cabinet", new { id = menus.main_id });
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        public ActionResult MenuMoveDn()
        {
            try
            {
                Init("cabinet_menu");
                if (id == "")
                    return RedirectToAction("Menu", "Cabinet");
                Menus menus = new Menus(this.shared);
                menus.MoveDn(id);
                return RedirectToAction("Menu", "Cabinet", new { id = menus.main_id });
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
                Init("cabinet_menu");
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
                Init("cabinet_menu");
                if (id == "")
                    return RedirectToAction("MenuMain", "Cabinet");
                MenuMain menumain = new MenuMain(this.shared);
                if (id == "Add")
                    menumain.SetDefaults();
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
                Init("cabinet_menu");
                if (id == "")
                    return RedirectToAction("MenuMain", "Cabinet");
                MenuMain menumain = new MenuMain(this.shared);
                if (id == "Add")
                {
                    menumain.SetDefaults();
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

        public ActionResult DelMenu()
        {
            try
            {
                Init("cabinet_menu");
                if (id == "")
                    return RedirectToAction("MenuMain", "Cabinet");
                Menus menus = new Menus(this.shared);
                menus.Select(id);
                if (shared.error.AnyError())
                    return RedirectToAction("MenuMain", "Cabinet");
                string menuMainId = menus.main_id;
                menus.Delete(id);
                return RedirectToAction("Menu", "Cabinet", new { id = menuMainId });
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

    }
}