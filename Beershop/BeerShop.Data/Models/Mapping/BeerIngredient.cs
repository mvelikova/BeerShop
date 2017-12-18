using System;
using System.Collections.Generic;
using System.Text;

namespace BeerShop.Data.Models.Mapping
{
  public  class BeerIngredient
    {
        public int BeerId { get; set; }
        public Beer Beer { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
