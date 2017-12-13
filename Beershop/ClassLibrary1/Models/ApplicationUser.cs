using System.Collections.Generic;
using BeerShop.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Beershop.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Beer> Beers { get; set; } = new HashSet<Beer>();
        public ICollection<Event> Events { get; set; } = new HashSet<Event>();
        public ICollection<BeerComment> BeerComments { get; set; } = new HashSet<BeerComment>();
        public ICollection<EventComment> EventComments { get; set; } = new HashSet<EventComment>();
    }
}