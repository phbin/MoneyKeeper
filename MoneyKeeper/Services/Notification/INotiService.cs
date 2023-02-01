using MoneyKeeper.Dtos;
using MoneyKeeper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyKeeper.Services
{

    public interface INotiService
    {
        Task<IEnumerable<Notification>> CreateListNoti(IEnumerable<CreateNotificationDto> notiDto, bool saveChanges = false);
        Task<Notification> CreateNoti(CreateNotificationDto notiDto);
        Task<IEnumerable<Notification>> GetNotifications(int userId);
    }
}
