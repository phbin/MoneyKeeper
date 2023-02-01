using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MoneyKeeper.Dtos;
using MoneyKeeper.Dtos.Auth;
using MoneyKeeper.Error;
using MoneyKeeper.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyKeeper.Services
{
    public class NotiService : INotiService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public DataContext _context { get; set; }

        public NotiService(IConfiguration configuration, DataContext context, IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Notification>> GetNotifications(int userId)
        {
            var notis = await _context.Notifcations.Where(no => no.UserId == userId)
            .OrderByDescending(c => c.CreatedAt).Include(c => c.Wallet).ToListAsync();

            return notis;
        }
        public async Task<Notification> CreateNoti(CreateNotificationDto notiDto)
        {
            var noti = _mapper.Map<Notification>(notiDto);

            await _context.Notifcations.AddAsync(noti);

            await _context.SaveChangesAsync();
            return noti;
        }
        public async Task<IEnumerable<Notification>> CreateListNoti(IEnumerable<CreateNotificationDto> notiDto, bool saveChanges = false)
        {
            var notis = _mapper.Map<IEnumerable<Notification>>(notiDto);

            await _context.Notifcations.AddRangeAsync(notis);
            if (saveChanges)
            {
                await _context.SaveChangesAsync();
            }

            return notis;
        }
    }
}
