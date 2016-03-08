using Beerswap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Beerswap.Models
{
    // Here we handle our CRUD methods
    public class BeerPostRepository
    {
        private BeerContext context;

        public IDbSet<ApplicationUser> Users { get { return context.Users; } }

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


        public BeerPosting CreatePosting(ApplicationUser owner, BeerPosting _newbeerposting)
        {
            context.BeerPostings.Add(_newbeerposting);
            context.SaveChanges();

            return _newbeerposting;
        }

        public List<BeerPosting> GetBeerPostings(ApplicationUser specificUser)
        {
            var query = from b in context.BeerPostings where b.OwnerId == specificUser.Id select b;
            return query.ToList<BeerPosting>();
        }

        public int GetBeerPostingCount(string userid)
        {
            var query = from b in context.BeerPostings where b.OwnerId == userid select b;
            return query.Count();
        }

        public int GetBeerPostingCount()
        {
            var query = from p in context.BeerPostings select p;
            return query.Count();
        }

        public List<BeerPosting> GetAllPostings()
        {
            var query = from b in context.BeerPostings where b.PostingAccepted == false select b;
            return query.ToList<BeerPosting>();
            //return context.BeerPostings.ToList();
        }

        public bool RemoveBeerPosting(int _beerpostingid)
        {
            var query = from b in context.BeerPostings where b.BeerPostingID == _beerpostingid select b;
            BeerPosting found_post = null;
            bool result = true;

            try
            {
                found_post = query.Single<BeerPosting>();
                context.BeerPostings.Remove(found_post);
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

        public BeerPosting GetPostingById (int posting_id)
        {
            var query = from p in context.BeerPostings where p.BeerPostingID == posting_id select p;
            return query.Single<BeerPosting>();
        }


        public bool EditBeerPosting(int _postId, string newName, int newQuantity, string newNote)
        {
            var query = from p in context.BeerPostings where p.BeerPostingID == _postId select p;
            BeerPosting found_post = null;
            bool result = true;

            try
            {
                found_post = query.Single<BeerPosting>();
                found_post.BeerName = newName;
                found_post.Quantity = newQuantity;
                found_post.Note = newNote;
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

        public Swap CreateSwap(string _specificUser)
        {
            Swap make_swap = new Swap { OfferUserId = _specificUser };
            context.Swaps.Add(make_swap);
            context.SaveChanges();

            return make_swap;
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

        public List<Swap> GetAllSwaps()
        {
            var query = from s in context.BeerPostings select s;
            return query.SelectMany(beerposting => beerposting.Swaps).ToList();
        }

        public List<Swap> GetSwapsForUser(ApplicationUser specificUser)
        {
            var query = from s in context.Swaps where s.OfferUserId == specificUser.Id || s.OwnerId == specificUser.Id select s;

            return query.ToList<Swap>();
        }

        public Swap GetSwapById(int _beerpostid, int _swapid)
        {
            var query = from s in context.BeerPostings where s.BeerPostingID == _beerpostid select s;
            BeerPosting foundit = query.Single<BeerPosting>();
            var query2 = from x in foundit.Swaps where x.SwapId == _swapid select x;
            Swap foundagain = query2.Single<Swap>();
            return foundagain;
        }

        public bool EditBeerOfferedName(int _postid, Swap _swap, string _newName)
        {
            var query = from p in context.BeerPostings where p.BeerPostingID == _postid select p;
            BeerPosting found_post = null;
            bool result = true;

            try
            {
                found_post = query.Single<BeerPosting>();
                _swap.BeerOffered = _newName;
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

        public bool EditQtyOffered(int _postid, Swap _swap, int _qtyoffered)
        {
            var query = from p in context.BeerPostings where p.BeerPostingID == _postid select p;
            BeerPosting found_post = null;
            bool result = true;

            try
            {
                found_post = query.Single<BeerPosting>();
                _swap.QtyOffered = _qtyoffered;
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

        public bool EditSwapAcceptanceStatus(int _swapid)
        {
            var query = from p in context.Swaps where p.SwapId == _swapid select p;
            Swap found_swap = null;
            bool result = true;

            try
            {
                found_swap = query.Single<Swap>();
                found_swap.AcceptSwap = true;
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

        public bool EditPostingAcceptanceStatus(int _swapid)
        {
            var query1 = from p in context.Swaps where p.SwapId == _swapid select p;
            Swap query1Found = null;
            query1Found = query1.Single<Swap>();

            var query2 = from q in context.BeerPostings where q.BeerPostingID == query1Found.BeerPostingID select q;
            BeerPosting query2Found = null;
            query2Found = query2.Single<BeerPosting>();

            bool result = true;

            try
            {
                query2Found.PostingAccepted = true;
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
    }
}