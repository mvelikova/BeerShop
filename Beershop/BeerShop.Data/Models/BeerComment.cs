using System.ComponentModel.DataAnnotations;
using Beershop.Data.Models;

namespace BeerShop.Data.Models
{
    public class BeerComment:Comment
    {
       
        [Required]
        public int BeerId { get; set; }

        public Beer Beer { get; set; }
        
    }
}