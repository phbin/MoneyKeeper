using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static MochiApi.Common.Enum;

namespace MochiApi.Models
{
    [Table("WalletMember")]
    [Keyless]
    public class WalletMember
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int WalletId { get; set; }
        public MemberRole Role { get; set; }
        public Wallet? Wallet { get; set; }
        public DateTime JoinAt { get; set; }

    }
}
