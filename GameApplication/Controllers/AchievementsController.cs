using GameApplication.Models;
using GameApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GameApplication.Controllers
{
    public class AchievementsController : Controller
    {
        // GET: Achievements
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

        // GET: Achievements/Details/5
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

        // GET: Achievements/Edit/5
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

        [HttpPut]
        public ActionResult Update(Achievement achievement)
        {
            ServiceRepository service = new ServiceRepository();

            achievement.UpdateTime = DateTime.Now;
            achievement.UpdatedBy = 1;

            HttpResponseMessage response = service.PutResponse("api/v1/achievements?id=" + achievement.Id.ToString(), achievement);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        // GET: Achievements/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Achievement achievement)
        {
            ServiceRepository service = new ServiceRepository();

            achievement.CreateTime = DateTime.Now;
            achievement.UpdateTime = DateTime.Now;
            achievement.CreatedBy = 1;
            achievement.UpdatedBy = 1;

            HttpResponseMessage response = service.PostResponse("api/v1/achievements", achievement);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        // GET: Achievements/Delete/5
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

        // GET: Achievements/Delete/5
        [HttpDelete]
        public ActionResult DeletePlayer(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.DeleteResponse("api/v1/achievements?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}
