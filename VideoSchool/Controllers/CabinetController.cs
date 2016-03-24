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
        string role;
        string id;

        /// <summary>
        /// Constructor
        /// </summary>
        public CabinetController ()
        {
            shared = new Shared();
        }

        /// <summary>
        /// Init current user information
        /// </summary>
        public void Init ()
        {
            role = GetRouteRole().ToUpper();
            shared.menu.Init(role.ToUpper());
            id = GetRouteID();
            try   { currUserId = Session["user_id"].ToString(); }
            catch { currUserId = "";  }
        }

        public void Init (string menu)
        {
            Init();
            shared.menu.SetActive(menu);
        }

        /// <summary>
        /// User summary
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Init("cabinet_index");
            User user = new User(shared);
            user.Select(currUserId);
            ViewBag.UserID = currUserId;
            return View(user);
        }

        protected string GetRouteID()
        {
            return (RouteData.Values["id"] ?? "").ToString();
        }

        protected string GetRouteRole()
        {
            return (RouteData.Values["role"] ?? "").ToString();
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