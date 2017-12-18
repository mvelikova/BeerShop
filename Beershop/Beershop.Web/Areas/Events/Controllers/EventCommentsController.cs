using Beershop.Data.Models;
using BeerShop.Data.Models;
using BeerShop.Services.Contracts;
using Ganss.XSS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerShop.Web.Areas.Events.Models.Comments;

namespace eventshop.Web.Areas.Events.Controllers
{
    [Produces("application/json")]
    [Route("api/EventComments")]
    public class EventCommentsController : Controller
    {
        private readonly IEventCommentService comments;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHtmlSanitizer html;
        private readonly IEventService events;

        public EventCommentsController(UserManager<ApplicationUser> users, IEventCommentService comments, IEventService events)
        {
            this.userManager = users;
            this.comments = comments;
            this.events = events;
       }
//        // GET: api/EventComments
//        [HttpGet]
//        public IEnumerable<EventComment> GetEventComments()
//        {
//            return _context.EventComments;
//        }
//
//        // GET: api/EventComments/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetEventComment([FromRoute] int id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            var eventComment = await _context.EventComments.SingleOrDefaultAsync(m => m.Id == id);
//
//            if (eventComment == null)
//            {
//                return NotFound();
//            }
//
//            return Ok(eventComment);
//        }
//
//        // PUT: api/EventComments/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutEventComment([FromRoute] int id, [FromBody] EventComment eventComment)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            if (id != eventComment.Id)
//            {
//                return BadRequest();
//            }
//
//            _context.Entry(eventComment).State = EntityState.Modified;
//
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!EventCommentExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }
//
//            return NoContent();
//        }

        // POST: api/eventComments
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostEventCommentViewModel comment)
        {
            //create new comment from model
            if (!this.ModelState.IsValid)
                return NotFound();

            //  var user = await this.userManager.FindByNameAsync(comment.Username);
            var @event = this.events.GetSingle(x => x.Id == comment.EventId);

            if (User == null || @event == null)
                return NotFound();
            var eventComment = new EventComment()
            {
                Message = comment.Message,
                EventId = comment.EventId,
                UserId = this.userManager.GetUserId(User),
                User = await this.userManager.GetUserAsync(this.User)
            };
            this.comments.Add(eventComment);
            return Ok(eventComment);
        }


        // DELETE: api/eventComments/5
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventComment = comments.GetSingle(c => c.Id == id);
            if (eventComment == null)
            {
                return NotFound();
            }
            if (userManager.GetUserId(User) != eventComment.UserId)
            {
                return NotFound();
            }
            comments.Remove(eventComment);


            return Ok();
        }

        private bool EventCommentExists(int id)
        {
            return events.Any(e => e.Id == id);
        }
    }
}