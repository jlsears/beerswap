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
    public class PostingController : Controller
    {
        private BeerPostRepository hopCentral;

        public PostingController()
        {
            hopCentral = new BeerPostRepository();
        }

        public PostingController(BeerPostRepository hoppy)
        {
            hopCentral = hoppy;
        }


        // GET: Posting
        public ActionResult Index()
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser brewLover = hopCentral.Users.FirstOrDefault(u => u.Id == user_id);

            List<BeerPosting> whatsAvailable = hopCentral.GetAllPostings();
            return View(whatsAvailable);
        }

        public ActionResult SwapView()
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser brewLover = hopCentral.Users.FirstOrDefault(u => u.Id == user_id);

            List<Swap> theirSwaps = hopCentral.GetSwapsForUser(brewLover);
            return View(theirSwaps);

        }

        public ActionResult PostHistoryView()
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser brewLover = hopCentral.Users.FirstOrDefault(u => u.Id == user_id);

            List<BeerPosting> theirPostHistory = hopCentral.GetBeerPostings(brewLover);
            return View(theirPostHistory);

        }


        // GET: Posting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateBeerPosting(FormCollection form)
        {
            UserManager<ApplicationUser> manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //ApplicationUser brewLover = manager.FindById(User.Identity.GetUserId());

            string beer_name = form.Get("beerposting-name");
            int beer_qty = Convert.ToInt32(form.Get("beerposting-quantity"));
            string beer_note = form.Get("beerposting-note");
            string user_id = User.Identity.GetUserId();
            ApplicationUser brewLover = hopCentral.Users.FirstOrDefault(u => u.Id == user_id);
            string brewLoverId = brewLover.Id;

            hopCentral.CreatePosting(brewLover, new BeerPosting { OwnerId = brewLoverId, BeerName = beer_name, Quantity = beer_qty, Note = beer_note });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateBeerPosting(FormCollection form)
        {
            UserManager<ApplicationUser> manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            int beer_posting_id = Convert.ToInt32(form.Get("post-id"));
            string beer_name = form.Get("beerposting-name");
            int beer_qty = Convert.ToInt32(form.Get("beerposting-quantity"));
            string beer_note = form.Get("beerposting-note");

            hopCentral.EditBeerPosting(beer_posting_id, beer_name, beer_qty, beer_note);

            return RedirectToAction("PostHistoryView");
        }

        [HttpGet]
        public ActionResult AcceptSwapOffer(string _swapid)
        {
            UserManager<ApplicationUser> manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            int swap_id = Convert.ToInt32(_swapid);
            //int swap_id = Convert.ToInt32(form.Get("swap-id"));

            hopCentral.EditSwapAcceptanceStatus(swap_id);

            return RedirectToAction("SwapView");
        }


        [HttpGet]
        public ActionResult DeleteBeerPosting(BeerPosting _specificPosting)
        {
            hopCentral.RemoveBeerPosting(_specificPosting);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateSwapOffer(FormCollection form)
        {
            string beer_name_offered = form.Get("swap-beer-name");
            string posting_id_string = form.Get("post-id");
            int beer_posting_id = Convert.ToInt32(form.Get("post-id"));
            string posting_user = form.Get("posting-user-id");
            int beer_qty_offered = Convert.ToInt32(form.Get("swap-quantity-offered"));
            int beer_qty_wanted = Convert.ToInt32(form.Get("swap-quantity-wanted"));
            string user_id = User.Identity.GetUserId();
            ApplicationUser brewLover = hopCentral.Users.FirstOrDefault(u => u.Id == user_id);
            string brewLoverId = brewLover.Id;

            if (posting_id_string != null)
            {
                hopCentral.AddSwap( beer_posting_id, new Swap { OwnerId = posting_user, OfferUserId = brewLoverId, BeerOffered = beer_name_offered, QtyOffered = beer_qty_offered, SwapDate = DateTime.Now, QtyWanted = beer_qty_wanted, BeerPostingID = beer_posting_id });
            }

            return RedirectToAction("SwapView");
        }


        // GET: Posting/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posting/Create
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

        // GET: Posting/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Posting/Edit/5
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

        // GET: Posting/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Posting/Delete/5
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
