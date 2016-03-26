using System;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {

        /// <summary>
        /// Show list of all actions / by filter
        /// </summary>
        /// <param name="filter">Any searchable filter</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Action(string filter)
        {
            try
            {
                Init("cabinet_action");
                Models.Units.Action action = new Models.Units.Action(shared);
                action.filter = filter ?? "";
                action.SelectActions();
                return View(action);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Load data form for action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditAction()
        {
            try
            {
                Init("cabinet_action");
                if (id == "")
                    return RedirectToAction("Action", "Cabinet");
                Models.Units.Action action = new Models.Units.Action(shared);
                if (id == "Add")
                    action.SetDefaults();
                else
                    action.Select(id);
                return View("ActionEdit",action);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Save posted data for Action
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditAction(Models.Units.Action post)
        {
            try
            {
                Init("cabinet_action");
                if (id == "")
                    return RedirectToAction("Action", "Cabinet");
                Models.Units.Action action = new Models.Units.Action(shared);
                if (id == "Add")
                {
                    action.SetDefaults();
                    action.Copy(post);
                    action.Insert();
                }
                else
                {
                    action.Select(id);
                    if (shared.error.AnyError())
                        return RedirectToAction("Action", "Cabinet");
                    action.Copy(post);
                    action.Update();
                }
                return RedirectToAction("Action", "Cabinet");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Delete action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DropAction()
        {
            try
            {
                Init("cabinet_action");
                if (id == "")
                    return RedirectToAction("Action", "Cabinet");
                Models.Units.Action action = new Models.Units.Action(shared);
                action.Delete(id);
                return RedirectToAction("Action", "Cabinet");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }
    }
}