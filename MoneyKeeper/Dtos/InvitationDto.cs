using static MoneyKeeper.Common.Enum;
using System;

namespace MoneyKeeper.Dtos
{
    public class InvitationDto
    {
        public InvitationDto()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int WalletId { get; set; }
        public WalletDto? Wallet { get; set; }
        public int SenderId { get; set; }
        public UserDto? Sender { get; set; }
        public int UserId { get; set; }
        public UserDto? User { get; set; }
        public InvitationStatus Status { get; set; }
    }
}
