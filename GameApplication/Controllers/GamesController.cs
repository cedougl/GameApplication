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
    public class GamesController : Controller
    {
        // GET: Games
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

        public ActionResult Statistics(long gameId)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/statistics?gameId=" + gameId.ToString());
            response.EnsureSuccessStatusCode();
            List<Statistic> stats = response.Content.ReadAsAsync<List<Statistic>>().Result;
            ViewBag.Title = "Game Statistics";
            return View(stats);
        }

        public ActionResult Achievements(long gameId)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/achievements?gameId=" + gameId.ToString());
            response.EnsureSuccessStatusCode();
            List<Achievement> achievements = response.Content.ReadAsAsync<List<Achievement>>().Result;
            ViewBag.Title = "Game Achievements";
            return View(achievements);
        }

        // GET: Games/Details/5
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

        // GET: Games/Edit/5
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

        // GET: Games/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Game game)
        {
            ServiceRepository service = new ServiceRepository();

            game.CreateTime = DateTime.Now;
            game.UpdateTime = DateTime.Now;
            game.CreatedBy = 1;
            game.UpdatedBy = 1;

            HttpResponseMessage response = service.PostResponse("api/v1/games", game);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        // GET: Games/Delete/5
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

        // GET: Games/Delete/5
        [HttpDelete]
        public ActionResult DeletePlayer(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.DeleteResponse("api/v1/games?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}
