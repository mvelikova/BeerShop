using System.Linq;
using Beershop.Data.Models;
using BeerShop.Data.Models;
using BeerShop.Data.Models.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeerShop.Data
{
    public class BeerShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public BeerShopDbContext(DbContextOptions<BeerShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<BeerComment> BeerComments { get; set; }
        public DbSet<EventComment> EventComments { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(a => a.EventComments)
                .WithOne(e => e.User);

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Events) //dependent
                .WithOne(b => b.User) //principal
                .OnDelete(DeleteBehavior.SetNull);

            //events
            builder.Entity<Event>()
                .HasMany(a => a.Comments)
                .WithOne(b => b.Event);

//            //...........................

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.BeerComments) //dependent entity
                .WithOne(e => e.User);
            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Beers)
                .WithOne(b => b.User)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Beer>()
                .HasMany(a => a.Comments)
                .WithOne(b => b.Beer);
//
//            //beertypes
            builder.Entity<BeerType>()
                .HasKey(b => new
                {
                    b.BeerId,
                    b.TypeId
                });

            //beerIngredients
            builder.Entity<BeerIngredient>()
                .HasKey(b => new
                {
                    b.BeerId,
                    b.IngredientId
                });
//

            builder.Entity<BeerIngredient>()
                .HasOne(bc => bc.Beer)
                .WithMany(b => b.Ingredients)
                .HasForeignKey(bc => bc.BeerId);
//
            builder.Entity<BeerType>()
                .HasOne(bc => bc.Beer)
                .WithMany(b => b.Types)
                .HasForeignKey(bc => bc.BeerId);

            builder.Entity<BeerIngredient>()
                .HasOne(bc => bc.Ingredient)
                .WithMany(b => b.Beers)
                .HasForeignKey(bc => bc.IngredientId);
            //
            builder.Entity<BeerType>()
                .HasOne(bc => bc.Type)
                .WithMany(b => b.Beers)
                .HasForeignKey(bc => bc.TypeId);

            base.OnModelCreating(builder);
        }
    }
}