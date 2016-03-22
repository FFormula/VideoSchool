using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSchool.Models;
using VideoSchool.Models.Units;
using VideoSchool.Models.Share;

namespace VideoSchool.Controllers
{
    public class LoginController : Controller
    {
        Shared shared;
        User user;

        /// <summary>
        /// Create a Share and Init Login Controller 
        /// </summary>
        public LoginController ()
        {
            shared = new Shared();
            user = new User(shared);
            shared.menu.Init("HOME");
            //
        }

        /// <summary>
        /// Generate an Index page
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            try 
            {
                shared.menu.SetActive ("login_index");
                return View(user);
            } 
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Process an Sign In
        /// </summary>
        /// <param name="post">email and passw</param>
        /// <returns>An error view or Redirect to the Index</returns>
        [HttpPost]
        public ActionResult Index(User post)
        {
            try
            {
                user.email = post.email;
                user.passw = post.passw;
                user.Login();
                if (shared.error.UserError())
                {
                    ViewBag.error = shared.error.text;
                    return View(user);
                }
                user.Select(user.id);
                SetLoginUserParams();
                return RedirectToAction("Index", "Login");
            } 
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        private void SetLoginUserParams()
        {
            Session["logged"] = "1";
            Session["user_id"] = user.id;
            Session["user_email"] = user.email;
            Session["user_name"] = user.name;
        }

        private void ClearLoginUserParams()
        {
            Session["logged"] = null;
            Session["user_id"] = null;
            Session["user_name"] = null;
            Session["user_email"] = null;
        }

        /// <summary>
        /// Lougout from site
        /// </summary>
        /// <returns>An error or redirect to Index page</returns>
        public ActionResult Logout ()
        {
            try
            {
                ClearLoginUserParams();
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// A new User Registration Form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Signup()
        {
            try
            {
                shared.menu.SetActive("home_signup");
                return View(user);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Process User Registration
        /// </summary>
        /// <param name="post">name, email, passw</param>
        /// <returns>An error view or redirect to Index page</returns>
        [HttpPost]
        public ActionResult Signup(User post)
        {
            try 
            {
                user.name = post.name;
                user.email = post.email;
                user.passw = post.passw;
                user.Insert();
                if (shared.error.UserError ())
                {
                    ViewBag.error = shared.error.text;
                    return View(user);
                }
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        public ActionResult RequestPassword()
        {
            try
            {
                return View(user);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        [HttpPost]
        public ActionResult RequestPassword(User post)
        {
            try 
            {
                UserPassw up = new UserPassw(shared);
                up.email = post.email;
                up.passw = post.passw;
                string code = up.RequestPassword();
                if (shared.error.UserError())
                {
                    ViewBag.error = shared.error.text;
                    return View(user);
                }

                EMail email = new EMail(shared);
                
                string message = @"To use your new password follow the link:
                
                " + shared.config.hostname + "Login/ActivatePassword?code=" + code;
                
                email.Send (up.email, "Activate your new Password", message);

                return RedirectToAction("ActivatePassword", "Login");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        [HttpGet]
        public ActionResult ActivatePassword (string code)
        {
            try {
                if ((code ?? "") == "")
                {
                    ViewBag.error = "Check your email and follow activation link to set activate your new password";
                    return View(user);
                }
                UserPassw up = new UserPassw(shared);
                string myid = up.ActivatePassword (code);
                if (shared.error.UserError())
                {
                    ViewBag.error = shared.error.text;
                    return View(user);
                }
                if (myid == "")
                {
                    ViewBag.error = "User not found";
                    return View(user);
                }
                user.Select(myid);
                SetLoginUserParams();
                ViewBag.success = "1";
                return View(user);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Generate an Error View
        /// </summary>
        /// <param name="ex">Last Exception</param>
        /// <returns>ActionResult for View</returns>
        public ActionResult ShowError(Exception ex)
        {
            if (shared.error.NoErrors())
                shared.error.MarkSystemError(ex);
            return View("~/Views/Error.cshtml", shared.error);
        }

    }
}