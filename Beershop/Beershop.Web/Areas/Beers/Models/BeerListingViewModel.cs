using BeerShop.Data.Models;
using BeerShop.Web.Infrastructure.Mapping;

namespace BeerShop.Web.Areas.Beers.Models
{
    public class BeerListingViewModel : IMapFrom<Beer>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string Price { get; set; }

        public string Country { get; set; }
    }
}