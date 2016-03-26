using System;
using System.Web.Mvc;
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
        public ActionResult User(string filter = null)
        {
            try
            {
                Init("cabinet_user");
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
        public ActionResult EditUser()
        {
            try
            {
                Init("cabinet_user");
                if (id == "")
                    return RedirectToAction("User", "Cabinet");
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
        public ActionResult EditUser(User post)
        {
            try
            {
                Init("cabinet_user");
                if (id == "")
                    return RedirectToAction("User", "Cabinet");
                User user = new User(shared);
                user.Select(id);
                if (shared.error.AnyError())
                    return RedirectToAction("User", "Cabinet");
                user.name = post.name;
                user.email = post.email;
                user.Update();
                return RedirectToAction("User", "Cabinet");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }
    }
}