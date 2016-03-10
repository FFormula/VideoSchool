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
        public ActionResult Users()
        {
            User user = new User(shared);

            return View();
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
    }
}