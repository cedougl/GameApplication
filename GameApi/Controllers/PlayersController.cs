using System;
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
    /// The PlayersController class contains all of the Web API methods related to players
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/players")]
    public class PlayersController : ApiController
    {
        // Database context
        private GameDbContext db = new GameDbContext();

        /// <summary>
        /// Gets all players
        /// </summary>
        /// <returns>List of players</returns>
        // GET: api/v1/Players
        [HttpGet]
        public IQueryable<Player> GetPlayers()
        {
            return db.Players;
        }

        /// <summary>
        /// Gets a limited number of players at a given page offset.  This is used for paging on the client. 
        /// </summary>
        /// <param name="limit">Number of records to take at a time</param>
        /// <param name="offset">The page number offset</param>
        /// <returns>List of paged players</returns>
        // GET: api/v1/Players?limit=10&offset=1
        [HttpGet]
        public IQueryable<Player> GetPlayers(int limit, int offset)
        {
            // Use limit and offset parameters to return the appropriate records
            return db.Players
                .OrderBy(c => c.Id)
                .Skip(offset)
                .Take(limit);
        }

        /// <summary>
        /// Gets players whose first name or last name contain a given search string 
        /// </summary>
        /// <param name="searchString">The search string to look for in the first and last name</param>
        /// <returns>List of players whose first name or last name contain the search string</returns>
        // GET: api/v1/Players?searchString=an
        [HttpGet]
        public IQueryable<Player> GetPlayers(string searchString)
        {
            // Check to see if the search string is null or empty
            if (!string.IsNullOrEmpty(searchString))
            {
                // Search both first name and last name for the search string
                return db.Players.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
            }
            else
            {
                // Return all players if the search string is null or empty
                return db.Players;
            }
        }

        /// <summary>
        /// Gets a list of players sorted by the given column name and in the given order. 
        /// </summary>
        /// <param name="sortColumn">Column name to sort by</param>
        /// <param name="orderBy">Sort order - Ascending (ASC) or descending (DESC)</param>
        /// <returns></returns>
        // GET: api/v1/Players?sortColumn=FirstName&orderBy=ASC
        [HttpGet]
        public IQueryable<Player> GetPlayers(string sortColumn, string orderBy)
        {
            // If the orderBy parameter is not ASC or DESC then this is a bad request
            if ((String.Compare(orderBy, "ASC", true) != 0) && (String.Compare(orderBy, "DESC", true) != 0))
            {
                return null;
            }

            // Determine if the sort order is ascending
            bool ascending = (String.Compare(orderBy, "ASC", true) == 0);

            // Sort the list by the sort column name in the proper order, and return the list of players
            return db.Players.OrderByDynamic(sortColumn, ascending ? QueryableExtensions.Order.Asc : QueryableExtensions.Order.Desc);
        }

        /// <summary>
        /// Gets a list of players utilizing paging, sorting, and searching options all at once.  If parameters are
        /// null then they are not applied.
        /// </summary>
        /// <param name="sortColumn">Column name to sort by</param>
        /// <param name="orderBy">Sort order - Ascending (ASC) or Descending (DESC)</param>
        /// <param name="searchString">String to search for in the first and last name of the players</param>
        /// <param name="limit">Number of records to take at a time</param>
        /// <param name="offset">The page number offset</param>
        /// <returns>The paged list of players in the proper order which meet the search criteria</returns>
        // GET: api/v1/Players?sortColumn=FirstName&orderBy=ASC&searchString=an&limit=10&offset=1
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

            // Search for the string specified, if it is not null or empty
            if (!String.IsNullOrEmpty(searchString))
            {
                // Find the string in the first name or last name
                players = players.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
            }

            // Sort by column, if the column name is not null
            if (!String.IsNullOrEmpty(sortColumn))
            {
                // Sort the records by the column name specified, in the proper sort order
                bool ascending = (String.Compare(orderBy, "ASC", true) == 0);
                players = players.OrderByDynamic(sortColumn, ascending ? QueryableExtensions.Order.Asc : QueryableExtensions.Order.Desc);
            }

            // Use limit and offset to return the desired records for the current page
            return players
                .OrderBy(c => c.Id)
                .Skip(offset)
                .Take(limit);
        }

        /// <summary>
        /// Gets the player with a given ID
        /// </summary>
        /// <param name="id">ID of the player</param>
        /// <returns>Player with the given ID</returns>
        // GET: api/v1/Players?id=5
        [HttpGet]
        [ResponseType(typeof(Player))]
        public async Task<IHttpActionResult> GetPlayer(long id)
        {
            // Find the player with the given ID
            Player player = await db.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            // Return the player
            return Ok(player);
        }

        /// <summary>
        /// Update the player with a given ID
        /// </summary>
        /// <param name="id">ID of the player to be updated</param>
        /// <param name="player">Player object used to update the player</param>
        /// <returns>Status</returns>
        // PUT: api/v1/Players?id=5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdatePlayer(long id, Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Make sure the IDs match
            if (id != player.Id)
            {
                return BadRequest();
            }

            db.Entry(player).State = EntityState.Modified;

            try
            {
                // Save the changes
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

        /// <summary>
        /// Adds a player to the database
        /// </summary>
        /// <param name="player">Player object to be added to the database</param>
        /// <returns>Status</returns>
        // POST: api/v1/Players
        [HttpPost]
        [ResponseType(typeof(Player))]
        public async Task<IHttpActionResult> AddPlayer(Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the player
            db.Players.Add(player);
            // Save the changes
            await db.SaveChangesAsync();

            return Content(HttpStatusCode.Created, "Player created");
        }

        /// <summary>
        /// Deletes the player with the given ID
        /// </summary>
        /// <param name="id">ID of the player to be deleted</param>
        /// <returns>Player deleted</returns>
        // DELETE: api/v1/Players?id=5
        [HttpDelete]
        [ResponseType(typeof(Player))]
        public async Task<IHttpActionResult> DeletePlayer(long id)
        {
            // Find the player
            Player player = await db.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            // Remove the player
            db.Players.Remove(player);
            // Save the changes
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

        /// <summary>
        /// Determines if a player exists given an ID
        /// </summary>
        /// <param name="id">ID to search for</param>
        /// <returns>true if the player ID exists</returns>
        private bool PlayerExists(long id)
        {
            return db.Players.Count(e => e.Id == id) > 0;
        }
    }
}