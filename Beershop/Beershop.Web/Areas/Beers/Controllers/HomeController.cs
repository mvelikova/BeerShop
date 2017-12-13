using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BeerShop.Services.Contracts;
using BeerShop.Web.Areas.Beers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeerShop.Web.Areas.Beers.Controllers
{
 
    public class HomeController : BeersBaseController
    {
        private readonly IBeerService beers;

          public HomeController(IBeerService service)
          {
              beers = service;
          }

        // GET: Beers/Beers
        public async Task<IActionResult> Index()
        {
            var listBeers = beers.GetAll().AsQueryable().ProjectTo<BeerListingViewModel>().ToList();
            return View(listBeers);
        }

//        // GET: Beers/Beers/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var beer = await _context.Beers
//                .Include(b => b.User)
//                .SingleOrDefaultAsync(m => m.Id == id);
//            if (beer == null)
//            {
//                return NotFound();
//            }
//
//            return View(beer);
//        }
//
//        // GET: Beers/Beers/Create
//        public IActionResult Create()
//        {
//            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
//            return View();
//        }
//
//        // POST: Beers/Beers/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Name,Color,Price,Description,ImageUrl,Country,UserId")] Beer beer)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(beer);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", beer.UserId);
//            return View(beer);
//        }
//
//        // GET: Beers/Beers/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var beer = await _context.Beers.SingleOrDefaultAsync(m => m.Id == id);
//            if (beer == null)
//            {
//                return NotFound();
//            }
//            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", beer.UserId);
//            return View(beer);
//        }
//
//        // POST: Beers/Beers/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Color,Price,Description,ImageUrl,Country,UserId")] Beer beer)
//        {
//            if (id != beer.Id)
//            {
//                return NotFound();
//            }
//
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(beer);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!BeerExists(beer.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", beer.UserId);
//            return View(beer);
//        }
//
//        // GET: Beers/Beers/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var beer = await _context.Beers
//                .Include(b => b.User)
//                .SingleOrDefaultAsync(m => m.Id == id);
//            if (beer == null)
//            {
//                return NotFound();
//            }
//
//            return View(beer);
//        }
//
//        // POST: Beers/Beers/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var beer = await _context.Beers.SingleOrDefaultAsync(m => m.Id == id);
//            _context.Beers.Remove(beer);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//
//        private bool BeerExists(int id)
//        {
//            return _context.Beers.Any(e => e.Id == id);
//        }
    }
}
