using System;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {
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

        /// <summary>
        /// Load user edit form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserEdit()
        {
            try
            {
                string id = GetRouteID();
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

        /// <summary>
        /// Save posted user data
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserEdit(User post)
        {
            try
            {
                string id = GetRouteID();
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
    }
}