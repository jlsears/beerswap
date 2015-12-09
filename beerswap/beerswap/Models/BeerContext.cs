using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Beerswap.Models;

namespace Beerswap.Models
{
    // Main class generating/coordinating EF functionality
    // Aka the data access class
    public class BeerContext : ApplicationDbContext
    {
        public virtual IDbSet<BeerPosting> BeerPostings { get; set; }
        public virtual IDbSet<Swap> Swaps { get; set; }
    }
}