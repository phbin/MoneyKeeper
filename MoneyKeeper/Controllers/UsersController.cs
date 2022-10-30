using Microsoft.AspNetCore.Mvc;

namespace MoneyKeeper.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
