using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeerShop.Data;
using BeerShop.Data.Models;
using BeerShop.Data.Models.Mapping;
using BeerShop.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Type = BeerShop.Data.Models.Type;

namespace BeerShop.Services.Implementations
{
   public class BeerService:GenericDataService<Beer>,IBeerService
   {
       private readonly BeerShopDbContext db;
        public BeerService(BeerShopDbContext dbContext) : base(dbContext)
        {
            db = dbContext;
        }

       
       public List<Type> GetAllBeerTypes()
       {
           return db.Types.ToList();
        }

       public List<Ingredient> GetAllBeerIngredients()
       {
           return db.Ingredients.ToList();
       }

       public void AddIngredient(string name)
       {
           db.Ingredients.Add(new Ingredient {Name = name});
       }

       public void AddType(string name)
       {
           db.Types.Add(new Type { Name = name });

        }

        public List<Type> GetBeerTypes(int ?id)
        {
          return db.Beers.Where(b => b.Id == id).FirstOrDefault().Types.Select(bt=>bt.Type).ToList();
           
        }

       public List<Ingredient> GetBeerIngredients(int? id)
       {
           return db.Beers.Where(b => b.Id == id).FirstOrDefault().Ingredients.Select(I => I.Ingredient).ToList();
        }
       public void AddIngredientToBeer(int id,string name)
       {
           var ingredient = db.Ingredients.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());
           if (ingredient!=null)
           {
               var newIngr = new BeerIngredient {BeerId = id ,IngredientId = ingredient.Id};
               var beer = db.Beers.FirstOrDefault(b => b.Id == newIngr.BeerId);
                beer.Ingredients.Add(newIngr);
               db.SaveChanges();
                return;
            }
         
           ingredient = new Ingredient {Name = name};
           db.Ingredients.Add(ingredient);
           db.SaveChanges();
            var bi= new BeerIngredient { BeerId = id, IngredientId = ingredient.Id };
          var beeer= db.Beers.FirstOrDefault(b => b.Id == bi.BeerId);
            beeer.Ingredients.Add(bi);
           db.SaveChanges();

        }

       public void AddTypeToBeer(int id,string name)
       {
           db.Types.Add(new Type { Name = name });

       }
    }
}
