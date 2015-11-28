using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Beerswap.Models;

namespace beerswaptests.Models
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
        public void SwapEnsureICanAssignQtyOffered()
        {
            Swap swapping = new Swap();
            swapping.QtyOffered = 3;
            int expected = 3;
            int actual = swapping.QtyOffered;
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
    }
}
