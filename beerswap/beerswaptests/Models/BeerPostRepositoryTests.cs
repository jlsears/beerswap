﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Data.Entity;
using Beerswap.Models;
using System.Collections.Generic;

namespace Beerswaptests.Models
{
    [TestClass]
    public class BeerPostRepositoryTests
    {
        private Mock<BeerContext> mock_context;
        private Mock<DbSet<BeerPosting>> mock_beerpostings;
        private Mock<DbSet<Swap>> mock_swapoffers;
        private List<BeerPosting> my_beerpostings;
        private List<Swap> my_swapoffers;
        private ApplicationUser owner, userA, userB;

        private void ConnectMocksToDataSource()
        {
            var data = my_beerpostings.AsQueryable();

            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.Provider).Returns(data.Provider);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.Expression).Returns(data.Expression);

            mock_context.Setup(m => m.BeerPostings).Returns(mock_beerpostings.Object);

            var swap_data = my_swapoffers.AsQueryable();

            mock_swapoffers.As<IQueryable<Swap>>().Setup(m => m.Provider).Returns(swap_data.Provider);
            mock_swapoffers.As<IQueryable<Swap>>().Setup(m => m.GetEnumerator()).Returns(swap_data.GetEnumerator);
            mock_swapoffers.As<IQueryable<Swap>>().Setup(m => m.ElementType).Returns(swap_data.ElementType);
            mock_swapoffers.As<IQueryable<Swap>>().Setup(m => m.Expression).Returns(swap_data.Expression);

            mock_context.Setup(m => m.Swaps).Returns(mock_swapoffers.Object);
        }

        [TestInitialize]
        [TestMethod]
        public void Initialize()
        {
            mock_context = new Mock<BeerContext>();
            mock_beerpostings = new Mock<DbSet<BeerPosting>>();
            mock_swapoffers = new Mock<DbSet<Swap>>();
            my_beerpostings = new List<BeerPosting>();
            my_swapoffers = new List<Swap>();
            owner = new ApplicationUser();
            userA = new ApplicationUser();
            userB = new ApplicationUser();

        }

        [TestCleanup]
        [TestMethod]
        public void Cleanup()
        {
            mock_context = null;
            mock_beerpostings = null;
            mock_swapoffers = null;
            my_beerpostings = null;
            my_swapoffers = null;
        }

        [TestMethod]
        public void BPRepositoryEnsureICanCreateInstance()
        {
            BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);
            Assert.IsNotNull(brewing);
        }

        // ****************************
        // Beer Posting Tests
        // ****************************

        //[TestMethod]
        //public void BPRepositoryEnsureICanGetBeerPosting()
        //{
        //    my_beerpostings.Add(new BeerPosting { BeerPostingID = 1, BeerName = "Swill", OwnerId = "1" });
        //    my_beerpostings.Add(new BeerPosting { BeerPostingID = 2, BeerName = "Different Swill", OwnerId = "1" });
        //    ConnectMocksToDataSource();

        //    BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);

        //    List<BeerPosting> getbeers = brewing.GetBeerPostings(userA);

        //    int expected = 2;
        //    //int actual = getbeers;
        //    Assert.AreEqual(expected, getbeers.Count());
        //}

        [TestMethod]
        public void BPRepositoryEnsureICanGetAllBeerPostings()
        {
            my_beerpostings.Add(new BeerPosting { BeerName = "Something sour", Quantity = 5, OwnerId = "1" });
            my_beerpostings.Add(new BeerPosting { BeerName = "Something lemon", Quantity = 3, OwnerId = "2" });

            ConnectMocksToDataSource();
            BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);

            List<BeerPosting> thesePosts = brewing.GetAllPostings();
            Assert.AreEqual(2, brewing.GetBeerPostingCount());
        }

        [TestMethod]
        public void BPRepositoryEnsureICanGetBeerPostingCount()
        {
            var data = my_beerpostings.AsQueryable();

            BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);

            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.Provider).Returns(data.Provider);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.Expression).Returns(data.Expression);

            mock_context.Setup(m => m.BeerPostings).Returns(mock_beerpostings.Object);

            int actual = brewing.GetBeerPostingCount();

            my_beerpostings.Add(new BeerPosting { BeerPostingID = 1, BeerName = "Swill" });
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(b => b.GetEnumerator()).Returns(data.GetEnumerator());

            Assert.AreEqual(1, brewing.GetBeerPostingCount());

        }

        [TestMethod]
        public void BPRepositoryEnsureICanGetBeerPostingCountforSpecificUser()
        {
            var data = my_beerpostings.AsQueryable();

            BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);

            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.Provider).Returns(data.Provider);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.Expression).Returns(data.Expression);

            mock_context.Setup(m => m.BeerPostings).Returns(mock_beerpostings.Object);

            my_beerpostings.Add(new BeerPosting { BeerPostingID = 2, BeerName = "Swill", OwnerId = "2" });
            my_beerpostings.Add(new BeerPosting { BeerPostingID = 3, BeerName = "More Swill", OwnerId = "2" });
            my_beerpostings.Add(new BeerPosting { BeerPostingID = 4, BeerName = "Something Else", OwnerId = "1" });
            my_beerpostings.Add(new BeerPosting { BeerPostingID = 5, BeerName = "Different", OwnerId = "2" });
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(b => b.GetEnumerator()).Returns(data.GetEnumerator());

            Assert.AreEqual(3, brewing.GetBeerPostingCount("2"));

        }

        //[TestMethod]
        //public void BPRepositoryEnsureICanDeleteABeerPosting()
        //{
        //    ConnectMocksToDataSource();

        //    BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);

        //    BeerPosting added_posting = brewing.CreatePosting("Swill", userA);

        //    brewing.RemoveBeerPosting(added_posting);
        //    Assert.AreEqual(0, brewing.GetBeerPostingCount());
        //}

        [TestMethod]
        public void BPRepositoryEnsureICanEditABeerPostingName()
        {
            BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);
            ConnectMocksToDataSource();

            BeerPosting added_posting = new BeerPosting { BeerPostingID = 1, BeerName = "Swill", OwnerId = "1" };
            my_beerpostings.Add(added_posting);

            bool editing = brewing.EditBeerPostingName(1, "Other Swill");

            string actual = "Other Swill";

            Assert.AreEqual(actual, added_posting.BeerName);
            Assert.IsTrue(editing);
        }

        [TestMethod]
        public void BPRepositoryEnsureICanEditABeerPostingQuantity()
        {
            BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);
            ConnectMocksToDataSource();

            BeerPosting added_posting = new BeerPosting { BeerPostingID = 1, BeerName = "Swill", Quantity = 5 };
            my_beerpostings.Add(added_posting);

            bool editing = brewing.EditBeerPostingQuantity(1, 3);

            int actual = 3;

            Assert.AreEqual(actual, added_posting.Quantity);
            Assert.IsTrue(editing);
        }

        [TestMethod]
        public void BPRepositoryEnsureICanEditABeerPostingNote()
        {
            BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);
            ConnectMocksToDataSource();

            BeerPosting added_posting = new BeerPosting { BeerPostingID = 1, BeerName = "Swill", Note = "I don't like this beer" };
            my_beerpostings.Add(added_posting);

            bool editing = brewing.EditBeerPostingNote(1, "I mean I really, really don't like this beer");

            string actual = "I mean I really, really don't like this beer";

            Assert.AreEqual(actual, added_posting.Note);
            Assert.IsTrue(editing);
        }


        // ****************************
        // Swap Tests
        // ****************************

        [TestMethod]
        public void BPRepositoryEnsureICanAddSwapToPosting()
        {
            BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);
            Swap swapping = new Swap { SwapId = 1, BeerOffered = "Dos Perros" };
            my_beerpostings.Add(new BeerPosting { BeerPostingID = 1, BeerName = "Swill" });

            ConnectMocksToDataSource();

            bool actual = brewing.AddSwap(1, swapping);

            Assert.AreEqual(1, brewing.GetSwapCount());
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void BPRepositoryEnsureICanGetAllSwaps()
        {
            var swaps_here = new List<Swap> { new Swap { BeerName = "Good Beer", SwapId = 1 } };

            my_beerpostings.Add(new BeerPosting { BeerName = "pale ale", OwnerId = "1", BeerPostingID = 1, Swaps = swaps_here });
            my_beerpostings.Add(new BeerPosting { BeerName = "amber", OwnerId = "2", BeerPostingID = 2, Swaps = swaps_here });
            ConnectMocksToDataSource();
            BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);

            int expected = 2;
            int actual = brewing.GetAllSwaps().Count;

            Assert.AreEqual(expected, actual);
        }

        //[TestMethod]
        //public void BPRepositoryEnsureICanGetSwapsByUser()
        //{
        //    my_swapoffers.Add(new Swap { BeerName = "Something sour", QtyOffered = 5, OfferUserId = "1" });
        //    my_swapoffers.Add(new Swap { BeerName = "Something lemony", QtyOffered = 3, OfferUserId = "2" });

        //    ConnectMocksToDataSource();
        //    BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);

        //    int expected = 1;
        //    List<Swap> actual_swaps = brewing.GetSwapsForUser(userA);

        //    Assert.AreEqual(expected, actual_swaps.Count);
        //}

        //[TestMethod]
        //public void BPRepositoryEnsureICanGetSpecificSwap()
        //{
        //    var swaps_here = new Swap { BeerName = "Good Beer", SwapId = 1 };
        //    var swaps_again = new Swap { BeerName = "Good Beer", SwapId = 2 };

        //    my_beerpostings.Add(new BeerPosting { BeerName = "pale ale", Owner = userA, BeerPostingID = 1 });
        //    my_beerpostings.Add(new BeerPosting { BeerName = "amber", Owner = userB, BeerPostingID = 2 });
        //    ConnectMocksToDataSource();
        //    BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);

        //    int expected = 1;
        //    brewing.AddSwap(1, swaps_here);
        //    int actual = brewing.GetSwapById(1, 1).

        //    Assert.AreEqual(expected, actual);
        //}

        [TestMethod]
        public void BPRepositoryEnsureICanEditBeerOfferedName()
        {
            BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);
            Swap swapping = new Swap { SwapId = 1, BeerOffered = "generic beer" };
            my_beerpostings.Add(new BeerPosting { BeerPostingID = 1, BeerName = "Swill", OwnerId = "1" });

            ConnectMocksToDataSource();
            bool nameChange = brewing.AddSwap(1, swapping);

            brewing.EditBeerOfferedName(1, swapping, "Dos Perros");

            string actual = "Dos Perros";

            Assert.AreEqual(actual, swapping.BeerOffered);
            Assert.IsTrue(nameChange);
        }

        [TestMethod]
        public void BPRepositoryEnsureICanEditBeerOfferedQuantity()
        {
            BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);
            Swap swapping = new Swap { SwapId = 1, QtyOffered = 4 };
            my_beerpostings.Add(new BeerPosting { BeerPostingID = 1, BeerName = "Swill", OwnerId = "1" });

            ConnectMocksToDataSource();
            bool adding = brewing.AddSwap(1, swapping);

            brewing.EditQtyOffered(1, swapping, 3);

            int actual = 3;

            Assert.AreEqual(actual, swapping.QtyOffered);
            Assert.IsTrue(adding);
        }

        //[TestMethod]
        //public void BPRepositoryEnsureICanEditSwapAcceptedStatus()
        //{
        //    BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);
        //    Swap swapping = new Swap { SwapId = 1, AcceptSwap = false };
        //    my_beerpostings.Add(new BeerPosting { BeerPostingID = 1, BeerName = "Swill", OwnerId = "1" });

        //    ConnectMocksToDataSource();
        //    bool adding = brewing.AddSwap(1, swapping);

        //    brewing.EditSwapAcceptanceStatus(1);

        //    bool actual = true;

        //    Assert.AreEqual(actual, swapping.AcceptSwap);
        //    Assert.IsTrue(adding);
        //}

        [TestMethod]
        public void BPRepositoryEnsureICanGetSwapCount()
        {
            var data = my_beerpostings.AsQueryable();

            BeerPostRepository brewing = new BeerPostRepository(mock_context.Object);

            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.Provider).Returns(data.Provider);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.Expression).Returns(data.Expression);

            mock_context.Setup(m => m.BeerPostings).Returns(mock_beerpostings.Object);

            //int actual = brewing.GetBeerPostingCount();

            my_beerpostings.Add(new BeerPosting { BeerPostingID = 1, BeerName = "Swill" });
            Swap swapping = new Swap { SwapId = 1, BeerOffered = "Dos Perros" };
            brewing.AddSwap(1, swapping);

            int actual = brewing.GetSwapCount();

            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(b => b.GetEnumerator()).Returns(data.GetEnumerator());
            Assert.AreEqual(1, brewing.GetSwapCount());
        }
    }
}

