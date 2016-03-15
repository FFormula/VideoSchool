using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;

namespace VideoSchool.Controllers
{
    public class CabinetController : Controller
    {
        Shared shared;

        /// <summary>
        /// Constructor
        /// </summary>
        public CabinetController ()
        {
            shared = new Shared(RunMode.WebDebug);
        }

        // GET: Cabinet
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// A list of all applicable schools
        /// </summary>
        /// <returns></returns>
        public ActionResult School()
        {
            return View();
        }

        /// <summary>
        /// A list of my grades
        /// </summary>
        /// <returns></returns>
        public ActionResult Grades()
        {
            return View();
        }

        /// <summary>
        /// List of all Users for administrator
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserList(string filter = null)
        {
            try
            {
                User user = new User(shared);
                user.filter = filter ?? "";
                user.SelectUsers();
                return View(user);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        [HttpGet]
        public ActionResult UserEdit()
        {
            try
            {
                string id = (RouteData.Values["id"] ?? "").ToString();
                if (id == "")
                    return RedirectToAction("UserList", "Cabinet");
                User user = new User(shared);
                user.Select(id);
                return View(user);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        [HttpPost]
        public ActionResult UserEdit(User post)
        {
            try
            {
                string id = (RouteData.Values["id"] ?? "").ToString();
                if (id == "")
                    return RedirectToAction("UserList", "Cabinet");
                User user = new User(shared);
                user.Select(id);
                if (shared.error.AnyError())
                    return RedirectToAction("UserList", "Cabinet");
                user.name = post.name;
                user.email = post.email;
                user.Update();
                return RedirectToAction("UserList", "Cabinet");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        [HttpGet]
        public ActionResult ActionList (string filter)
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

        [HttpGet]
        public ActionResult ActionEdit ()
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

        [HttpPost]
        public ActionResult ActionEdit (Models.Units.Action post)
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


        //---



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
                    role.Select(id);
                return View(role);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }
        //-------





        /// <summary>
        /// A list of medals
        /// </summary>
        /// <returns></returns>
        public ActionResult Medals()
        {
            return View();
        }

        /// <summary>
        /// System information for an administrator
        /// </summary>
        /// <returns></returns>
        public ActionResult System()
        {
            return View();
        }

        /// <summary>
        /// Manage menu topics
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            return View();
        }

        /// <summary>
        /// A list of all payments
        /// </summary>
        /// <returns></returns>
        public ActionResult Payments()
        {
            return View();
        }

        /// <summary>
        /// Generate an Error View
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public ActionResult ShowError(Exception ex)
        {
            if (shared.error.NoErrors())
                shared.error.MarkSystemError(ex);
            return View("~/Views/Error.cshtml", shared.error);
        }
    }
}