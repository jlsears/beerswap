﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Beerswap.Models;


namespace Beerswaptests.Models
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

        [TestMethod]
        public void BeerPostingEnsureCanAssignNote()
        {
            BeerPosting havebeer = new BeerPosting { Note = "Sample note offering beers here." };
            string expected = "Sample note offering beers here.";
            string actual = havebeer.Note;
            Assert.AreEqual(expected, actual);
        }
    }
}
