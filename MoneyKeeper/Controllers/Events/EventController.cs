using Microsoft.AspNetCore.Mvc;

namespace MoneyKeeper.Controllers.Events
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
