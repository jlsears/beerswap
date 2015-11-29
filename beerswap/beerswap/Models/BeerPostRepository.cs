using Beerswap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beerswap.Models
{
    // Here we handle our CRUD methods
    public class BeerPostRepository
    {
        private BeerContext context;

        // Repository constructor
        public BeerPostRepository()
        {
            context = new BeerContext();
        }

        // Repository constructor with an argument
        public BeerPostRepository(BeerContext _context)
        {
            context = _context;
        }

        public BeerPosting CreatePosting(string beername, ApplicationUser owner)
        {
            BeerPosting drinkup = new BeerPosting { BeerName = beername, Owner = owner };
            context.BeerPostings.Add(drinkup);
            context.SaveChanges();

            return drinkup;
        }
    }
}