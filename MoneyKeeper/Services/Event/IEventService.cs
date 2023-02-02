using MoneyKeeper.Dtos;
using MoneyKeeper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyKeeper.Services.Event
{
    public interface IEventService
    {
        Task<Models.Event> CreateEvent(int userId, CreateEventDto createEventDto);
        Task<Models.Event> DeleteEvent(int userId, int eventId);
        Task<Models.Event?> GetEventById(int id, int userId);
        Task<IEnumerable<Models.Event>> GetEvents(int userId);
        Task<IEnumerable<Models.Event>> GetEventsOfWallet(int userId, int walletId);
        Task<IEnumerable<Transaction>> GetTransactionOfEvent(int id);
        Task ToggleEventFinish(int userId, int eventId);
        Task<Models.Event> UpdateEvent(int userId, int eventId, UpdateEventDto updateDto);
        Task UpdateEventSpent(int eventId);
        Task<bool> CheckEventIsInWallet(int eventId, int walletId, int creatorId);

    }
}
