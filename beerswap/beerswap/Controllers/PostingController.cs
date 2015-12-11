using System;
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
            string list_name = form.Get("list-name");
            string board_id = form.Get("beerposting-id");
            string user_id = User.Identity.GetUserId();
            ApplicationUser brewLover = hopCentral.Users.FirstOrDefault(u => u.Id == user_id);


            hopCentral.CreatePosting(brewLover);


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
