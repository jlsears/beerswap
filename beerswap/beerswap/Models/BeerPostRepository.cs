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

        public void RemoveBeerPosting(BeerPosting hastalavista)
        {
            context.BeerPostings.Remove(hastalavista);
            context.SaveChanges();
        }

        public bool EditBeerPostingName(int _postId, string newName)
        {
            var query = from p in context.BeerPostings where p.BeerPostingID == _postId select p;
            BeerPosting found_post = null;
            bool result = true;

            try
            {
                found_post = query.Single<BeerPosting>();
                found_post.BeerName = newName;
                context.SaveChanges();
            }
            catch (InvalidOperationException)
            {

                result = false;
            }
            catch (ArgumentNullException)
            {
                result = false;
            }
            return result;
        }

        public bool EditBeerPostingQuantity(int _beerpostid, int _newquantity)
        {
            var query = from p in context.BeerPostings where p.BeerPostingID == _beerpostid select p;
            BeerPosting found_post = null;
            bool result = true;

            try
            {
                found_post = query.Single<BeerPosting>();
                found_post.Quantity = _newquantity;
                context.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                result = false;
            }
            catch (ArgumentNullException)
            {
                result = false;
            }
            return result;
        }

        public bool EditBeerPostingNote(int _beerpostid, string _newnote)
        {
            var query = from p in context.BeerPostings where p.BeerPostingID == _beerpostid select p;
            BeerPosting found_post = null;
            bool result = true;

            try
            {
                found_post = query.Single<BeerPosting>();
                found_post.Note = _newnote;
                context.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                result = false;
            }
            catch (ArgumentNullException)
            {
                result = false;
            }
            return result;
        }


        // ************************
        // Swap Methods
        // ************************

        public int GetSwapCount()
        {
            return GetAllSwaps().Count;
        }

        public bool AddSwap(int _beerpostid, Swap haveSwap)
        {
            var query = from p in context.BeerPostings where p.BeerPostingID == _beerpostid select p;
            BeerPosting foundPosting = null;
            bool result = true;
            try
            {
                foundPosting = query.Single<BeerPosting>();
                foundPosting.Swaps.Add(haveSwap);
                context.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                result = false;
            }
            catch(ArgumentNullException)
            {
                result = false;
            }
            return result;
        }

        //public int GetBeerSwapCount()
        //{
        //    var query = from s in context.Swaps select s;
        //    return query.Count();
        //}

        public List<Swap> GetAllSwaps()
        {
            var query = from s in context.BeerPostings select s;
            return query.SelectMany(beerposting => beerposting.Swaps).ToList();
        }
    }
}