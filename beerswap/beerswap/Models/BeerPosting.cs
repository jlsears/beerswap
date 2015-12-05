using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Beerswap.Models
{
    public class BeerPosting
    {
        [Key]
        public int BeerPostingID { get; set; }
        public string BeerName { get; set; }
        public ApplicationUser Owner { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        public virtual List<Swap> Swaps { get; set; }

        public BeerPosting()
        {
            Swaps = new List<Swap>();
        }
    }
}