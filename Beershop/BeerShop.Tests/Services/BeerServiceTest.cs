using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeerShop.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using BeerShop.Data.Models;
using BeerShop.Data.Models.Mapping;
using BeerShop.Services.Implementations;
using BeerShop.Tests;
using FluentAssertions;

namespace BeerShop.Tests.Services
{
  public  class BeerServiceTest
    {
        
            public BeerServiceTest()
            {
           Tests.Initialize();
            }

            [Fact]
            public void ShouldAddBeer()
            {
                //Arrange
                var db = GetDatabase();

                var b1 = new Beer()
                {
                    Name = "First",
                    Description = "First Description",
                    Price = 12.12m,
                ImageUrl = "test",
                    Country = "London"
                };

                var b2 = new Beer()
                {
                    Name = "Second",
                    Description = "Second Description",
                    Price = 8.99m,
                ImageUrl = "test",
                   Country= "Bulgaria"
                };

                var Beers = new BeerService(db);

                //Act
                Beers.Add(b1, b2);
                var result = Beers.GetAll();

                //Assert
                result.Should().BeOfType<List<Beer>>();
                result.Should().HaveCount(2);
                result.Any(r => r.Name == "Second").Should().Be(true);
                result.Any(r => r.Name == "First").Should().Be(true);

        }

        [Fact]
            public void ShouldReturnAllBeers()
            {
                //Arrange
                var db = GetDatabase();

            var b1 = new Beer()
            {
                Name = "First",
                Description = "First Description",
                ImageUrl = "test",
                Price = 12.12m,
                Country = "London"
            };

                var b2 = new Beer()
                {
                    Name = "Second",
                    Description = "Second Description",
                    Price = 8.99m,
                    ImageUrl = "test",
                    Country = "Bulgaria"
                };

            var Beers = new BeerService(db);
            Beers.Add(b1, b2);

                //Act
                var result = Beers.GetAll();

                //Assert
                result.Should().BeOfType<List<Beer>>();

                result.Should().HaveCount(2);
            }

            [Fact]
            public void ShouldUpdateBeer()
            {
                //Arrange
                var db = GetDatabase();


            var b1 = new Beer()
            {
                Name = "First",
                Description = "First Description",
                Price = 12.12m,
                ImageUrl = "test",
                Country = "London"
            };

            //Act

            var Beers = new BeerService(db);
            Beers.Add(b1);

                b1.Name = "UpdatedBeer";
                Beers.Update(b1);

                //Assert
                Beers.GetList(x => x.Name == "UpdatedBeer").Count.Should().Be(1);
            }

            [Fact]
            public void ShouldDeleteBeer()
            {
                //Arrange
                var db = GetDatabase();
                var b1 = new Beer()
                {
                    Name = "First",
                    Description = "First Description",
                    Price = 12.12m,
                    ImageUrl = "test",
                    Country = "London"
                };


                var b2 = new Beer()
                {
                    Name = "Second",
                    Description = "Second Description",
                    Price = 8.99m,
                    ImageUrl = "test",
                    Country = "Bulgaria"
                };

            var Beers = new BeerService(db);
            Beers.Add(b1, b2);

                //Act

                Beers.Remove(b2);
                //Assert

                Beers.GetAll().Count.Should().Be(1);
            }

            [Fact]
            public void ShouldGetWithNavigationProperties()
            {
                //Arrange
                var db = GetDatabase();
                var b1 = new Beer()
                {
                    Name = "First",
                    Description = "First Description",
                    Price = 12.12m,
                    ImageUrl = "test",
                    Country = "London",
                
            Ingredients = new List<BeerIngredient>()
                {
                    new BeerIngredient()
                    {
                        Ingredient = new Ingredient()
                        {
                            Name = "Ingr1"
                        }
                    },
                    new BeerIngredient()
                    {
                        Ingredient = new Ingredient()
                        {
                            Name = "Ingr2"
                        }
                    }
                }
                };

                var Beers = new BeerService(db);
            Beers.Add(b1);

                //Act

                var result = Beers.Join(x => x.Ingredients).FirstOrDefault(x => x.Name == "First");

                //Assert

                result.Ingredients.Count.Should().Be(2);
            }

        [Fact]
       

        private BeerShopDbContext GetDatabase()
            {
                var dbOptions = new DbContextOptionsBuilder<BeerShopDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new BeerShopDbContext(dbOptions);
            }
        
    }
}
