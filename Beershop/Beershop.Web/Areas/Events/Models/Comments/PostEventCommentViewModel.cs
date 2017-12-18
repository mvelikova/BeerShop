using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BeerShop.Data.Models;
using BeerShop.Web.Infrastructure.Mapping;

namespace BeerShop.Web.Areas.Events.Models.Comments
{
    public class PostEventCommentViewModel:IMapFrom<EventComment>
    {
        [MaxLength(2000)]
        public string Message { get; set; }

        public int EventId { get; set; }
    }
}
