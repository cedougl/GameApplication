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
    public class MatchesController : Controller
    {
        // GET: Matches
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

        // GET: Matches/Details/5
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

        // GET: Matches/Edit/5
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

        [HttpPut]
        public ActionResult Update(Match match)
        {
            ServiceRepository service = new ServiceRepository();

            match.UpdateTime = DateTime.Now;
            match.UpdatedBy = 1;

            HttpResponseMessage response = service.PutResponse("api/v1/games?id=" + match.Id.ToString(), match);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        // GET: Matches/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Match match)
        {
            ServiceRepository service = new ServiceRepository();

            match.CreateTime = DateTime.Now;
            match.UpdateTime = DateTime.Now;
            match.CreatedBy = 1;
            match.UpdatedBy = 1;

            HttpResponseMessage response = service.PostResponse("api/v1/matches", match);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        // GET: Matches/Delete/5
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

        // GET: Matches/Delete/5
        [HttpDelete]
        public ActionResult DeletePlayer(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.DeleteResponse("api/v1/matches?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}
