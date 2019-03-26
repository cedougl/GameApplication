using GameApplication.Models;
using GameApplication.Repository;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace GameApplication.Controllers
{
    /// <summary>
    /// MatchesController - Controller for all match related requests
    /// </summary>
    public class MatchesController : Controller
    {
        /// <summary>
        /// Index - Calls the web API to get a list of matches
        /// </summary>
        /// <returns>ActionResult with a list of matches</returns>
        [HttpGet]
        public ActionResult Index()
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/matches");
            response.EnsureSuccessStatusCode();
            List<Match> matches = response.Content.ReadAsAsync<List<Match>>().Result;
            ViewBag.Title = "All Matches";
            return View(matches);
        }

        /// <summary>
        /// IndexByGameId - Calls the web API to get a list of matches for a particular game
        /// </summary>
        /// <returns>ActionResult with a list of matches with the game ID specified</returns>
        [HttpGet]
        public ActionResult IndexByGameId(int gameId)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/matches?gameId=" + gameId.ToString());
            response.EnsureSuccessStatusCode();
            List<Match> matches = response.Content.ReadAsAsync<List<Match>>().Result;
            ViewBag.Title = "Game Matches";
            return View(matches);
        }

        /// <summary>
        /// IndexByPlayerId - Calls the web API to get a list of matches for a particular player
        /// </summary>
        /// <returns>ActionResult with a list of matches with the player ID specified</returns>
        [HttpGet]
        public ActionResult IndexByPlayerId(int playerId)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/matches?playerId=" + playerId.ToString());
            response.EnsureSuccessStatusCode();
            List<Match> matches = response.Content.ReadAsAsync<List<Match>>().Result;
            ViewBag.Title = "Player Matches";
            return View(matches);
        }

        /// <summary>
        /// Details - Calls the web API to get the details for a specific match
        /// </summary>
        /// <param name="id">Match ID to find</param>
        /// <returns>ViewResult with the specific match object</returns>
        [HttpGet]
        public ActionResult Details(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/matches?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Match match = response.Content.ReadAsAsync<Match>().Result;
            ViewBag.Title = "Match Details";
            return View(match);
        }

        /// <summary>
        /// Details - Calls the web API to get the details for a specific match for editing
        /// </summary>
        /// <param name="id">Match ID to find</param>
        /// <returns>ViewResult with the specific match object</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/matches?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Match match = response.Content.ReadAsAsync<Match>().Result;
            ViewBag.Title = "Match Details";
            return View(match);
        }

        /// <summary>
        /// Update - Calls the web API to update a specific match object
        /// </summary>
        /// <param name="match">Match object to be used for the update</param>
        /// <returns>RedirectToRouteResult to the index action after the update</returns>
        [HttpPut]
        public ActionResult Update(Match match)
        {
            ServiceRepository service = new ServiceRepository();

            // Set the update time to now
            match.UpdateTime = DateTime.Now;
            match.UpdatedBy = 1;

            // Call the web API
            HttpResponseMessage response = service.PutResponse("api/v1/games?id=" + match.Id.ToString(), match);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create - Calls the web API to add a match object
        /// </summary>
        /// <param name="match">Match object to be added</param>
        /// <returns>RedirectToRouteResult to the index action after the create</returns>
        [HttpPost]
        public ActionResult Create(Match match)
        {
            ServiceRepository service = new ServiceRepository();

            // Set the creation and update time to now
            match.CreateTime = DateTime.Now;
            match.UpdateTime = DateTime.Now;
            match.CreatedBy = 1;
            match.UpdatedBy = 1;

            HttpResponseMessage response = service.PostResponse("api/v1/matches", match);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete - Calls the web API to get the details for a specific match for deleting
        /// </summary>
        /// <param name="id">Match ID to find</param>
        /// <returns>ViewResult with the specific match object</returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/matches?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Match match = response.Content.ReadAsAsync<Match>().Result;
            ViewBag.Title = "Match Details";
            return View(match);
        }

        /// <summary>
        /// DeleteMatch - Calls the web API to delete a match object
        /// </summary>
        /// <param name="id">Match ID of the object to be deleted</param>
        /// <returns>RedirectToRouteResult to the index action after the delete</returns>
        [HttpDelete]
        public ActionResult DeleteMatch(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.DeleteResponse("api/v1/matches?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}
