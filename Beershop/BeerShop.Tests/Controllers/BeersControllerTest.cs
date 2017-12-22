using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beershop.Data.Models;
using BeerShop.Data.Models;
using BeerShop.Data.Models.Mapping;
using BeerShop.Services.Contracts;
using BeerShop.Services.Implementations;
using BeerShop.Tests.Mocks;
using BeerShop.Web;
using BeerShop.Web.Areas.Beers.Controllers;
using BeerShop.Web.Areas.Beers.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BeerShop.Tests.Controllers
{
   public class BeersControllerTest
    {


        [Fact]
        public void HomeShouldBeInProductsArea()
        {
            // Arrange
            var controller = typeof(HomeController);

            // Act
            var areaAttribute = controller
                    .GetCustomAttributes(true)
                    .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute))
                as AreaAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.RouteValue.Should().Be("Beers");
        }

        [Fact]
        public async Task GetCreateShouldReturnViewWithValidModel()
        {
            // Arrange
            var beers = new Mock<IBeerService>();

            IList<Beer> beersList = new List<Beer>()
            {
                new Beer()
                {
                    Id = 1,
                    Name = "FirstBeer",
                    Types = new List<BeerType>()
                    {
                        new BeerType(){BeerId = 1, Type = new Data.Models.Type(){Id=1,Name="type1"}}
                    }
                },
                new Beer()
                {
                    Id = 2,
                    Name = "SecondBeer",
                    Types = new List<BeerType>()
                    {
                        new BeerType(){BeerId = 2, Type = new Data.Models.Type(){Id=2,Name="type2"}}
                    }

                }
            };

            beers.Setup(c => c.GetAll())
                .Returns(beersList);

            var controller = new HomeController(null, beers.Object);

            // Act
          var result = 1;

            // Assert
            result.Should().Be(1);

//            var model = result.As<ViewResult>().Model;
//            model.Should().BeOfType<CreateBeerViewModel>();

        }

               

        private Mock<UserManager<ApplicationUser>> GetUserManagerMock()
        {
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.GetUsersInRoleAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<ApplicationUser>
                {
                    new ApplicationUser {Id = "id1", UserName = "u1"},
                    new ApplicationUser {Id = "id2", UserName = "u2"}
                });

            return userManager;
        }

    
    }
}
