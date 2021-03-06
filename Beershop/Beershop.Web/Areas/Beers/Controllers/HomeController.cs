﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Beershop.Data.Models;
using BeerShop.Data.Models;
using BeerShop.Data.Models.Mapping;
using BeerShop.Services;
using BeerShop.Services.Contracts;
using BeerShop.Web.Areas.Beers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BeerShop.Web.Areas.Beers.Controllers
{
 
    public class HomeController : BeersBaseController
    {
        private readonly IBeerService beers;
        private readonly UserManager<ApplicationUser> userManager;
          public HomeController(UserManager<ApplicationUser> userManager, IBeerService service)
          {
              beers = service;
              this.userManager = userManager;
          }

        // GET: Beers/Beers
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var listBeers = beers.GetAll(e => e.User, e => e.Comments).AsQueryable().ProjectTo<BeerListingViewModel>().ToList();
            ViewData["UserId"] = userManager.GetUserId(User);
            return View(listBeers);
        }

        // GET: Beers/Beers/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
//
//            var beer = beers.GetSignleWithComments(id);
            var beer = beers.Join(b=>b.Types).ThenJoin(t=>t.Type).Join(b => b.Comments).ThenJoin(c => c.User).Join(b=>b.Ingredients).ThenJoin(i=>i.Ingredient).Single(b => b.Id == id);
            if (beer == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = userManager.GetUserId(User);

            return View(beer);
        }

        // GET: Beers/Beers/Create
        public IActionResult Create()
        {
            var model = new CreateBeerViewModel
            {
                Types = beers.GetAllBeerTypes().Select(t => t.Name).ToList()
            };

            return View(model);
        }

        // POST: Beers/Beers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBeerViewModel beer)
        {
          
           
            if (ModelState.IsValid)
            {

                var newBeer = new Beer
                {
                    Name = beer.Name,
                    Description = beer.Description,
                    Price = beer.Price,
                    Country = beer.Country,
                    ImageUrl = beer.ImageUrl,
                    UserId = userManager.GetUserId(this.User)
            };

                beers.Add(newBeer);
               
//
               
                if (beer.Types[0]!=null)
                {
                  var  typesList2 = beer.Types[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var type in typesList2)
                    {
                        beers.AddTypeToBeer(newBeer.Id, type);
                    }
                }


                if (beer.Ingredients != null)
                {
                    var ingredientsList = beer.Ingredients.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var ingr in ingredientsList)
                    {
                        beers.AddIngredientToBeer(newBeer.Id, ingr.Trim());
                    }
                }
                return RedirectToAction(nameof(Index));
            }
         
            return View();
        }

//        // GET: Beers/Beers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = beers.GetSingle(b => b.Id == id);
            if (beer == null)
            {
                return NotFound();
            }
            if (userManager.GetUserId(User) != beer.UserId && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            EditBeerViewModel model = Mapper.Map<EditBeerViewModel>(beer);
           
            return View(model);
        }

        // POST: Beers/Beers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EditBeerViewModel beer)
        {
            if (id != beer.Id)
            {
                return NotFound();
            }
            var newBeer = beers.GetSingle(b => b.Id == beer.Id);
            if (userManager.GetUserId(User) != newBeer.UserId && !User.IsInRole("Admin"))
            {
                return NotFound();
            }

            newBeer.Name = beer.Name;
            newBeer.Description = beer.Description;
            newBeer.Price = beer.Price;
            newBeer.Country = beer.Country;
            newBeer.ImageUrl = beer.ImageUrl;
            newBeer.UserId = userManager.GetUserId(this.User);
              
            
            if (ModelState.IsValid)
            {
                try
                {
                   
                    
                    beers.Update(newBeer);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerExists(beer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
           
           
            // ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", beer.UserId);
            return View(newBeer);
        }

        // GET: Beers/Beers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = beers.GetSingle(b => b.Id == id, b => b.User);
            if (beer == null)
            {
                return NotFound();
            }
            if (userManager.GetUserId(User) != beer.UserId && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            return View(beer);
        }

        // POST: Beers/Beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
          
            var beer = beers.GetSingle(b => b.Id == id);
          beers.Remove(beer);
            if (userManager.GetUserId(User) !=beer.UserId && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> GetAllIngredients()
        {

           
            var ingedients = beers.GetAllBeerIngredients().GroupBy(b => b.Name).Select(b => b.First()).ToList();
            return Ok(ingedients);
        }
        public async Task<IActionResult> GetAllIngredientsWithSubstr(string substr)
        {

            if (substr==null)
            {
                substr = "";
            }
            var ingedients = beers.GetAllBeerIngredients().Where(i=>i.Name.ToLower().Contains(substr)).ToList();
            return Ok(ingedients);
        }
        private bool BeerExists(int id)
        {
            return beers.Any(b => b.Id == id);
        }
    }
}
