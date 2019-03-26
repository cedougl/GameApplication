using System.Web.Mvc;

namespace GameApplication.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Home page
        /// </summary>
        /// <returns>ViewResult</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// About view
        /// </summary>
        /// <returns>ViewResult</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Contact view
        /// </summary>
        /// <returns>ViewResult</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}