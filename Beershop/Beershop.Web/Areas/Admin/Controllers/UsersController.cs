using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beershop.Data.Models;
using BeerShop.Services.Contracts;
using BeerShop.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BeerShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IServiceProvider serviceProvider;
        private readonly IBeerCommentService beercomments;
        private readonly IBeerService beers;

        public UsersController(UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider, IBeerService beers, IBeerCommentService beercomments)
        {
            this.userManager = userManager;
            this.serviceProvider = serviceProvider;
            this.beercomments = beercomments;
            this.beers = beers;
        }

        public IActionResult Index()
        {
            var usersToList = new List<UsersModel>();
            var users = this.userManager.Users;

            foreach (var user in users)
                usersToList.Add(new UsersModel()
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Roles = this.userManager.GetRolesAsync(user).GetAwaiter().GetResult().ToArray()
                });

            return View(usersToList);
        }

        public IActionResult Edit(string username)
        {
            var user = this.userManager.FindByNameAsync(username).GetAwaiter().GetResult();

            if (user == null)
            {
                return NotFound();
            }

            this.ViewData["ReturnUrl"] = "/Admin/Users";

            var roles = this.serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>()
                .Roles
                .Select(x => x.Name);

            this.ViewData["AppRoles"] = roles;

            return View(new UsersModel()
            {
                Username = user.UserName,
                Email = user.Email,
                Roles = this.userManager.GetRolesAsync(user).GetAwaiter().GetResult().ToArray()
            });
        }

      

        public async Task<IActionResult> ChangeUserRole(string username, string newRole)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var isInRole = this.userManager.IsInRoleAsync(user, newRole).GetAwaiter().GetResult();

            if (isInRole)
            {
                //remove role
                await this.userManager.RemoveFromRoleAsync(user, newRole);
                return RedirectToAction(nameof(Edit), new { username = username });
            }

            //add role
            await this.userManager.AddToRoleAsync(user, newRole);
            return RedirectToAction(nameof(Edit), new { username = username });
        }
    }
}