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
        [TestMethod]
        public void BPRepositoryEnsureICanCreateInstance()
        {
            BeerPostRepository brewing = new BeerPostRepository();
            Assert.IsNotNull(brewing);
        }
    }
}
