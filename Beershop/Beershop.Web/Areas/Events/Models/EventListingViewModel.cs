
namespace BeerShop.Web.Areas.Events.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Beershop.Data.Models;
    using BeerShop.Data.Models;
    using BeerShop.Web.Infrastructure.Mapping;

    public class EventListingViewModel:IMapFrom<Event>
    {
        public int Id { get; set; }

       public string Name { get; set; }

       
        public string Place { get; set; }

        
        public DateTime StartTime { get; set; }

     
      public string ImageUrl { get; set; }

       
        public string UserId { get; set; }

      
        public ICollection<EventComment> Comments { get; set; } = new HashSet<EventComment>();
    }
}
