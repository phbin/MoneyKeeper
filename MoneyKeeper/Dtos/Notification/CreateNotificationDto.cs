using MoneyKeeper.Models;
using System.ComponentModel.DataAnnotations;
using static MoneyKeeper.Common.Enum;
using static System.Net.WebRequestMethods;

namespace MoneyKeeper.Dtos
{
    public class CreateNotificationDto
    {
        public CreateNotificationDto()
        {
            Description = null!;
        }
        public string Description { get; set; }
        public NotificationType Type { get; set; }
        public int UserId{ get; set; }
        public int? WalletId { get; set; }
        public int? TransactionId { get; set; }
        public int? BudgetId { get; set; }
    }
}
