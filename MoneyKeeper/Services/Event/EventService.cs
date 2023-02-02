using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoneyKeeper.Data;
using MoneyKeeper.Dtos;
using MoneyKeeper.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoneyKeeper.Common.Enum;

namespace MoneyKeeper.Services.Event
{
    public class EventService:IEventService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public DataContext _context { get; set; }

        public EventService(IConfiguration configuration, DataContext context, IMapper mapper)
        {
            _configuration = configuration;
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Models.Event>> GetEvents(int userId)
        {
            var @events = await _context.Events.Include(e => e.Wallet).Where(e =>
          (e.WalletId != null && e.Wallet!.WalletMembers.Any(wM => wM.UserId == userId && wM.Status == Common.Enum.MemberStatus.Accepted))
          || (e.WalletId == null && e.CreatorId == userId)
          ).ToListAsync();

            return @events;
        }
        public async Task<IEnumerable<Models.Event>> GetEventsOfWallet(int userId, int walletId)
        {
            var @events = await _context.Events.AsNoTracking().Where(e =>
          (e.WalletId == walletId && e.Wallet!.WalletMembers.Any(wM => wM.UserId == userId && wM.Status == MemberStatus.Accepted))
          || (e.WalletId == null && e.CreatorId == userId)
          ).Include(e => e.Wallet).ToListAsync();

            return @events;
        }
        public async Task<Models.Event?> GetEventById(int id, int userId)
        {
            var @event = await _context.Events.AsNoTracking()
            .Where(e => e.Id == id && (e.Wallet!.WalletMembers.Any(wM => wM.UserId == userId && wM.Status == Common.Enum.MemberStatus.Accepted))
          || (e.WalletId == null && e.CreatorId == userId)
          ).Include(e => e.Wallet).FirstOrDefaultAsync();

            return @event;
        }
        public async Task<IEnumerable<Transaction>> GetTransactionOfEvent(int id)
        {
            var trans = await _context.Transactions.AsNoTracking().Where(b => b.EventId != id).OrderByDescending(tr => tr.CreatedAt)
            .ToListAsync();

            return trans;
        }

        public async Task<Models.Event> CreateEvent(int userId, CreateEventDto createEventDto)
        {
            var @event = _mapper.Map<Models.Event>(createEventDto);

            @event.CreatorId = userId;
            await _context.Events.AddAsync(@event);

            await _context.SaveChangesAsync();
            return @event;
        }
        public async Task<Models.Event> UpdateEvent(int userId, int eventId, UpdateEventDto updateDto)
        {
            var @event = await _context.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if (@event == null)
            {
                throw new ApiException("Event not found", 400);
            }
            _mapper.Map(updateDto, @event);

            await _context.SaveChangesAsync();
            return @event;
        }

        public async Task<Models.Event> DeleteEvent(int userId, int eventId)
        {
            var @event = await _context.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if (@event == null)
            {
                throw new ApiException("Event not found", 400);
            }
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return @event;
        }

        public async Task ToggleEventFinish(int userId, int eventId)
        {
            var @event = await _context.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if (@event == null)
            {
                throw new ApiException("Event not found", 400);
            }
            @event.IsFinished = !@event.IsFinished;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateEventSpent(int eventId)
        {
            var @event = await _context.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if (@event == null)
            {
                throw new ApiException("Event not found", 400);
            }
            long amount = await _context.Transactions.AsNoTracking().Include(t => t.Category)
            .Where(t => t.EventId == eventId)
            .Select(t => t.Category!.Type == CategoryType.Income ? t.Amount : -1 * t.Amount).
            SumAsync();
            @event.SpentAmount = amount;
        }

        public async Task<bool> CheckEventIsInWallet(int eventId, int walletId, int creatorId)
        {
            var @event = await _context.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if (@event == null)
            {
                return false;
            }
            return @event.WalletId.HasValue ? @event.WalletId == walletId : @event.CreatorId == creatorId;
        }
    }
}
