using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Beershop.Data.Models;
using BeerShop.Data.Models.Mapping;

namespace BeerShop.Data.Models
{
    public class Beer
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string Price { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<BeerType> Types { get; set; }

        public ICollection<BeerComment> Comments { get; set; } = new HashSet<BeerComment>();
    }
}