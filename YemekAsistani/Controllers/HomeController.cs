using Microsoft.AspNetCore.Mvc;

namespace YemekAsistani.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Musics()
        {
            return View();
        }
    }
}