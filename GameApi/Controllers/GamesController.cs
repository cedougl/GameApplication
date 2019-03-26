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
    /// The GamesController class contains all of the Web API methods related to games
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/games")]
    public class GamesController : ApiController
    {
        // Database context
        private GameDbContext db = new GameDbContext();

        /// <summary>
        /// Gets all games
        /// </summary>
        /// <returns>List of games</returns>
        // GET: api/v1/Games
        [HttpGet]
        public IQueryable<Game> GetGames()
        {
            return db.Games;
        }

        /// <summary>
        /// Gets a game by ID
        /// </summary>
        /// <param name="id">Game ID to find</param>
        /// <returns>Game object with the given ID</returns>
        // GET: api/v1/Games?id=5
        [HttpGet]
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> GetGame(long id)
        {
            // Find the game by ID
            Game game = await db.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        /// <summary>
        /// Updates the game by using the ID
        /// </summary>
        /// <param name="id">Game ID to find</param>
        /// <param name="game">Game object to use for the update</param>
        /// <returns>Status</returns>
        // PUT: api/v1/Games/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateGame(long id, Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate IDs are the same
            if (id != game.Id)
            {
                return BadRequest();
            }

            db.Entry(game).State = EntityState.Modified;

            try
            {
                // Save the changes
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

        /// <summary>
        /// Adds the game object to the database 
        /// </summary>
        /// <param name="game">Game object to be added</param>
        /// <returns>Status</returns>
        // POST: api/v1/Games
        [HttpPost]
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> AddGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the game
            db.Games.Add(game);
            // Save the changes
            await db.SaveChangesAsync();

            return Content(HttpStatusCode.Created, "Game created");
        }

        /// <summary>
        /// Deletes the game by ID
        /// </summary>
        /// <param name="id">ID of the game to be deleted</param>
        /// <returns>The game object deleted</returns>
        // DELETE: api/v1/Games?id=5
        [HttpDelete]
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> DeleteGame(long id)
        {
            // Find the game to be deleted by ID
            Game game = await db.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            // Remove the game
            db.Games.Remove(game);
            // Save the changes
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

        /// <summary>
        /// Determines if a game with agiven ID exists
        /// </summary>
        /// <param name="id">ID to find</param>
        /// <returns>true if the game exists; false otherwise</returns>
        private bool GameExists(long id)
        {
            return db.Games.Count(e => e.Id == id) > 0;
        }
    }
}