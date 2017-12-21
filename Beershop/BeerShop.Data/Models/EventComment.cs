using System.ComponentModel.DataAnnotations;
using Beershop.Data.Models;

namespace BeerShop.Data.Models
{
  public  class EventComment:Comment
    {
        [Required]
        public int EventId { get; set; }

        public Event Event { get; set; }
    }
}
