using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GameApi.Models;
using Microsoft.Web.Http;

namespace GameApi.Controllers
{
    /// <summary>
    /// The MatchesController class contains all of the Web API methods related to matches
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/matches")]
    public class MatchesController : ApiController
    {
        // Database context
        private GameDbContext db = new GameDbContext();

        /// <summary>
        /// GetMatches - Gets all matches
        /// </summary>
        /// <returns>List of matches</returns>
        // GET: api/v1/Matches
        [HttpGet]
        public IQueryable<Match> GetMatches()
        {
            return db.Matches;
        }

        /// <summary>
        /// GetMatch - Gets a match with a specific ID
        /// </summary>
        /// <param name="id">ID to find</param>
        /// <returns>Match object with the given ID</returns>
        // GET: api/v1/Matches?id=5
        [HttpGet]
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> GetMatch(long id)
        {
            // Find the match with the given ID
            Match match = await db.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }

        /// <summary>
        /// GetPlayerMatches - Gets the matches for a given player using the player ID
        /// </summary>
        /// <param name="playerId">Player ID to find</param>
        /// <returns>List of matches for a given player</returns>
        // GET: api/v1/Matches?playerId=5
        [HttpGet]
        [ResponseType(typeof(List<Match>))]
        public async Task<IHttpActionResult> GetPlayerMatches(long playerId)
        {
            // Find the matches with the given player ID
            List<Match> matches = await db.Matches.Where(x => x.PlayerId == playerId).ToListAsync();
            if (matches == null)
            {
                return NotFound();
            }

            return Ok(matches);
        }

        /// <summary>
        /// GetGameMatches - Gets the matches for a given game using the ID
        /// </summary>
        /// <param name="gameId">Game ID to find</param>
        /// <returns>List of matches for a given game ID</returns>
        // GET: api/v1/Matches?gameId=5
        [HttpGet]
        [ResponseType(typeof(List<Match>))]
        public async Task<IHttpActionResult> GetGameMatches(long gameId)
        {
            // Find the matches with the given game ID
            List<Match> matches = await db.Matches.Where(x => x.GameId == gameId).ToListAsync();
            if (matches == null)
            {
                return NotFound();
            }

            return Ok(matches);
        }

        /// <summary>
        /// UpdateMatch - Updates a match with a given ID
        /// </summary>
        /// <param name="id">ID to find</param>
        /// <param name="match">Match object to use for the update</param>
        /// <returns>Status</returns>
        // PUT: api/Matches/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateMatch(long id, Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Make sure the IDs match
            if (id != match.Id)
            {
                return BadRequest();
            }

            db.Entry(match).State = EntityState.Modified;

            try
            {
                // Save the changes
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

        /// <summary>
        /// AddMatch - Creates and adds a match to the database
        /// </summary>
        /// <param name="match">Match object to be added</param>
        /// <returns>Status</returns>
        // POST: api/v1/Matches
        [HttpPost]
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> AddMatch(Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the match
            db.Matches.Add(match);
            // Save the changes
            await db.SaveChangesAsync();

            return Content(HttpStatusCode.Created, "Created");
        }

        /// <summary>
        /// DeleteMatch - Deletes a match with a given ID
        /// </summary>
        /// <param name="id">ID to find</param>
        /// <returns>The match object deleted</returns>
        // DELETE: api/v1/Matches?id=5
        [HttpDelete]
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> DeleteMatch(long id)
        {
            // Find the match by ID
            Match match = await db.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            // Remove the match
            db.Matches.Remove(match);
            // Save the changes
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