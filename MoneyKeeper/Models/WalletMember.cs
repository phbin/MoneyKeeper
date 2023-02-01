using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static MoneyKeeper.Common.Enum;

namespace MoneyKeeper.Models
{
    [Table("WalletMember")]
    public class WalletMember
    {
        public WalletMember()
        {
            Role = MemberRole.Member;
            JoinAt = DateTime.UtcNow;
            Status = MemberStatus.Pending;
        }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int WalletId { get; set; }
        public MemberRole Role { get; set; }
        public Wallet? Wallet { get; set; }
        public DateTime JoinAt { get; set; }
        public MemberStatus Status { get; set; }
    }
}
