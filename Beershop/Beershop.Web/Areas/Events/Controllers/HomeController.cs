using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Beershop.Data.Models;
using BeerShop.Data;
using BeerShop.Data.Models;
using BeerShop.Services.Contracts;
using BeerShop.Services.Implementations;
using BeerShop.Web.Areas.Events.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BeerShop.Web.Areas.Events.Controllers
{
    [Area("Events")]
    public class HomeController : Controller
    {
        private readonly IEventService events;
        private readonly UserManager<ApplicationUser> usermanager;
        public HomeController(IEventService events, UserManager<ApplicationUser> users)
        {
            this.events = events;
            this.usermanager = users;
        }

        // GET: Events/Events
        public async Task<IActionResult> Index()
        {
            var ev= this.events.GetAll(e => e.User).ToList();
            return View( ev);
        }

        // GET: Events/Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = events.GetSingle(e => e.Id == id, e => e.User);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Events/Create
        public IActionResult Create()
        {
            var model = new CreateEventViewModel();
           
            return View(model);
        }

        // POST: Events/Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEventViewModel @event)
        {
            if (ModelState.IsValid)
            {
                var newEvnet = new Event
                {
                    Name = @event.Name,
                    Place = @event.Place,
                    StartTime = @event.StartTime,
                    Description = @event.Description,
                    ImageUrl = @event.ImageUrl,
                    UserId = usermanager.GetUserId(User)
                };
                events.Add(newEvnet);
                return RedirectToAction(nameof(Index));
            }
          //  ViewData["UserId"] = usermanager.GetUserId(this.User);
            return View(@event);
        }
//
//        // GET: Events/Events/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var @event = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
//            if (@event == null)
//            {
//                return NotFound();
//            }
//            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", @event.UserId);
//            return View(@event);
//        }
//
//        // POST: Events/Events/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Place,StartTime,Description,ImageUrl,UserId")] Event @event)
//        {
//            if (id != @event.Id)
//            {
//                return NotFound();
//            }
//
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(@event);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!EventExists(@event.Id))
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
//            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", @event.UserId);
//            return View(@event);
//        }
//
//        // GET: Events/Events/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var @event = await _context.Events
//                .Include(e => e.User)
//                .SingleOrDefaultAsync(m => m.Id == id);
//            if (@event == null)
//            {
//                return NotFound();
//            }
//
//            return View(@event);
//        }
//
//        // POST: Events/Events/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var @event = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
//            _context.Events.Remove(@event);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//
//        private bool EventExists(int id)
//        {
//            return _context.Events.Any(e => e.Id == id);
//        }
    }
}
