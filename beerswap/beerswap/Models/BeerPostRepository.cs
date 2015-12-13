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
            //BeerPosting drinkup = new BeerPosting { Owner = owner };
            context.BeerPostings.Add(_newbeerposting);
            context.SaveChanges();

            return _newbeerposting;
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

        public BeerPosting GetPostingById (int posting_id)
        {
            var query = from p in context.BeerPostings where p.BeerPostingID == posting_id select p;
            return query.Single<BeerPosting>();
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

        public List<Swap> GetAllSwaps()
        {
            var query = from s in context.BeerPostings select s;
            return query.SelectMany(beerposting => beerposting.Swaps).ToList();
        }

        public Swap GetSwapById(int _beerpostid, int _swapid)
        {
            var query = from s in context.BeerPostings where s.BeerPostingID == _beerpostid select s;
            //return query.Select(beerposting => beerposting.Swaps).ToList();
            //BeerPosting found_post = query.Single<BeerPosting>();
            //Swap query2 = from y in context.Swaps where y.SwapId == _swapid select y;
            BeerPosting foundit = query.Single<BeerPosting>();
            var query2 = from x in foundit.Swaps where x.SwapId == _swapid select x;
            Swap foundagain = query2.Single<Swap>();
            return foundagain;

            //var query = from s in context.Swaps where s.BeerPostingID == _beerpostid select s;
            //BeerPosting found_post = query.Single<Swap>();

            //return query.ToList();
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

        public bool EditSwapAcceptanceStatus(int _postid, Swap _swap, bool _swapacceptance)
        {
            var query = from p in context.BeerPostings where p.BeerPostingID == _postid select p;
            BeerPosting found_post = null;
            bool result = true;

            try
            {
                found_post = query.Single<BeerPosting>();
                _swap.AcceptSwap = _swapacceptance;
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