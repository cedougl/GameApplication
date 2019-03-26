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
    /// The AchievementsController class contains all of the Web API methods related to achievements
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/achievements")]
    public class AchievementsController : ApiController
    {
        // Database context
        private GameDbContext db = new GameDbContext();

        /// <summary>
        /// GetAchievements - Gets all achievements
        /// </summary>
        /// <returns>List of achievements</returns>
        // GET: api/v1/Achievements
        [HttpGet]
        public IQueryable<Achievement> GetAchievements()
        {
            // Return all achievements
            return db.Achievements;
        }

        /// <summary>
        /// GetAchievement - Gets an achievement by ID
        /// </summary>
        /// <param name="id">ID of the achievement to be retrieved</param>
        /// <returns>Achievement object with the given ID</returns>
        // GET: api/v1/Achievements?id=5
        [HttpGet]
        [ResponseType(typeof(Achievement))]
        public async Task<IHttpActionResult> GetAchievement(long id)
        {
            // Find the achievement by ID
            Achievement achievement = await db.Achievements.FindAsync(id);
            if (achievement == null)
            {
                return NotFound();
            }

            return Ok(achievement);
        }

        /// <summary>
        /// GetPlayerAchievements - Gets the achievements for a given player using the player ID
        /// </summary>
        /// <param name="playerId">Player ID to find</param>
        /// <returns>List of achievements for a given player</returns>
        // GET: api/v1/Achievements?playerId=5
        [HttpGet]
        [ResponseType(typeof(List<Achievement>))]
        public async Task<IHttpActionResult> GetPlayerAchievements(long playerId)
        {
            // Find the achievements with the given player ID
            List<Achievement> achievements = await db.Achievements.Where(x => x.PlayerId == playerId).ToListAsync();
            if (achievements == null)
            {
                return NotFound();
            }

            return Ok(achievements);
        }

        /// <summary>
        /// GetGameAchievements - Gets the achievements for a given game using the ID
        /// </summary>
        /// <param name="gameId">Game ID to find</param>
        /// <returns>List of achievements for a given game ID</returns>
        // GET: api/v1/Achievements?gameId=5
        [HttpGet]
        [ResponseType(typeof(List<Achievement>))]
        public async Task<IHttpActionResult> GetGameAchievements(long gameId)
        {
            // Find the achievements for a given game ID
            List<Achievement> achievements = await db.Achievements.Where(x => x.GameId == gameId).ToListAsync();
            if (achievements == null)
            {
                return NotFound();
            }

            return Ok(achievements);
        }

        /// <summary>
        /// UpdateAchievement - Updates the achievement object with a given ID 
        /// </summary>
        /// <param name="id">ID of the achievement object to be updated</param>
        /// <param name="achievement">Achievement object to be used for the update</param>
        /// <returns>Status</returns>
        // PUT: api/v1/Achievements?id=5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateAchievement(long id, Achievement achievement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Valiate the IDs are the same
            if (id != achievement.Id)
            {
                return BadRequest();
            }

            db.Entry(achievement).State = EntityState.Modified;

            try
            {
                // Save the changes
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AchievementExists(id))
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
        /// AddAchievement - Adds the achievement object to the database
        /// </summary>
        /// <param name="achievement">Achievement object to be added</param>
        /// <returns>Status</returns>
        // POST: api/v1/Achievements
        [HttpPost]
        [ResponseType(typeof(Achievement))]
        public async Task<IHttpActionResult> AddAchievement(Achievement achievement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the achievement
            db.Achievements.Add(achievement);
            // Save the changes
            await db.SaveChangesAsync();

            return Content(HttpStatusCode.Created, "Created");
        }

        /// <summary>
        /// DeleteAchievement - Deletes the achievement by id
        /// </summary>
        /// <param name="id">ID of the achievement to be deleted</param>
        /// <returns>Achievement object deleted</returns>
        // DELETE: api/v1/Achievements?id=5
        [HttpDelete]
        [ResponseType(typeof(Achievement))]
        public async Task<IHttpActionResult> DeleteAchievement(long id)
        {
            Achievement achievement = await db.Achievements.FindAsync(id);
            if (achievement == null)
            {
                return NotFound();
            }

            // Remove the achievement
            db.Achievements.Remove(achievement);
            // Save the changes
            await db.SaveChangesAsync();

            return Ok(achievement);
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
        /// AchievementExists - Determines if an achievement exists with the given ID
        /// </summary>
        /// <param name="id">ID to find</param>
        /// <returns>true if the achievement exists; false otherwise</returns>
        private bool AchievementExists(long id)
        {
            return db.Achievements.Count(e => e.Id == id) > 0;
        }
    }
}