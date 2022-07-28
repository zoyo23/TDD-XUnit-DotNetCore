using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
