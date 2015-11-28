using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Beerswap.Models;


namespace beerswaptests.Models
{
    [TestClass]
    public class BeerPostingsTests
    {
        [TestMethod]
        public void BeerPostingEnsureICanCreateInstance()
        {
            BeerPosting havebeer = new BeerPosting();
            Assert.IsNotNull(havebeer);
        }

        [TestMethod]
        public void BeerPostingEnsureCanAssignBeerName()
        {
            BeerPosting havebeer = new BeerPosting { BeerName = "Sam Adam's Summer Ale"};
            string expected = "Sam Adam's Summer Ale";
            string actual = havebeer.BeerName;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BeerPostingEnsureCanAssignBeerQuantity()
        {
            BeerPosting havebeer = new BeerPosting { Quantity = 5 };
            int expected = 5;
            int actual = havebeer.Quantity;
            Assert.AreEqual(expected, actual);
        }
    }
}
