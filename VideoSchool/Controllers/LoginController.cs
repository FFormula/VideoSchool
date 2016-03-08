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
            shared = new Shared(this);
            user = new User(shared);
        }

        [HttpGet]
        public ActionResult Index()
        {
            user.Select("1");
            if (shared.error.IsErrors()) return ShowError();
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(User post)
        {
            user.email = post.email;
            user.passw = post.passw;
            user.Login();
            if (shared.error.mode == ErrorMode.UserError)
            {
                ViewBag.error = shared.error.text;
                return View(user);
            }
            if (shared.error.IsErrors()) return ShowError();                
            user.Select(user.id);
            if (shared.error.IsErrors()) return ShowError();                

            Session["user_id"] = user.id;
            Session["user_email"] = user.email;
            Session["user_name"] = user.name;

            return RedirectToAction("Index", "Login");
        }


        public ActionResult Logout ()
        {
            Session["user_id"] = null;
            Session["user_name"] = null;
            Session["user_email"] = null;
            if (shared.error.IsErrors()) return ShowError();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult Signup()
        {
            if (shared.error.IsErrors()) return ShowError();
            return View(user);
        }

        [HttpPost]
        public ActionResult Signup(User post)
        {
            user.name = post.name;
            user.email = post.email;
            user.passw = post.passw;
            user.Insert();
            if (shared.error.mode == ErrorMode.UserError)
            {
                ViewBag.error = shared.error.text;
                return View(user);
            }
            if (shared.error.IsErrors()) return ShowError();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult ShowError()
        {
            return View("~/Views/Error.cshtml", shared.error);
        }

    }
}