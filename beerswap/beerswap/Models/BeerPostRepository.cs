﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beerswap.Models
{
    public class BeerPostRepository
    {
        private BeerContext context;

        public BeerPostRepository()
        {
            context = new BeerContext();
        }
    }
}