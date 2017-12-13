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

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=.;Database=BeerShopDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.EventComments)
                .WithOne(e => e.User);

            builder.Entity<BeerComment>()
                .HasOne(a => a.User) //dependent entity
                .WithMany(e => e.BeerComments); //principal

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.BeerComments) //dependent entity
                .WithOne(e => e.User); //principal

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Beers)
                .WithOne(b => b.User);

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Events)
                .WithOne(b => b.User);

            //events
            builder.Entity<Event>()
                .HasMany(a => a.Comments)
                .WithOne(b => b.Event);

            builder.Entity<Event>()
                .HasOne(a => a.User)
                .WithMany(b => b.Events);

            //beer
            builder.Entity<BeerComment>()
                .HasOne(a => a.Beer) //dependent entity
                .WithMany(e => e.Comments); //principal

            //beertypes
            builder.Entity<BeerType>()
                .HasKey(b => new
                {
                    b.BeerId,
                    b.TypeId
                });

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(builder);
        }
    }
}