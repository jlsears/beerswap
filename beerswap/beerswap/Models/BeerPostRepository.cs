﻿using Beerswap.Models;
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


        // ************************
        // Beer Postings Methods
        // ************************


        public BeerPosting CreatePosting(string beername, ApplicationUser owner)
        {
            BeerPosting drinkup = new BeerPosting { BeerName = beername, Owner = owner };
            context.BeerPostings.Add(drinkup);
            context.SaveChanges();

            return drinkup;
        }

        public List<BeerPosting> GetBeerPostings(ApplicationUser specificUser)
        {
            var query = from b in context.BeerPostings where b.Owner == specificUser select b;
            return query.ToList<BeerPosting>();
        }

        public int GetBeerPostingCount()
        {
            var query = from p in context.BeerPostings select p;
            return query.Count();
        }

        public List<BeerPosting> GetAllPostings()
        {
            return context.BeerPostings.ToList();
        }
    }
}