using GameApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using GameApplication.Models;

namespace GameApplication.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
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

        // GET: Statistics/Details/5
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

        // GET: Statistics/Edit/5
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

        // GET: Statistics/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Statistic stat)
        {
            ServiceRepository service = new ServiceRepository();

            stat.CreateTime = DateTime.Now;
            stat.UpdateTime = DateTime.Now;
            stat.CreatedBy = 1;
            stat.UpdatedBy = 1;

            HttpResponseMessage response = service.PostResponse("api/v1/statistics", stat);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        // GET: Statistics/Delete/5
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

        // GET: Statistics/Delete/5
        [HttpDelete]
        public ActionResult DeletePlayer(int id)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.DeleteResponse("api/v1/statistics?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}
