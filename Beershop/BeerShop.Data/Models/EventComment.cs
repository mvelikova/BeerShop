using System.ComponentModel.DataAnnotations;
using Beershop.Data.Models;

namespace BeerShop.Data.Models
{
  public  class EventComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Message { get; set; }


        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public int? EventId { get; set; }

        public Event Event { get; set; }
    }
}
