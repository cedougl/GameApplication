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

namespace GameApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/matches")]
    public class MatchesController : ApiController
    {
        private GameDbContext db = new GameDbContext();

        // GET: api/Matches
        [HttpGet]
        public IQueryable<Match> GetMatches()
        {
            return db.Matches;
        }

        // GET: api/Matches/5
        [HttpGet]
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> GetMatch(long id)
        {
            Match match = await db.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }

        // GET: api/Matches?playerId=5
        [HttpGet]
        [ResponseType(typeof(List<Match>))]
        public async Task<IHttpActionResult> GetPlayerMatches(long playerId)
        {
            List<Match> matches = await db.Matches.Where(x => x.PlayerId == playerId).ToListAsync();
            if (matches == null)
            {
                return NotFound();
            }

            return Ok(matches);
        }

        // GET: api/Matches?gameId=5
        [HttpGet]
        [ResponseType(typeof(List<Match>))]
        public async Task<IHttpActionResult> GetGameMatches(long gameId)
        {
            List<Match> matches = await db.Matches.Where(x => x.GameId == gameId).ToListAsync();
            if (matches == null)
            {
                return NotFound();
            }

            return Ok(matches);
        }

        // PUT: api/Matches/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateMatch(long id, Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != match.Id)
            {
                return BadRequest();
            }

            db.Entry(match).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // POST: api/Matches
        [HttpPost]
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> AddMatch(Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Matches.Add(match);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = match.Id }, match);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Matches/5
        [HttpDelete]
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> DeleteMatch(long id)
        {
            Match match = await db.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            db.Matches.Remove(match);
            await db.SaveChangesAsync();

            return Ok(match);
        }

        /// <summary>
        /// Dispose the object
        /// </summary>
        /// <param name="disposing">Flag to indicate whether we are disposing</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Determines if a match already exists
        /// </summary>
        /// <param name="id">The match id to search for</param>
        /// <returns>true if the match exists; false if it does not exist</returns>
        private bool MatchExists(long id)
        {
            return db.Matches.Count(e => e.Id == id) > 0;
        }
    }
}