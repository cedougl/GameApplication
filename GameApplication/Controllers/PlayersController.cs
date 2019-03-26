using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameApplication.Repository;
using GameApplication.Models;
using System.Net.Http;

namespace GameApplication.Controllers
{
    public class PlayersController : Controller
    {
        // GET: Players
        //[HttpGet]
        //public ActionResult Index(string sortColumnName, string orderBy, string searchString)
        //{
        //    ServiceRepository service = new ServiceRepository();

        //    string url = "";

        //    // Neither sorting or search string was specified
        //    if (string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(searchString))
        //    {
        //        url = "api/v1/players";
        //    }
        //    // Search string and sorting
        //    else if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(searchString))
        //    {
        //        url = "api/v1/players?sortColumn=" + sortColumnName + "&orderBy=" + orderBy + "&search=" + searchString;
        //    }
        //    // Sorting
        //    else if (!string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(searchString))
        //    {
        //        url = "api/v1/players?sortColumn=" + sortColumnName + "&orderBy=" + orderBy;
        //    }
        //    // Search string
        //    else if (string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(searchString))
        //    {
        //        url = "api/v1/players?searchString=" + searchString;
        //    }


        //    HttpResponseMessage response = service.GetResponse(url);
        //    response.EnsureSuccessStatusCode();
        //    List<Player> players = response.Content.ReadAsAsync<List<Player>>().Result;
        //    ViewBag.Title = "Players";

        //    return View(players);
        //}

        [HttpGet]
        public ActionResult Index(string sortColumnName, string orderBy, string searchString, int limit, int offset)
        {
            ServiceRepository service = new ServiceRepository();

            string url = "api/v1/players?sortColumn=" + sortColumnName + "&orderBy=" + orderBy + "&search=" + searchString +
                "&limit=" + limit.ToString() + "&offset=" + offset.ToString();

            // Neither sorting or search string was specified
            //if (string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(searchString))
            //{
            //    url = "api/v1/players";
            //}
            //// Search string and sorting
            //else if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(searchString))
            //{
            //    url = "api/v1/players?sortColumn=" + sortColumnName + "&orderBy=" + orderBy + "&search=" + searchString;
            //}
            //// Sorting
            //else if (!string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(searchString))
            //{
            //    url = "api/v1/players?sortColumn=" + sortColumnName + "&orderBy=" + orderBy;
            //}
            //// Search string
            //else if (string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(searchString))
            //{
            //    url = "api/v1/players?searchString=" + searchString;
            //}


            HttpResponseMessage response = service.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<Player> players = response.Content.ReadAsAsync<List<Player>>().Result;
            ViewBag.Title = "Players";

            return View(players);
        }

        // GET: Players/Details/5
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

        public ActionResult Statistics(long playerId)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/statistics?playerId=" + playerId.ToString());
            response.EnsureSuccessStatusCode();
            List<Statistic> stats = response.Content.ReadAsAsync<List<Statistic>>().Result;
            ViewBag.Title = "Player Statistics";
            return View(stats);
        }

        public ActionResult Achievements(long playerId)
        {
            ServiceRepository service = new ServiceRepository();
            HttpResponseMessage response = service.GetResponse("api/v1/achievements?playerId=" + playerId.ToString());
            response.EnsureSuccessStatusCode();
            List<Achievement> achievements = response.Content.ReadAsAsync<List<Achievement>>().Result;
            ViewBag.Title = "Player Achievements";
            return View(achievements);
        }

        // GET: Players/Edit/5
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

        [HttpPut]  
        public ActionResult Update(Player player)
        {
            ServiceRepository service = new ServiceRepository();

            player.UpdateTime = DateTime.Now;
            player.UpdatedBy = 1;

            HttpResponseMessage response = service.PutResponse("api/v1/players?id=" + player.Id.ToString(), player);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        // GET: Players/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Player player)
        {
            ServiceRepository service = new ServiceRepository();

            player.CreateTime = DateTime.Now;
            player.UpdateTime = DateTime.Now;
            player.CreatedBy = 1;
            player.UpdatedBy = 1;

            HttpResponseMessage response = service.PostResponse("api/v1/players", player);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        // GET: Players/Delete/5
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

        // GET: Players/Delete/5
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
