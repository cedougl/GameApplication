using GameApplication.Models;
using GameApplication.Repository;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace GameApplication.Controllers
{
    /// <summary>
    /// GamesController - Controller for all game related requests
    /// </summary>
    public class GamesController : Controller
    {
        /// <summary>
        /// Calls the web API to get a list of games
        /// </summary>
        /// <returns>ViewResult with the list of games</returns>
        [HttpGet]
        public ActionResult Index()
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/games");
            response.EnsureSuccessStatusCode();
            List<Game> games = response.Content.ReadAsAsync<List<Game>>().Result;
            ViewBag.Title = "All Games";
            return View(games);
        }

        /// <summary>
        /// Calls web API to get a specific game object
        /// </summary>
        /// <param name="id">Game ID to find</param>
        /// <returns>ViewResult with specific game object</returns>
        [HttpGet]
        public ActionResult Details(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/games?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Game game = response.Content.ReadAsAsync<Game>().Result;
            ViewBag.Title = "Game Details";
            return View(game);
        }

        /// <summary>
        /// Calls web API to get a specific game object for editing
        /// </summary>
        /// <param name="id">Game ID to find</param>
        /// <returns>ViewResult with specific game object</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/games?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Game game = response.Content.ReadAsAsync<Game>().Result;
            ViewBag.Title = "Player Details";
            return View(game);
        }

        /// <summary>
        /// Calls the web API to update a game object
        /// </summary>
        /// <param name="game">Game object to be used for the update</param>
        /// <returns>RedirectToRouteResult to the index action after the update</returns>
        [HttpPut]
        public ActionResult Update(Game game)
        {
            ServiceRepository service = new ServiceRepository();

            game.UpdateTime = DateTime.Now;
            game.UpdatedBy = 1;

            HttpResponseMessage response = service.PutResponse("api/v1/games?id=" + game.Id.ToString(), game);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Calls the web API to add a game object
        /// </summary>
        /// <param name="game">Game object to be added</param>
        /// <returns>RedirectToRouteResult to the index action after the create</returns>
        [HttpPost]
        public ActionResult Create(Game game)
        {
            ServiceRepository service = new ServiceRepository();

            // Set the creation and update time to now
            game.CreateTime = DateTime.Now;
            game.UpdateTime = DateTime.Now;
            game.CreatedBy = 1;
            game.UpdatedBy = 1;

            HttpResponseMessage response = service.PostResponse("api/v1/games", game);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Calls web API to get a specific game object for deletion
        /// </summary>
        /// <param name="id">Game ID to find</param>
        /// <returns>ViewResult with specific game object</returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/games?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Game game = response.Content.ReadAsAsync<Game>().Result;
            ViewBag.Title = "Game Details";
            return View(game);
        }

        /// <summary>
        /// Calls the web API to delete a game
        /// </summary>
        /// <param name="id">Game ID of the object to be deleted</param>
        /// <returns>RedirectToRouteResult to the index action after the delete</returns>
        [HttpDelete]
        public ActionResult DeleteGame(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.DeleteResponse("api/v1/games?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}
