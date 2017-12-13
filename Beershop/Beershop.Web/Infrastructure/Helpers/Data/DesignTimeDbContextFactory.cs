using System.IO;
using BeerShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BeerShop.Web.Infrastructure.Helpers.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BeerShopDbContext>
    {
        public BeerShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BeerShopDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new BeerShopDbContext(builder.Options);
        }
    }
}
