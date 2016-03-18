using System;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {
        /// <summary>
        /// list roles of the filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ActionResult RoleList(string filter)
        {
            try
            {
                Models.Units.Role role = new Models.Units.Role(shared);
                role.filter = filter ?? "";
                role.SelectRoles();
                return View(role);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Load Role data form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoleEdit()
        {
            try
            {
                string id = (RouteData.Values["id"] ?? "").ToString();
                if (id == "")
                    return RedirectToAction("RoleList", "Cabinet");
                Models.Units.Role role = new Models.Units.Role(shared);
                if (id == "Add")
                    role.SelectNew();
                else
                    role.Select(id);
                return View(role);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Save posted Role data
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RoleEdit(Models.Units.Role post)
        {
            try
            {
                string id = (RouteData.Values["id"] ?? "").ToString();
                if (id == "")
                    return RedirectToAction("RoleList", "Cabinet");
                Models.Units.Role role = new Models.Units.Role(shared);
                if (id == "Add")
                {
                    role.SelectNew();
                    role.name = post.name;
                    role.info = post.info;

                    role.Insert();
                }
                else
                {
                    role.Select(id);
                    if (shared.error.AnyError())
                        return RedirectToAction("RoleList", "Cabinet");
                    role.name = post.name;
                    role.info = post.info;

                    role.Update();
                }
                return RedirectToAction("RoleList", "Cabinet");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Show List of Action for specified Role
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleAction()
        {
            try
            {
                string id = (RouteData.Values["id"] ?? "").ToString();
                if (id == "")
                    return RedirectToAction("RoleList", "Cabinet");
                Models.Units.Role role = new Models.Units.Role(shared);
                if (id == "Add")
                    role.SelectNew();
                else
                {
                    role.Select(id);
                    role.SelectActionByRoleID();
                    role.SelectActionForAddRole(role.id);
                }
                return View(role);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Add new record to table role_action
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="AddActionId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddAction4Role(string RoleId = "", string AddActionId = "")
        {
            try
            {
                /*if (RoleId == "")
                    return RedirectToAction("RoleList", "Cabinet");*/
                Models.Units.Role role = new Models.Units.Role(shared);

                role.Select(RoleId);
                role.InsertActionToRole(RoleId, AddActionId);


                return RedirectToAction("RoleAction", "Cabinet", new { id = RoleId });
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Delete record from table role_action
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="DelActionId"></param>
        /// <returns></returns>
        public ActionResult DelActionInRole(string RoleId = "", string DelActionId = "")
        {

            Models.Units.Role role = new Models.Units.Role(shared);
            role.Select(RoleId);
            role.DeleteFromRole(RoleId, DelActionId);

            return RedirectToAction("RoleAction", "Cabinet", new { id = RoleId });
        }    
    }
}