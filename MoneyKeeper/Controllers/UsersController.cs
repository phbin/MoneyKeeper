using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Attributes;
using MoneyKeeper.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyKeeper.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        public DataContext _context { get; set; }
        public UserController(DataContext context)
        {
            _context = context;
        }

        [Protect]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return Ok(new
            {
                data = await _context.Users.OrderBy(u => u.Id).ToListAsync()
            });
        }
    }
}
