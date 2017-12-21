using System.Collections.Generic;
using BeerShop.Data.Models;
using BeerShop.Data.Models.Mapping;
using BeerShop.Web.Infrastructure.Mapping;

namespace BeerShop.Web.Areas.Beers.Models
{
    public class BeerListingViewModel : IMapFrom<Beer>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<BeerIngredient> Ingredients { get; set; }

        //  public string Type { get; set; }

        public string ImageUrl { get; set; }

        public string Country { get; set; }

        public string UserId { get; set; }

        public ICollection<BeerComment> Comments { get; set; } = new HashSet<BeerComment>();
    }
}