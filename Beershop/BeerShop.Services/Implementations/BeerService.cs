using System;
using System.Collections.Generic;
using System.Text;
using BeerShop.Data;
using BeerShop.Data.Models;
using BeerShop.Services.Contracts;

namespace BeerShop.Services.Implementations
{
   public class BeerService:GenericDataService<Beer>,IBeerService
    {
        public BeerService(BeerShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}
