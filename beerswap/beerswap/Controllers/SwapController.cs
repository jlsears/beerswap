using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Beerswap.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Mvc.Ajax;

namespace Beerswap.Controllers
{
    public class SwapController : Controller
    {
        private BeerPostRepository hopCentral;

        public SwapController()
        {
            hopCentral = new BeerPostRepository();
        }

        public SwapController(BeerPostRepository hoppy)
        {
            hopCentral = hoppy;
        }


        // GET: Swap
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult SwapView()
        //{
        //    string user_id = User.Identity.GetUserId();
        //    ApplicationUser brewLover = hopCentral.Users.FirstOrDefault(u => u.Id == user_id);

        //    List<Swap> theirSwaps = hopCentral.GetSwapsForUser(brewLover);
        //    return View(theirSwaps);

        //}

        [HttpPost]
        public ActionResult AcceptSwapOffer(int _swapid)
        {
            UserManager<ApplicationUser> manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            int swap_id = _swapid;
            //int swap_id = Convert.ToInt32(form.Get("swap-id"));

            hopCentral.EditSwapAcceptanceStatus(swap_id);

            return RedirectToAction("SwapView");
        }

        // GET: Swap/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Swap/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Swap/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Swap/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Swap/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Swap/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Swap/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
