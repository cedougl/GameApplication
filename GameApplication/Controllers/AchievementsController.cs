using GameApplication.Models;
using GameApplication.Repository;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace GameApplication.Controllers
{
    /// <summary>
    /// AchievementsController - Controller for all achievement related requests
    /// </summary>
    public class AchievementsController : Controller
    {
        /// <summary>
        /// Calls the web API to get a list of achievements
        /// </summary>
        /// <returns>ActionResult with the list of achievements</returns>
        [HttpGet]
        public ActionResult Index()
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/achievements");
            response.EnsureSuccessStatusCode();
            List<Achievement> achievements = response.Content.ReadAsAsync<List<Achievement>>().Result;
            ViewBag.Title = "All Achievements";
            return View(achievements);
        }

        /// <summary>
        /// Calls the web API to get a list of achievements for a particular game
        /// </summary>
        /// <returns>ActionResult with the list of achievements for the specific game ID</returns>
        [HttpGet]
        public ActionResult IndexByGameId(int gameId)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/achievements?gameId=" + gameId.ToString());
            response.EnsureSuccessStatusCode();
            List<Achievement> achievements = response.Content.ReadAsAsync<List<Achievement>>().Result;
            ViewBag.Title = "Game Achievements";
            return View(achievements);
        }

        /// <summary>
        /// Calls the web API to get a list of achievements for a particular player
        /// </summary>
        /// <returns>ActionResult with the list of achievements for the specific game ID</returns>
        [HttpGet]
        public ActionResult IndexByPlayerId(int playerId)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/achievements?playerId=" + playerId.ToString());
            response.EnsureSuccessStatusCode();
            List<Achievement> achievements = response.Content.ReadAsAsync<List<Achievement>>().Result;
            ViewBag.Title = "Player Achievements";
            return View(achievements);
        }

        /// <summary>
        /// Calls the web API to get the details for a specific achievement
        /// </summary>
        /// <param name="id">Achievement ID to find</param>
        /// <returns>ViewResult for the specific achievement</returns>
        [HttpGet]
        public ActionResult Details(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/achievements?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Achievement achievement = response.Content.ReadAsAsync<Achievement>().Result;
            ViewBag.Title = "Achievement Details";
            return View(achievement);
        }

        /// <summary>
        /// Calls the web API to get the details for a specific achievement for editing
        /// </summary>
        /// <param name="id">Achievement ID to find</param>
        /// <returns>ViewResult for the specific achievement</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/achievements?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Achievement achievement = response.Content.ReadAsAsync<Achievement>().Result;
            ViewBag.Title = "Achievement Details";
            return View(achievement);
        }

        /// <summary>
        /// Calls the web API to update an achievement object
        /// </summary>
        /// <param name="achievement">Achievement object to be used for the update</param>
        /// <returns>RedirectToRouteResult to the index action after the update</returns>
        [HttpPut]
        public ActionResult Update(Achievement achievement)
        {
            ServiceRepository service = new ServiceRepository();

            // Set the update time to now
            achievement.UpdateTime = DateTime.Now;
            achievement.UpdatedBy = 1;

            HttpResponseMessage response = service.PutResponse("api/v1/achievements?id=" + achievement.Id.ToString(), achievement);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Calls the web API to update an achievement object
        /// </summary>
        /// <param name="achievement">Achievement object to be added</param>
        /// <returns>RedirectToRouteResult to the index action after the create</returns>
        [HttpPost]
        public ActionResult Create(Achievement achievement)
        {
            ServiceRepository service = new ServiceRepository();

            // Set the creation and update time to now
            achievement.CreateTime = DateTime.Now;
            achievement.UpdateTime = DateTime.Now;
            achievement.CreatedBy = 1;
            achievement.UpdatedBy = 1;

            HttpResponseMessage response = service.PostResponse("api/v1/achievements", achievement);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Calls the web API to get the details for a specific achievement for deletion
        /// </summary>
        /// <param name="id">Achievement ID to find</param>
        /// <returns>ViewResult for the specific achievement</returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/achievements?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Achievement achievement = response.Content.ReadAsAsync<Achievement>().Result;
            ViewBag.Title = "Achievement Details";
            return View(achievement);
        }

        /// <summary>
        /// Calls the web API to delete an achievement
        /// </summary>
        /// <param name="id">Achievement ID of the object to be deleted</param>
        /// <returns>RedirectToRouteResult to the index action after the delete</returns>
        [HttpDelete]
        public ActionResult DeleteAchievement(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.DeleteResponse("api/v1/achievements?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}
