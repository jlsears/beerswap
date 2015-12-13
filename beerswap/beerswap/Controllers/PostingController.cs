﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Beerswap.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

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

            //ViewBag.CurrentPostId = my_board.PostingId;

            List<BeerPosting> whatsAvailable = hopCentral.GetAllPostings();
            return View(whatsAvailable);
        }

        // GET: Posting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateBeerPosting(FormCollection form)
        {
            string beer_name = form.Get("beerposting-name");
            int beer_qty = Convert.ToInt32(form.Get("beerposting-quantity"));
            //int beer_post_id = Convert.ToInt32(form.Get("post-id"));
            string beer_note = form.Get("beerposting-note");
            string user_id = User.Identity.GetUserId();
            ApplicationUser brewLover = hopCentral.Users.FirstOrDefault(u => u.Id == user_id);

            hopCentral.CreatePosting(brewLover, new BeerPosting { BeerName = beer_name, Quantity = beer_qty, Note = beer_note });

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult CreateSwapOffer(FormCollection form)
        {
            string beer_name_offered = form.Get("swap-beer-name");
            string beer_posting_id = form.Get("post-id");
            string posting_id_string = form.Get("post-id");
            int beer_qty_offered = Convert.ToInt32(form.Get("swap-quantity-offered"));
            int beer_qty_wanted = Convert.ToInt32(form.Get("swap-quantity-wanted"));
            string user_id = User.Identity.GetUserId();
            ApplicationUser brewLover = hopCentral.Users.FirstOrDefault(u => u.Id == user_id);

            //BeerPosting current_posting = hopCentral.GetPostingById(Convert.ToInt32((beer_posting_id)));
            if (beer_posting_id != null)
            {
                hopCentral.AddSwap(Convert.ToInt32(beer_posting_id), new Swap { BeerOffered = beer_name_offered, QtyOffered = beer_qty_offered, QtyWanted = beer_qty_wanted });
            }



            return RedirectToAction("Index");
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
