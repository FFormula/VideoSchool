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

        public LoginController ()
        {
            sql = new MySQL();
            user = new User(sql);
        }

        [HttpGet]
        public ActionResult Index()
        {
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
            }
            else
            {

            }
            return View(user);
        }


    }
}