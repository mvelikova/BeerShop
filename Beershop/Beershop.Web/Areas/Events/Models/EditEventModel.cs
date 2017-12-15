using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BeerShop.Data.Models;
using BeerShop.Web.Infrastructure.Mapping;

namespace BeerShop.Web.Areas.Events.Models
{
    public class EditEventModel:IMapFrom<Event>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
