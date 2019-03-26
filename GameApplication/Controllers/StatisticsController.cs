using GameApplication.Repository;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using GameApplication.Models;

namespace GameApplication.Controllers
{
    /// <summary>
    /// StatisticsController - Controller for all statistics requests
    /// </summary>
    public class StatisticsController : Controller
    {
        /// <summary>
        /// Calls the web API to get all statistics records and returns them to the view
        /// </summary>
        /// <returns>ViewResult with a list of statistics</returns>
        [HttpGet]
        public ActionResult Index()
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/statistics");
            response.EnsureSuccessStatusCode();
            List<Statistic> stats = response.Content.ReadAsAsync<List<Statistic>>().Result;
            ViewBag.Title = "All Statistics";
            return View(stats);
        }

        /// <summary>
        /// Calls the web API to get all statistics for a particular game
        /// </summary>
        /// <param name="gameId">Game ID to find</param>
        /// <returns>ViewResult with a list of statistics for the specific game ID</returns>
        [HttpGet]
        public ActionResult IndexByGameId(int gameId)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/statistics?gameId=" + gameId.ToString());
            response.EnsureSuccessStatusCode();
            List<Statistic> stats = response.Content.ReadAsAsync<List<Statistic>>().Result;
            ViewBag.Title = "Game Statistics";
            return View(stats);
        }

        /// <summary>
        /// Calls the web API to get all statistics for a particular player
        /// </summary>
        /// <param name="gameId">Player ID to find</param>
        /// <returns>ViewResult with a list of statistics for the specific player ID</returns>
        [HttpGet]
        public ActionResult IndexByPlayerId(int playerId)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/statistics?playerId=" + playerId.ToString());
            response.EnsureSuccessStatusCode();
            List<Statistic> stats = response.Content.ReadAsAsync<List<Statistic>>().Result;
            ViewBag.Title = "Player Statistics";
            return View(stats);
        }

        /// <summary>
        /// Calls the web API to get the statistic by ID
        /// </summary>
        /// <param name="id">Statistic ID to find</param>
        /// <returns>ViewResult with a statistic object for the specific ID</returns>
        [HttpGet]
        public ActionResult Details(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/statistics?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Statistic stat = response.Content.ReadAsAsync<Statistic>().Result;
            ViewBag.Title = "Statistic Details";
            return View(stat);
        }

        /// <summary>
        /// Calls the web API to get the statistic by ID for editing
        /// </summary>
        /// <param name="id">Statistic ID to find</param>
        /// <returns>ViewResult with a statistic object for the specific ID</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/statistics?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Statistic stat = response.Content.ReadAsAsync<Statistic>().Result;
            ViewBag.Title = "Statistic Details";
            return View(stat);
        }

        /// <summary>
        /// Calls the web API to update the statistic object
        /// </summary>
        /// <param name="stat">Statistic object to be used for the update</param>
        /// <returns>RedirectToRouteResult to the Index action once the update was successful</returns>
        [HttpPut]
        public ActionResult Update(Statistic stat)
        {
            ServiceRepository service = new ServiceRepository();

            stat.UpdateTime = DateTime.Now;
            stat.UpdatedBy = 1;

            HttpResponseMessage response = service.PutResponse("api/v1/statistics?id=" + stat.Id.ToString(), stat);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Calls the web API to add a new Statistic
        /// </summary>
        /// <param name="stat">Statistic object to be created</param>
        /// <returns>RedirectToRouteResult to the Index action after successful creation</returns>
        [HttpPost]
        public ActionResult Create(Statistic stat)
        {
            ServiceRepository service = new ServiceRepository();

            // Set the creation and update time to now
            stat.CreateTime = DateTime.Now;
            stat.UpdateTime = DateTime.Now;
            // Hard code the CreatedBy and UpdatedBy user ID for now, until we add user authentication
            stat.CreatedBy = 1;
            stat.UpdatedBy = 1;

            // Call the web API
            HttpResponseMessage response = service.PostResponse("api/v1/statistics", stat);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Calls the web API to get a particular Statisic for deletion
        /// </summary>
        /// <param name="id">Statistic ID to find</param>
        /// <returns>ViewResult with the specific Statistic object</returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/statistics?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Statistic stat = response.Content.ReadAsAsync<Statistic>().Result;
            ViewBag.Title = "Statistic Details";
            return View(stat);
        }

        /// <summary>
        /// Calls the web API to deletes a statistic object
        /// </summary>
        /// <param name="id">ID of the statistic object to be deleted</param>
        /// <returns>RedirectToRouteResult to the Index action after the delete</returns>
        [HttpDelete]
        public ActionResult DeleteStatistic(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.DeleteResponse("api/v1/statistics?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}
