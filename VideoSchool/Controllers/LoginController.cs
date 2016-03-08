using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSchool.Models;

namespace VideoSchool.Controllers
{
    public class LoginController : Controller
    {
        Shared shared;
        User user;

        public LoginController ()
        {
            shared = new Shared();
            user = new User(shared);
        }

        [HttpGet]
        public ActionResult Index()
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
        public ActionResult Index(User post)
        {
            try
            {
                user.email = post.email;
                user.passw = post.passw;
                user.Login();
                if (shared.error.UserErrors())
                {
                    ViewBag.error = shared.error.text;
                    return View(user);
                }
                user.Select(user.id);

                Session["user_id"] = user.id;
                Session["user_email"] = user.email;
                Session["user_name"] = user.name;

                return RedirectToAction("Index", "Login");
            } 
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        public ActionResult Logout ()
        {
            try
            {
                Session["user_id"] = null;
                Session["user_name"] = null;
                Session["user_email"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        [HttpGet]
        public ActionResult Signup()
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
        public ActionResult Signup(User post)
        {
            try 
            {
                user.name = post.name;
                user.email = post.email;
                user.passw = post.passw;
                user.Insert();
                if (shared.error.UserErrors ())
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

        public ActionResult ShowError(Exception ex)
        {
            if (shared.error.NoErrors())
                shared.error.MarkSystemError(ex);
            return View("~/Views/Error.cshtml", shared.error);
        }

    }
}