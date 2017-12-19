using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Beershop.Data.Models;
using BeerShop.Data.Models;
using BeerShop.Data.Models.Mapping;
using BeerShop.Web.Infrastructure.Mapping;

namespace BeerShop.Web.Areas.Beers.Models
{
    public class CreateBeerViewModel:IMapFrom<Beer>
    {
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        

        [Required]
        public string Price { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Ingredients { get; set; }

        [Required]
        public string Country { get; set; }

    }
}
