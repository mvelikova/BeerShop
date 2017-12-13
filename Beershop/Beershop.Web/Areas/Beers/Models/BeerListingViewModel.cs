using BeerShop.Data.Models;
using BeerShop.Web.Infrastructure.Mapping;

namespace BeerShop.Web.Areas.Beers.Models
{
    public class BeerListingViewModel : IMapFrom<Beer>
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public string Price { get; set; }
    }
}