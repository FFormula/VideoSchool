using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSchool.Models;

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