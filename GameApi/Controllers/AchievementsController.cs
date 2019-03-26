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
    [Route("api/v{version:apiVersion}/achievements")]
    public class AchievementsController : ApiController
    {
        private GameDbContext db = new GameDbContext();

        // GET: api/Achievements
        [HttpGet]
        public IQueryable<Achievement> GetAchievements()
        {
            return db.Achievements;
        }

        // GET: api/Achievements/5
        [HttpGet]
        [ResponseType(typeof(Achievement))]
        public async Task<IHttpActionResult> Get(long id)
        {
            Achievement achievement = await db.Achievements.FindAsync(id);
            if (achievement == null)
            {
                return NotFound();
            }

            return Ok(achievement);
        }

        // GET: api/Achievements?playerId=5
        [HttpGet]
        [ResponseType(typeof(List<Achievement>))]
        public async Task<IHttpActionResult> GetPlayerAchievements(long playerId)
        {
            List<Achievement> achievements = await db.Achievements.Where(x => x.PlayerId == playerId).ToListAsync();
            if (achievements == null)
            {
                return NotFound();
            }

            return Ok(achievements);
        }

        // GET: api/Achievements?gameId=5
        [HttpGet]
        [ResponseType(typeof(List<Achievement>))]
        public async Task<IHttpActionResult> GetGameAchievements(long gameId)
        {
            List<Achievement> achievements = await db.Achievements.Where(x => x.GameId == gameId).ToListAsync();
            if (achievements == null)
            {
                return NotFound();
            }

            return Ok(achievements);
        }

        // PUT: api/Achievements/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateAchievement(long id, Achievement achievement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != achievement.Id)
            {
                return BadRequest();
            }

            db.Entry(achievement).State = EntityState.Modified;

            try
            {
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

        // POST: api/Achievements
        [HttpPost]
        [ResponseType(typeof(Achievement))]
        public async Task<IHttpActionResult> AddAchievement(Achievement achievement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Achievements.Add(achievement);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = achievement.Id }, achievement);
        }

        // DELETE: api/Achievements/5
        [HttpDelete]
        [ResponseType(typeof(Achievement))]
        public async Task<IHttpActionResult> DeleteAchievement(long id)
        {
            Achievement achievement = await db.Achievements.FindAsync(id);
            if (achievement == null)
            {
                return NotFound();
            }

            db.Achievements.Remove(achievement);
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

        private bool AchievementExists(long id)
        {
            return db.Achievements.Count(e => e.Id == id) > 0;
        }
    }
}