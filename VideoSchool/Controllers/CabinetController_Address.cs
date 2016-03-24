using System;
using System.Web.Mvc;
using VideoSchool.Models.Units;

namespace VideoSchool.Controllers
{
    public partial class CabinetController : Controller
    {
        /// <summary>
        /// Load editable address form for specified user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddressFor(string edit = "")
        {
            try
            {
                Init("cabinet_addressfor");
                string id = GetRouteID();
                if (id == "")
                    return RedirectToAction("User", "Cabinet");
                UserAddress userAddress = new UserAddress(shared);
               
                userAddress.Select(id);
                if (edit == "ModeEdit")
                    return View("Address", userAddress);
                return View("AddressList", userAddress);
               // return View("~/View/Shared/_Menu.cshtml", userAddress);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Save posted address data for specified user
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddressFor(UserAddress post)
        {
            Init("cabinet_addressfor");
            if (id == "")
                return RedirectToAction("User", "Cabinet");
            return SaveAddress(post, id);
        }


        /// <summary>
        /// Load editable address for current user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Address()
        {
            try
            {
                Init("cabinet_address");
                if (currUserId == "")
                    return RedirectToAction("Index", "Login");
                UserAddress userAddress = new UserAddress(shared);
                userAddress.Select(currUserId);
                return View(userAddress);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Save posted address data for current user
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Address(UserAddress post)
        {
            Init("cabinet_address");
            if (currUserId == "")
                return RedirectToAction("Index", "Login");
            return SaveAddress(post, currUserId);
        }


        /// <summary>
        /// Just saving address for user id
        /// </summary>
        /// <param name="post">post address data</param>
        /// <param name="id">user id</param>
        /// <returns></returns>
        private ActionResult SaveAddress(UserAddress post, string id)
        {
            try
            {
                UserAddress userAddress = new UserAddress(shared);
                userAddress.id = id;
                userAddress.Copy(post);
                userAddress.Update();
                if (shared.error.AnyError())
                {
                    ViewBag.error = shared.error.text;
                    return View("AddressList", post);
                }
                ViewBag.success = "OK";
                return View("AddressList", userAddress);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }
    }
}