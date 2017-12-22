using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Beershop.Data.Models;
using BeerShop.Data;
using BeerShop.Data.Models;
using BeerShop.Services;
using BeerShop.Services.Contracts;
using BeerShop.Services.Implementations;
using BeerShop.Web.Areas.Events.Models;
using BeerShop.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BeerShop.Web.Areas.Events.Controllers
{
    [Area("Events")]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allEvents = this.events.GetAll(e => e.User,e=>e.Comments).AsQueryable().ProjectTo<EventListingViewModel>().ToList();
            ViewData["UserId"] = usermanager.GetUserId(User);

            return View(allEvents);
        }

        // GET: Events/Events/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @event = events.Join(e=>e.Comments).ThenJoin(e=>e.User).Where(e=>e.Id==id).SingleOrDefault();

            if (@event == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = usermanager.GetUserId(User);
            return View(@event);
        }

        // GET: Events/Events/Creates
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

        // GET: Events/Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = events.GetSingle(e => e.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            if (usermanager.GetUserId(User) != @event.UserId)
            {
                return NotFound();
            }
           
            var model = AutoMapper.Mapper.Map<EditEventModel>(@event);
       
            return View(model);
        }

        // POST: Events/Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditEventModel @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var newEvent = events.GetSingle(e => e.Id == id);
                    if (usermanager.GetUserId(User) != newEvent.UserId)
                    {
                        return NotFound();
                    }
                    ;
                    newEvent.Id = @event.Id;
                    newEvent.Name = @event.Name;
                    newEvent.Place = @event.Place;
                    newEvent.StartTime = @event.StartTime;
                    newEvent.ImageUrl = @event.ImageUrl;
                    newEvent.Description = @event.Description;
                    newEvent.UserId = usermanager.GetUserId(User);


                    events.Update(newEvent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            
            return View(@event);
        }

        // GET: Events/Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = events.GetSingle(e => e.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            if (usermanager.GetUserId(User) != @event.UserId)
            {
                return NotFound();
            }

           

            return View(@event);
        }

        // POST: Events/Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = events.GetSingle(e => e.Id == id);

            events.Remove(@event);
            if (usermanager.GetUserId(User) != @event.UserId)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return events.Any(e => e.Id == id);
        }
    }
}