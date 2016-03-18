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
        public ActionResult ActionList(string filter)
        {
            try
            {
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
        public ActionResult ActionEdit()
        {
            try
            {
                string id = (RouteData.Values["id"] ?? "").ToString();
                if (id == "")
                    return RedirectToAction("ActionList", "Cabinet");
                Models.Units.Action action = new Models.Units.Action(shared);
                if (id == "Add")
                    action.SelectNew();
                else
                    action.Select(id);
                return View(action);
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
        public ActionResult ActionEdit(Models.Units.Action post)
        {
            try
            {
                string id = (RouteData.Values["id"] ?? "").ToString();
                if (id == "")
                    return RedirectToAction("ActionList", "Cabinet");
                Models.Units.Action action = new Models.Units.Action(shared);
                if (id == "Add")
                {
                    action.SelectNew();
                    action.name = post.name;
                    action.info = post.info;
                    action.status = post.status;
                    action.Insert();
                }
                else
                {
                    action.Select(id);
                    if (shared.error.AnyError())
                        return RedirectToAction("ActionList", "Cabinet");
                    action.name = post.name;
                    action.info = post.info;
                    action.status = post.status;
                    action.Update();
                }
                return RedirectToAction("ActionList", "Cabinet");
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
        public ActionResult ActionDrop()
        {
            try
            {
                string id = (RouteData.Values["id"] ?? "").ToString();
                if (id == "")
                    return RedirectToAction("ActionList", "Cabinet");
                Models.Units.Action action = new Models.Units.Action(shared);
                action.Delete(id);
                return RedirectToAction("ActionList", "Cabinet");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }


    }
}