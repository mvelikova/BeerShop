using System;
using System.Collections.Generic;
using System.Text;
using Beershop.Data.Models;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BeerShop.Tests.Mocks
{
    public class UserManagerMock
    {
        public static Mock<UserManager<ApplicationUser>> New
            => new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
    }
}
