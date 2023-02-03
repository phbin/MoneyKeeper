using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Attributes;
using MoneyKeeper.Data;
using MoneyKeeper.Dtos.User;
using MoneyKeeper.Models;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MoneyKeeper.Controllers.Users
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        public DataContext _context { get; set; }
        public IMapper _mapper { get; set; }
        public UserController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("search")]
        public async Task<IActionResult> GetUser([EmailAddress] string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

            var userDto = _mapper.Map<BasicUserDto>(user);
            return Ok(new ApiResponse<BasicUserDto>(userDto, "search users by email"));
        }
    }
}
