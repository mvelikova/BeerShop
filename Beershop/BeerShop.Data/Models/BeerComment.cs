using System.ComponentModel.DataAnnotations;
using Beershop.Data.Models;

namespace BeerShop.Data.Models
{
    public class BeerComment
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Message { get; set; }


        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public int BeerId { get; set; }

        public Beer Beer { get; set; }
        
    }
}