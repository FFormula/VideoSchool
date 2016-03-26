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
        public ActionResult Role(string filter)
        {
            try
            {
                Init("cabinet_role");
                Role role = new Role(shared);
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
        public ActionResult EditRole()
        {
            try
            {
                Init("cabinet_role");
                if (id == "")
                    return RedirectToAction("Role", "Cabinet");
                Role role = new Role(shared);
                if (id == "Add")
                    role.SetDefaults();
                else
                    role.Select(id);
                return View("RoleEdit",role);
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
        public ActionResult EditRole(Role post)
        {
            try
            {
                Init("cabinet_role");
                if (id == "")
                    return RedirectToAction("Role", "Cabinet");
                Role role = new Role(shared);
                if (id == "Add")
                {
                    role.SetDefaults();
                    role.Copy(post);
                    role.Insert();
                }
                else
                {
                    role.Select(id);
                    if (shared.error.AnyError())
                        return RedirectToAction("Role", "Cabinet");
                    role.Copy(post);
                    role.Update();
                }
                return RedirectToAction("Role", "Cabinet");
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
        public ActionResult RoleAction(string RoleId)
        {
            try
            {
                Init("cabinet_role");
                if (RoleId == "")
                    return RedirectToAction("Role", "Cabinet");
                RoleAction roleAction = new RoleAction(shared);
                roleAction.SelectActionsInRole(RoleId);
                roleAction.SelectNewActionsForRole(RoleId);
                return View(roleAction);
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
        /// <param name="ActionId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddRoleAction(string RoleId = "", string ActionId = "")
        {
            try
            {
                Init("cabinet_role");
                if (RoleId == "") return RedirectToAction("RoleAction", "Cabinet");
                if (ActionId == "") return RedirectToAction("RoleAction", "Cabinet");
                RoleAction roleAction = new RoleAction(shared);
                roleAction.InsertRoleAction(RoleId, ActionId);
                if (shared.error.AnyError())
                    return RedirectToAction("RoleAction", "Cabinet");
                return RedirectToAction("RoleAction", "Cabinet", new { RoleId = RoleId });
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
        public ActionResult ActionRoleDrop(string RoleId = "", string ActionId = "")
        {
            try 
            {
                Init("cabinet_role");
                if (RoleId == "") return RedirectToAction("Role", "Cabinet");
                if (ActionId == "") return RedirectToAction("RoleAction", "Cabinet", new { RoleId = RoleId });
                RoleAction roleAction = new RoleAction(shared);
                roleAction.DeleteRoleAction(RoleId, ActionId);
                if (shared.error.AnyError())
                    return RedirectToAction("RoleAction", "Cabinet", new { RoleId = RoleId });
                return RedirectToAction("RoleAction", "Cabinet", new { RoleId = RoleId });
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }    
    }
}