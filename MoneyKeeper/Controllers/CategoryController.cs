using Microsoft.AspNetCore.Mvc;

namespace MoneyKeeper.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
