using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Beerswap.Models;

namespace beerswap.Models
{
    public class BeerContext : DbContext
    {
        public virtual IDbSet<BeerPosting> BeerPostings { get; set; }
        public virtual IDbSet<Swap> Swaps { get; set; }
    }
}