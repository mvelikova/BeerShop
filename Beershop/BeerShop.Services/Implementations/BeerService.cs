using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeerShop.Data;
using BeerShop.Data.Models;
using BeerShop.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BeerShop.Services.Implementations
{
   public class BeerService:GenericDataService<Beer>,IBeerService
   {
       private readonly BeerShopDbContext db;
        public BeerService(BeerShopDbContext dbContext) : base(dbContext)
        {
            db = dbContext;
        }

        public List<Beer> GetAllWithComments()
        {
            return db.Beers.Include(b => b.Comments).ThenInclude(c => c.User).ToList();
        }

       public Beer GetSignleWithComments(int? id)
       {
           return db.Beers.Where(b => b.Id == id).Include(b => b.Comments).ThenInclude(c => c.User).FirstOrDefault();
       }
    }
}
