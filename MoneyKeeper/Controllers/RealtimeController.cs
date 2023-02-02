using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MoneyKeeper.Dtos;
using MoneyKeeper.Hubs;
using MoneyKeeper.Models;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MoneyKeeper.Controllers
{
    [ApiController]
    [Route("realtime")]
    public class RealtimeController : Controller
    {
        public DataContext _context { get; set; }
        public IMapper _mapper { get; set; }
        IHubContext<NotiHub> _notiHub;
        public RealtimeController(DataContext context, IMapper mapper, IHubContext<NotiHub> notiHub)
        {
            _context = context;
            _mapper = mapper;
            _notiHub = notiHub;
        }

        [HttpGet]
        public async Task<IActionResult> Invoke()
        {
            var userids = await _context.Users.Select(u => u.Id.ToString()).ToListAsync();

            await _notiHub.Clients.Users(userids).SendAsync("Notification",
                new NotificationDto
                {
                    Id = 1,
                    BudgetId = 1,
                    Description = Helper.NotiTemplate.GetRemindBudgetExceedLimit("Test", 1, 2023),
                    CreatedAt = DateTime.UtcNow,
                    Type = Common.Enum.NotificationType.BudgetExceed,
                    UserId = 1,
                    Wallet = new WalletDto { Id = 12, Name = "Test" },
                    WalletId = 12
                }
            );

            return Ok();
        }
    }
}
