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
        MySQL sql;
        User user;
        public ActionResult ErrorActionResult;

        public LoginController ()
        {
            sql = new MySQL();
            user = new User(sql);
            ViewBag.error = "";
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (IsError()) return ErrorActionResult;
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(User post)
        {
            user.email = post.email;
            user.passw = post.passw;
            if (user.Login())
            {
                user.Select(user.id);
                Session["user_id"] = user.id;
                Session["user_email"] = user.email;
                Session["user_name"] = user.name;
                if (IsError()) return ErrorActionResult;
                return RedirectToAction("Index", "Login");
            }
            ViewBag.error = "Адрес эл. почты или пароль указан не верно";
            if (IsError()) return ErrorActionResult;
            return View(user);
        }


        public ActionResult Logout ()
        {
            Session["user_id"] = null;
            Session["user_name"] = null;
            Session["user_email"] = null;
            if (IsError()) return ErrorActionResult;
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult Signup()
        {
            if (IsError()) return ErrorActionResult;
            return View(user);
        }

        [HttpPost]
        public ActionResult Signup(User post)
        {
            user.name = post.name;
            user.email = post.email;
            user.passw = post.passw;
            bool ok = user.Insert();
            if (IsError()) return ErrorActionResult;
            if (!ok)
            {
                ViewBag.error = "This email already taken";
                return View(user);
            }
            return RedirectToAction("Index", "Login");
        }

        public bool IsError()
        {
            if (sql.IsError())
            {
                ViewBag.error = sql.error;
                ViewBag.query = sql.query;
                ErrorActionResult = View("~/Views/Error.cshtml");
                return true;
            }
            if (user.IsError())
            {
                ViewBag.error =
                    user.error_text + "<br/>" +
                    user.error_excp + "<br/>" +
                    user.error_meth + "<br/>";
                ViewBag.query = sql.query;
                ErrorActionResult = View("~/Views/Error.cshtml");
                return true;
            }
            return false;
        }
    }
}