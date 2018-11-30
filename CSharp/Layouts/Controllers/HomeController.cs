using Microsoft.AspNetCore.Mvc;

namespace Layouts.Controllers {
    public class HomeController : Controller {
        public IActionResult Index () {
            return View ();
        }
    }
}