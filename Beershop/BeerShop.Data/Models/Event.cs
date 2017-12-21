using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Beershop.Data.Models;

namespace BeerShop.Data.Models
{
    public class Event
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

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<EventComment> Comments { get; set; } = new HashSet<EventComment>();
    }
}