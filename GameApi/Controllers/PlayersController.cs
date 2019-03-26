using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GameApi.Models;
using Microsoft.Web.Http;
using System.Linq.Expressions;
using PagedList;

namespace GameApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/players")]
    public class PlayersController : ApiController
    {
        private GameDbContext db = new GameDbContext();

        // GET: api/Players
        [HttpGet]
        public IQueryable<Player> GetPlayers()
        {
            return db.Players;
        }

        // GET: api/Players
        [HttpGet]
        public IQueryable<Player> GetPlayers(int limit, int offset)
        {
            // Use limit and offset parameters to return the appropriate records
            return db.Players
                .OrderBy(c => c.Id)
                .Skip(offset)
                .Take(limit);
        }

        [HttpGet]
        public IQueryable<Player> GetPlayers(string searchString)
        {
            // Search both first name and last name for the search string
            if (!string.IsNullOrEmpty(searchString))
            {
                return db.Players.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
            }
            else
            {
                return db.Players;
            }
        }

        [HttpGet]
        public IQueryable<Player> GetPlayers(string sortColumn, string orderBy)
        {
            // If the orderBy parameter is not ASC or DESC then this is a bad request
            if ((String.Compare(orderBy, "ASC", true) != 0) && (String.Compare(orderBy, "DESC", true) != 0))
            {
                return null;
            }

            bool ascending = (String.Compare(orderBy, "ASC", true) == 0);

            return db.Players.OrderByDynamic(sortColumn, ascending ? QueryableExtensions.Order.Asc : QueryableExtensions.Order.Desc);
        }

        [HttpGet]
        public IQueryable<Player> GetPlayers(
            string sortColumn,
            string orderBy,
            string searchString,
            int limit,
            int offset)
        {
            if (searchString != null)
            {
                offset = 1;
            }

            IQueryable<Player> players = db.Players;

            if (!String.IsNullOrEmpty(searchString))
            {
                players = players.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
            }

            // Sort by column, if the column name is not null
            if (!String.IsNullOrEmpty(sortColumn))
            {
                bool ascending = (String.Compare(orderBy, "ASC", true) == 0);
                players = players.OrderByDynamic(sortColumn, ascending ? QueryableExtensions.Order.Asc : QueryableExtensions.Order.Desc);
            }

            // Use limit and offset to return the desired records for the current page
            return players
                .OrderBy(c => c.Id)
                .Skip(offset)
                .Take(limit);
        }

        // GET: api/Players/5
        [HttpGet]
        [ResponseType(typeof(Player))]
        public async Task<IHttpActionResult> GetPlayer(long id)
        {
            Player player = await db.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // PUT: api/Players/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdatePlayer(long id, Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.Id)
            {
                return BadRequest();
            }

            db.Entry(player).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Players
        [HttpPost]
        [ResponseType(typeof(Player))]
        public async Task<IHttpActionResult> AddPlayer(Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Players.Add(player);
            await db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = player.Id }, player);
            return Content(HttpStatusCode.Created, "Player created");
        }

        // DELETE: api/Players/5
        [HttpDelete]
        [ResponseType(typeof(Player))]
        public async Task<IHttpActionResult> DeletePlayer(long id)
        {
            Player player = await db.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            db.Players.Remove(player);
            await db.SaveChangesAsync();

            return Ok(player);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerExists(long id)
        {
            return db.Players.Count(e => e.Id == id) > 0;
        }
    }
}