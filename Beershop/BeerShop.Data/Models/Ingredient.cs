using System;
using System.Collections.Generic;
using System.Text;
using BeerShop.Data.Models.Mapping;

namespace BeerShop.Data.Models
{
 public  class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BeerIngredient> Beers { get; set; }
    }
}
