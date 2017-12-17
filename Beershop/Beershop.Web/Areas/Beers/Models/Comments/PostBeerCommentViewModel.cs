using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Beershop.Data.Models;
using BeerShop.Data.Models;

namespace BeerShop.Web.Areas.Beers.Models.Comments
{
    public class PostBeerCommentViewModel
    {

       
        
        [MaxLength(2000)]
        public string Message { get; set; }

        public int BeerId { get; set; }

    }
}
