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
    [Route("api/v{version:apiVersion}/statistics")]
    public class StatisticsController : ApiController
    {
        private GameDbContext db = new GameDbContext();

        // GET: api/Statistics
        [HttpGet]
        public IQueryable<Statistic> GetStatistics()
        {
            return db.Statistics;
        }

        // GET: api/Statistics/5
        [HttpGet]
        [ResponseType(typeof(Statistic))]
        public async Task<IHttpActionResult> GetStatistic(long id)
        {
            Statistic statistic = await db.Statistics.FindAsync(id);
            if (statistic == null)
            {
                return NotFound();
            }

            return Ok(statistic);
        }

        // GET: api/Statistics?playerId=5
        [HttpGet]
        [ResponseType(typeof(List<Statistic>))]
        public async Task<IHttpActionResult> GetPlayerStatistics(long playerId)
        {
            List<Statistic> statistics = await db.Statistics.Where(x => x.PlayerId == playerId).ToListAsync();
            if (statistics == null)
            {
                return NotFound();
            }

            return Ok(statistics);
        }

        // GET: api/Statistics?gameId=5
        [HttpGet]
        [ResponseType(typeof(List<Statistic>))]
        public async Task<IHttpActionResult> GetGameStatistics(long gameId)
        {
            List<Statistic> statistics = await db.Statistics.Where(x => x.GameId == gameId).ToListAsync();
            if (statistics == null)
            {
                return NotFound();
            }

            return Ok(statistics);
        }

        // PUT: api/Statistics/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateStatistic(long id, Statistic statistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statistic.Id)
            {
                return BadRequest();
            }

            db.Entry(statistic).State = EntityState.Modified;

            try
            {
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

        // POST: api/Statistics
        [HttpPost]
        [ResponseType(typeof(Statistic))]
        public async Task<IHttpActionResult> AddStatistic(Statistic statistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Statistics.Add(statistic);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = statistic.Id }, statistic);
        }

        // DELETE: api/Statistics/5
        [HttpDelete]
        [ResponseType(typeof(Statistic))]
        public async Task<IHttpActionResult> DeleteStatistic(long id)
        {
            Statistic statistic = await db.Statistics.FindAsync(id);
            if (statistic == null)
            {
                return NotFound();
            }

            db.Statistics.Remove(statistic);
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

        private bool StatisticExists(long id)
        {
            return db.Statistics.Count(e => e.Id == id) > 0;
        }
    }
}