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
    [Route("api/v{version:apiVersion}/games")]
    public class GamesController : ApiController
    {
        private GameDbContext db = new GameDbContext();

        // GET: api/Games
        [HttpGet]
        public IQueryable<Game> GetGames()
        {
            return db.Games;
        }

        // GET: api/Games/5
        [HttpGet]
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> GetGame(long id)
        {
            Game game = await db.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // PUT: api/Games/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateGame(long id, Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game.Id)
            {
                return BadRequest();
            }

            db.Entry(game).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/Games
        [HttpPost]
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> AddGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Games.Add(game);
            await db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = game.Id }, game);
            return Content(HttpStatusCode.Created, "Game created");
        }

        // DELETE: api/Games/5
        [HttpDelete]
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> DeleteGame(long id)
        {
            Game game = await db.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            db.Games.Remove(game);
            await db.SaveChangesAsync();

            return Ok(game);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameExists(long id)
        {
            return db.Games.Count(e => e.Id == id) > 0;
        }
    }
}