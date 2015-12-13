using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Beerswap.Models;

namespace beerswap.Controllers
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
