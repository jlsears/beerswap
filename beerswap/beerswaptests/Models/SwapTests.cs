using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Beerswap.Models;

namespace Beerswaptests.Models
{
    [TestClass]
    public class SwapTests
    {
        [TestMethod]
        public void SwapEnsureICanCreateInstance()
        {
            Swap swapping = new Swap();
            Assert.IsNotNull(swapping);
        }

        [TestMethod]
        public void SwapEnsureICanAssignQtyAvailable()
        {
            Swap swapping = new Swap();
            swapping.QtyOffered = 3;
            int expected = 3;
            int actual = swapping.QtyOffered;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SwapEnsureICanAssignQtyWanted()
        {
            Swap swapping = new Swap();
            swapping.QtyWanted = 2;
            int expected = 2;
            int actual = swapping.QtyWanted;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SwapEnsureCanAssignAcceptSwapStatus()
        {
            Swap swapping = new Swap();
            swapping.AcceptSwap = true;
            bool expected = true;
            bool actual = swapping.AcceptSwap;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SwapEnsureICanAssignBeerNameOffered()
        {
            Swap swapping = new Swap();
            swapping.BeerOffered = "Dos Perros";
            string expected = "Dos Perros";
            string actual = swapping.BeerOffered;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SwapEnsureICanAssignBeerNameAvailable()
        {
            Swap swapping = new Swap();
            swapping.BeerName = "Pale Ale";
            string expected = "Pale Ale";
            string actual = swapping.BeerName;
            Assert.AreEqual(expected, actual);
        }

    }
}
