using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beerswap.Models
{
    public class Swap
    {
        public int SwapId { get; set; }
        public ApplicationUser OfferUser { get; set; }
        public string BeerName { get; set; }
        public DateTime SwapDate { get; set; }
        public int BeerPostingID { get; set; }
        public bool AcceptSwap { get; set; }
        public int QtyWanted { get; set; }
        public int QtyOffered { get; set; }
        public string BeerOffered { get; set; }
    }
}