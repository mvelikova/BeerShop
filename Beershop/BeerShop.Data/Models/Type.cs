using System.Collections.Generic;
using BeerShop.Data.Models.Mapping;

namespace BeerShop.Data.Models
{
  public  class Type
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public ICollection<BeerType> Beers { get; set; }
    }
}
