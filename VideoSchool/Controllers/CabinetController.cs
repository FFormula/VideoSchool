using System;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;
using VideoSchool.Models.Share;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {
        Shared shared;
        string currUserId = "";

        /// <summary>
        /// Constructor
        /// </summary>
        public CabinetController ()
        {
            shared = new Shared();
            shared.menu.Init("HOME");

        }

        /// <summary>
        /// Init current user information
        /// </summary>
        public void Init ()
        {
            currUserId = Session["user_id"].ToString();
            shared.menu.Init("HOME");
        }

        /// <summary>
        /// User summary
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Init();
            User user = new User(shared);
            user.Select(currUserId);
            ViewBag.UserID = currUserId;
            return View(user);
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