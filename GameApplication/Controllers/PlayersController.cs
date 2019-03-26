using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GameApplication.Repository;
using GameApplication.Models;
using System.Net.Http;

namespace GameApplication.Controllers
{
    /// <summary>
    /// PlayersController - Controller for all player requests
    /// </summary>
    public class PlayersController : Controller
    {
        /// <summary>
        /// Calls the web API to get a list of players based on criteria in the parameters
        /// </summary>
        /// <param name="sortColumnName">Column name to sort players by</param>
        /// <param name="orderBy">Sort order - Ascending or descending</param>
        /// <param name="searchString">String to search for in the first name and last name</param>
        /// <returns>ViewResult with the list of players</returns>
        [HttpGet]
        public ActionResult Index(string sortColumnName, string orderBy, string searchString)
        {
            ServiceRepository service = new ServiceRepository();

            string url = "";

            // Neither sorting or search string was specified
            if (string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(searchString))
            {
                url = "api/v1/players";
            }
            // Search string and sorting
            else if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(searchString))
            {
                url = "api/v1/players?sortColumn=" + sortColumnName + "&orderBy=" + orderBy + "&search=" + searchString;
            }
            // Sorting
            else if (!string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(searchString))
            {
                url = "api/v1/players?sortColumn=" + sortColumnName + "&orderBy=" + orderBy;
            }
            // Search string
            else if (string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(searchString))
            {
                url = "api/v1/players?searchString=" + searchString;
            }

            // Call the web API with the URL we have built based on the criteria
            HttpResponseMessage response = service.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<Player> players = response.Content.ReadAsAsync<List<Player>>().Result;
            ViewBag.Title = "Players";

            return View(players);
        }

        //[HttpGet]
        //public ActionResult Index(string sortColumnName, string orderBy, string searchString, int limit, int offset)
        //{
        //    ServiceRepository service = new ServiceRepository();

        //    string url = "api/v1/players?sortColumn=" + sortColumnName + "&orderBy=" + orderBy + "&search=" + searchString +
        //        "&limit=" + limit.ToString() + "&offset=" + offset.ToString();

        //    // Neither sorting or search string was specified
        //    //if (string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(searchString))
        //    //{
        //    //    url = "api/v1/players";
        //    //}
        //    //// Search string and sorting
        //    //else if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(searchString))
        //    //{
        //    //    url = "api/v1/players?sortColumn=" + sortColumnName + "&orderBy=" + orderBy + "&search=" + searchString;
        //    //}
        //    //// Sorting
        //    //else if (!string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(searchString))
        //    //{
        //    //    url = "api/v1/players?sortColumn=" + sortColumnName + "&orderBy=" + orderBy;
        //    //}
        //    //// Search string
        //    //else if (string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(searchString))
        //    //{
        //    //    url = "api/v1/players?searchString=" + searchString;
        //    //}


        //    HttpResponseMessage response = service.GetResponse(url);
        //    response.EnsureSuccessStatusCode();
        //    List<Player> players = response.Content.ReadAsAsync<List<Player>>().Result;
        //    ViewBag.Title = "Players";

        //    return View(players);
        //}

        /// <summary>
        /// Calls the web API to get the details for a specific player
        /// </summary>
        /// <param name="id">Player ID to find</param>
        /// <returns>ViewResult with the specific player object</returns>
        [HttpGet]
        public ActionResult Details(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/players?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Player player = response.Content.ReadAsAsync<Player>().Result;
            ViewBag.Title = "Player Details";
            return View(player);
        }

        //public ActionResult Statistics(long playerId)
        //{
        //    ServiceRepository service = new ServiceRepository();
        //    HttpResponseMessage response = service.GetResponse("api/v1/statistics?playerId=" + playerId.ToString());
        //    response.EnsureSuccessStatusCode();
        //    List<Statistic> stats = response.Content.ReadAsAsync<List<Statistic>>().Result;
        //    ViewBag.Title = "Player Statistics";
        //    return View(stats);
        //}

        //public ActionResult Achievements(long playerId)
        //{
        //    ServiceRepository service = new ServiceRepository();
        //    HttpResponseMessage response = service.GetResponse("api/v1/achievements?playerId=" + playerId.ToString());
        //    response.EnsureSuccessStatusCode();
        //    List<Achievement> achievements = response.Content.ReadAsAsync<List<Achievement>>().Result;
        //    ViewBag.Title = "Player Achievements";
        //    return View(achievements);
        //}

        /// <summary>
        /// Calls the web API to get the details for a specific player to be edited
        /// </summary>
        /// <param name="id">Player ID to find</param>
        /// <returns>ViewResult with the specific player object</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/players?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Player player = response.Content.ReadAsAsync<Player>().Result;
            ViewBag.Title = "Player Details";
            return View(player);
        }

        /// <summary>
        /// Calls the web API to update a player object
        /// </summary>
        /// <param name="player">Player object to be used for the update</param>
        /// <returns>RedirectToRouteResult to the index action after the update</returns>
        [HttpPut]  
        public ActionResult Update(Player player)
        {
            ServiceRepository service = new ServiceRepository();

            // Set the update time to now
            player.UpdateTime = DateTime.Now;
            player.UpdatedBy = 1;

            HttpResponseMessage response = service.PutResponse("api/v1/players?id=" + player.Id.ToString(), player);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Calls the web API to add a player object
        /// </summary>
        /// <param name="player">Player object to be added</param>
        /// <returns>RedirectToRouteResult to the index action after the update</returns>
        [HttpPost]
        public ActionResult Create(Player player)
        {
            ServiceRepository service = new ServiceRepository();

            // Set the creation and update time to now
            player.CreateTime = DateTime.Now;
            player.UpdateTime = DateTime.Now;
            // Hard code the CreatedBy and UpdatedBy user ID for now, until we add user authentication
            player.CreatedBy = 1;
            player.UpdatedBy = 1;

            HttpResponseMessage response = service.PostResponse("api/v1/players", player);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Calls the web API to get the details for a specific player to be deleted
        /// </summary>
        /// <param name="id">Player ID to find</param>
        /// <returns>ViewResult with the specific player object</returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/players?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Player player = response.Content.ReadAsAsync<Player>().Result;
            ViewBag.Title = "Player Details";
            return View(player);
        }

        /// <summary>
        /// Calls the web API to delete a player
        /// </summary>
        /// <param name="id">Player ID of the object to be deleted</param>
        /// <returns>RedirectToRouteResult to the index action after the delete</returns>
        [HttpDelete]
        public ActionResult DeletePlayer(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.DeleteResponse("api/v1/players?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}
