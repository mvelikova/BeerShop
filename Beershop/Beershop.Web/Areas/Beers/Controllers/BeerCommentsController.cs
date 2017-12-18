using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beershop.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeerShop.Data;
using BeerShop.Data.Models;
using BeerShop.Services.Contracts;
using BeerShop.Services.Implementations;
using BeerShop.Web.Areas.Beers.Models.Comments;
using Ganss.XSS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BeerShop.Web.Areas.Beers.Controllers
{
    [Produces("application/json")]
    [Route("api/BeerComments")]
    public class BeerCommentsController : Controller
    {
        private readonly IBeerCommentService comments;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHtmlSanitizer html;
        private readonly IBeerService beers;

        public BeerCommentsController(UserManager<ApplicationUser> users,IBeerCommentService comments, IBeerService beers)
        {
            this.userManager = users;
            this.comments = comments;
            this.beers = beers;
        }

        //        // GET: api/BeerComments
        //        [HttpGet]
        //        public IEnumerable<BeerComment> GetBeerComments()
        //        {
        //            return _context.BeerComments;
        //        }
        //
        //        // GET: api/BeerComments/5
        //        [HttpGet("{id}")]
        //        public async Task<IActionResult> GetBeerComment([FromRoute] int id)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }
        //
        //            var beerComment = await _context.BeerComments.SingleOrDefaultAsync(m => m.Id == id);
        //
        //            if (beerComment == null)
        //            {
        //                return NotFound();
        //            }
        //
        //            return Ok(beerComment);
        //        }
        //
        //        // PUT: api/BeerComments/5
        //        [HttpPut("{id}")]
        //        public async Task<IActionResult> PutBeerComment([FromRoute] int id, [FromBody] BeerComment beerComment)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }
        //
        //            if (id != beerComment.Id)
        //            {
        //                return BadRequest();
        //            }
        //
        //            _context.Entry(beerComment).State = EntityState.Modified;
        //
        //            try
        //            {
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!BeerCommentExists(id))
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

        // POST: api/BeerComments
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostBeerCommentViewModel comment)
        {
            //create new comment from model
            if (!this.ModelState.IsValid)
                return NotFound();

        //  var user = await this.userManager.FindByNameAsync(comment.Username);
            var beer = this.beers.GetSingle(x => x.Id == comment.BeerId);

            if (User == null || beer == null)
                return NotFound();
            var beerComment = new BeerComment()
            {
                Message = comment.Message,
                BeerId = comment.BeerId,
                UserId = this.userManager.GetUserId(User),
                User= await this.userManager.GetUserAsync(this.User)
            };
            this.comments.Add(beerComment);
          return Ok(beerComment);
        }


                // DELETE: api/BeerComments/5
                [HttpDelete]
                [Authorize]
        public async Task<IActionResult> Delete( int id)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var beerComment = comments.GetSingle(c => c.Id == id);
                    if (beerComment == null)
                    {
                        return NotFound();
                    }
                    if (userManager.GetUserId(User)!=beerComment.UserId)
                    {
                        return NotFound();
                    }
                    comments.Remove(beerComment);
                   
        
                    return Ok();
                }

        private bool BeerCommentExists(int id)
        {
            return comments.Any(c => c.Id == id);
        }
    }
}