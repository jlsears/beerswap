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
using System.Text.RegularExpressions;

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

            string beer_name = form.Get("beerposting-name");
            int beer_qty;
            Regex rx = new Regex("[0-9]+$");

            if ((rx.IsMatch(form.Get("beerposting-quantity"))) && (!form.Get("beerposting-quantity").Contains("-")))
            {
                beer_qty = Convert.ToInt32(form.Get("beerposting-quantity"));
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("That's not a valid number. Please try again.");
                return RedirectToAction("Index");
            }
           
            string beer_note = form.Get("beerposting-note");
            string user_id = User.Identity.GetUserId();
            string user_name = User.Identity.GetUserName();
            ApplicationUser brewLover = hopCentral.Users.FirstOrDefault(u => u.Id == user_id);
            string brewLoverId = brewLover.Id;

            hopCentral.CreatePosting(brewLover, new BeerPosting { OwnerName = user_name, OwnerId = brewLoverId, BeerName = beer_name, Quantity = beer_qty, Note = beer_note });

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
        public ActionResult AcceptSwapOffer(int id)
        {
            UserManager<ApplicationUser> manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            hopCentral.EditSwapAcceptanceStatus(id);
            hopCentral.EditPostingAcceptanceStatus(id);

            return RedirectToAction("SwapView");
        }


        [HttpGet]
        public ActionResult DeleteBeerPosting(int id)
        {
            hopCentral.RemoveBeerPosting(id);
            return RedirectToAction("PostHistoryView");
        }

        [HttpPost]
        public ActionResult CreateSwapOffer(FormCollection form)
        {
            string beer_name_offered = form.Get("swap-beer-name");
            string original_beer = form.Get("original-beer");
            string posting_id_string = form.Get("post-id");
            int beer_posting_id = Convert.ToInt32(form.Get("post-id"));
            string posting_user = form.Get("posting-user-id");
            int beer_qty_offered;
            int beer_qty_wanted;

            Regex rx = new Regex("[0-9]+$");

            if ((rx.IsMatch(form.Get("swap-quantity-offered"))) && (!form.Get("swap-quantity-offered").Contains("-")))
            {
                beer_qty_offered = Convert.ToInt32(form.Get("swap-quantity-offered"));
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("That's not a valid number. Please try again.");
                return RedirectToAction("Index");
            }

            if ((rx.IsMatch(form.Get("swap-quantity-wanted"))) && (!form.Get("swap-quantity-wanted").Contains("-")))
            {
                beer_qty_wanted = Convert.ToInt32(form.Get("swap-quantity-wanted"));
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("That's not a valid number. Please try again.");
                return RedirectToAction("Index");
            }

            string swap_note = form.Get("swap-note");
            string user_id = User.Identity.GetUserId();
            ApplicationUser brewLover = hopCentral.Users.FirstOrDefault(u => u.Id == user_id);
            string brewLoverId = brewLover.Id;

            if (posting_id_string != null)
            {
                hopCentral.AddSwap( beer_posting_id, new Swap { OwnerId = posting_user, OfferUserId = brewLoverId, SwapNote = swap_note, BeerOffered = beer_name_offered, QtyOffered = beer_qty_offered, SwapDate = DateTime.Now, QtyWanted = beer_qty_wanted, BeerPostingID = beer_posting_id, BeerName = original_beer });
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
