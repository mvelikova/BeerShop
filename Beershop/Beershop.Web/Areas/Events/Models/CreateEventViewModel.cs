using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Beershop.Data.Models;

namespace BeerShop.Web.Areas.Events.Models
{
    public class CreateEventViewModel
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

        [Required]
        public string ImageUrl { get; set; }

        
        public string UserId { get; set; }

       
    }
}
