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
    /// The StatisticsController class contains all of the Web API methods related to statistics
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/statistics")]
    public class StatisticsController : ApiController
    {
        // Database context
        private GameDbContext db = new GameDbContext();

        /// <summary>
        /// Gets all statistics
        /// </summary>
        /// <returns>List of statistics</returns>
        // GET: api/v1/Statistics
        [HttpGet]
        public IQueryable<Statistic> GetStatistics()
        {
            return db.Statistics;
        }

        /// <summary>
        /// Gets a statistic object given an ID 
        /// </summary>
        /// <param name="id">ID to search for</param>
        /// <returns>Statistic object with the given ID</returns>
        // GET: api/v1/Statistics?id=5
        [HttpGet]
        [ResponseType(typeof(Statistic))]
        public async Task<IHttpActionResult> GetStatistic(long id)
        {
            // Find the statistic object with the given ID
            Statistic statistic = await db.Statistics.FindAsync(id);
            if (statistic == null)
            {
                return NotFound();
            }

            // Return the statistic
            return Ok(statistic);
        }

        /// <summary>
        /// Get the statistics for a given player by using the ID
        /// </summary>
        /// <param name="playerId">Player ID to search for</param>
        /// <returns>List of statistics objects for a given player ID</returns>
        // GET: api/v1/Statistics?playerId=5
        [HttpGet]
        [ResponseType(typeof(List<Statistic>))]
        public async Task<IHttpActionResult> GetPlayerStatistics(long playerId)
        {
            // Find the Statistics that have the given player ID
            List<Statistic> statistics = await db.Statistics.Where(x => x.PlayerId == playerId).ToListAsync();
            if (statistics == null)
            {
                return NotFound();
            }

            return Ok(statistics);
        }

        /// <summary>
        /// Get the statistics for a given game by using the ID
        /// </summary>
        /// <param name="gameId">Game ID to search for</param>
        /// <returns>List of statistics objects for a given game ID</returns>
        // GET: api/v1/Statistics?gameId=5
        [HttpGet]
        [ResponseType(typeof(List<Statistic>))]
        public async Task<IHttpActionResult> GetGameStatistics(long gameId)
        {
            // Find the statistics for the given game ID
            List<Statistic> statistics = await db.Statistics.Where(x => x.GameId == gameId).ToListAsync();
            if (statistics == null)
            {
                return NotFound();
            }

            return Ok(statistics);
        }

        /// <summary>
        /// Updates the statistic object with a given ID
        /// </summary>
        /// <param name="id">ID of the statistic to be updated</param>
        /// <param name="statistic">Statistic object to use for the update</param>
        /// <returns>Status</returns>
        // PUT: api/v1/Statistics?id=5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateStatistic(long id, Statistic statistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Make sure the IDs match
            if (id != statistic.Id)
            {
                return BadRequest();
            }

            db.Entry(statistic).State = EntityState.Modified;

            try
            {
                // Save the changes
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatisticExists(id))
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
        /// Adds a statistic object to the database
        /// </summary>
        /// <param name="statistic">Statistic object to be added</param>
        /// <returns>Status</returns>
        // POST: api/v1/Statistics
        [HttpPost]
        [ResponseType(typeof(Statistic))]
        public async Task<IHttpActionResult> AddStatistic(Statistic statistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the statistic object
            db.Statistics.Add(statistic);
            // Save the changes
            await db.SaveChangesAsync();

            return Content(HttpStatusCode.Created, "Created");
        }

        /// <summary>
        /// Deletes the statistic with a given ID
        /// </summary>
        /// <param name="id">ID of the statistic to be deleted</param>
        /// <returns></returns>
        // DELETE: api/v1/Statistics?id=5
        [HttpDelete]
        [ResponseType(typeof(Statistic))]
        public async Task<IHttpActionResult> DeleteStatistic(long id)
        {
            // Find the statistic with the given ID
            Statistic statistic = await db.Statistics.FindAsync(id);
            if (statistic == null)
            {
                return NotFound();
            }

            // Remove the statistic
            db.Statistics.Remove(statistic);
            // Save the changes
            await db.SaveChangesAsync();

            return Ok(statistic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Determines if a given statistic ID exists
        /// </summary>
        /// <param name="id">ID to search for</param>
        /// <returns>true if the statistic ID exists; false otherwise</returns>
        private bool StatisticExists(long id)
        {
            return db.Statistics.Count(e => e.Id == id) > 0;
        }
    }
}