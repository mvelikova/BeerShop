using System;
using System.Collections.Generic;
using System.Text;
using Beershop.Data.Models;
using BeerShop.Web.Areas.Beers.Controllers;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BeerShop.Tests.Mocks
{
    class BeerControllerMock
    {
      
            public static Mock<HomeController> New
                => new Mock<HomeController>(null, null);
        }
    
}
