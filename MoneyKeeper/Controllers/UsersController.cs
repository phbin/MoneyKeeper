using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyKeeper.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : Controller
    {
        public DataContext _context { get; set; }
        public UsersController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("list-users")]
        public async Task<IActionResult> GetUser()
        {
            return Ok(new
            {
                data = await _context.Users.OrderBy(u => u.id).ToListAsync()
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByID(string id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.id == Guid.Parse(id));
                if(user == null) return NotFound();
                return Ok(user);
            }
            catch
            {
                 return BadRequest();
            }
        }
    }
}
