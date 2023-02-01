using static MoneyKeeper.Common.Enum;
using System;

namespace MoneyKeeper.Models
{
    public class Invitation
    {
        public Invitation()
        {
            CreatedAt = DateTime.UtcNow;
            Status = InvitationStatus.New;
        }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int WalletId { get; set; }
        public Wallet? Wallet { get; set; }
        public int SenderId { get; set; }
        public User? Sender { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public InvitationStatus Status { get; set; }
    }
}
