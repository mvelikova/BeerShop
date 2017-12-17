using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Beershop.Data.Models;

namespace BeerShop.Data.Models
{
    public abstract class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(2000)]
        public string Message { get; set; }

     [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
