using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Data.Entity;
using Beerswap.Models;
using System.Collections.Generic;

namespace beerswaptests.Models
{
    [TestClass]
    public class BeerPostRepositoryTests
    {
        private Mock<BeerContext> mock_context;
        private Mock<DbSet<BeerPosting>> mock_beerpostings;
        private List<BeerPosting> my_beerpostings;
        private ApplicationUser owner, userA, userB;

        private void ConnectMocksToDataSource()
        {
            var data = my_beerpostings.AsQueryable();

            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.Provider).Returns(data.Provider);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock_beerpostings.As<IQueryable<BeerPosting>>().Setup(m => m.Expression).Returns(data.Expression);

            mock_context.Setup(m => m.BeerPostings).Returns(mock_beerpostings.Object);
        }

        [TestInitialize]
        [TestMethod]
        public void Initialize()
        {
            mock_context = new Mock<BeerContext>();
            mock_beerpostings = new Mock<DbSet<BeerPosting>>();
            my_beerpostings = new List<BeerPosting>();
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
            my_beerpostings = null;
        }

        [TestMethod]
        public void BPRepositoryEnsureICanCreateInstance()
        {
            BeerPostRepository brewing = new BeerPostRepository();
            Assert.IsNotNull(brewing);
        }
    }
}
