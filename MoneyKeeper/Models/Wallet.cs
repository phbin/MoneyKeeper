using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyKeeper.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static MochiApi.Common.Enum;

namespace MochiApi.Models
{
    [Table("Wallet")]
    public class Wallet
    {
        public Wallet()
        {
            Name = string.Empty;
            Icon = string.Empty;
            Members = new List<User>();
            WalletMembers = new List<WalletMember>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Balance { get; set; }
        public bool IsDefault{ get; set; }
        public WalletType Type { get; set; }
        public ICollection<User>  Members{ get; set; }
        [NotMapped]
        public ICollection<WalletMember> WalletMembers { get; set; }

        public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
        {
            public void Configure(EntityTypeBuilder<Wallet> builder)
            {
            }
        }

    }
}
