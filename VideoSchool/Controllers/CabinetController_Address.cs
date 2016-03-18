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
        public ActionResult Address()
        {
            try
            {
                string id;
                if (RouteData.Values["id"] == null)
                    return RedirectToAction("UserList", "Cabinet");
                id = RouteData.Values["id"].ToString();
                UserAddress userAddress = new UserAddress(shared);
                userAddress.Select(id);
                return View("Address", userAddress);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }

        /// <summary>
        /// Load editable address for current user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MyAddress()
        {
            try
            {
                Init();
                if (currUserId == "")
                    return RedirectToAction("Index", "Cabinet");
                UserAddress userAddress = new UserAddress(shared);
                userAddress.Select(currUserId);
                return View("Address", userAddress);
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
        public ActionResult Address(UserAddress post)
        {
            string id;
            if (RouteData.Values["id"] == null)
                return RedirectToAction("UserList", "Cabinet");
            id = RouteData.Values["id"].ToString();
            return SaveAddress(post, id);
        }

        /// <summary>
        /// Save posted address data for current user
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MyAddress(UserAddress post)
        {
            Init();
            if (currUserId == "")
                return RedirectToAction("Index", "Cabinet");
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
                userAddress.zip = post.zip;
                userAddress.area = post.area;
                userAddress.city = post.city;
                userAddress.street = post.street;
                userAddress.country = post.country;
                userAddress.personal = post.personal;
                userAddress.Update();
                if (shared.error.AnyError())
                {
                    ViewBag.error = shared.error.text;
                    return View("Address", post);
                }
                ViewBag.success = "OK";
                return View("Address", post);
            }
            catch (Exception ex)
            {
                return ShowError(ex);
            }
        }
    }
}