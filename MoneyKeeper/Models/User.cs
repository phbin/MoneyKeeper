using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MochiApi.Models;

namespace MoneyKeeper.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string email { get; set; }
        public string password { get; set; }
        //public Settings Settings { get; set; }
        public ICollection<Wallet> Wallets { get; set; }
        [NotMapped]
        public ICollection<WalletMember> WalletMembers { get; set; }
    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
             .HasMany(p => p.Wallets)
             .WithMany(p => p.Members)
             .UsingEntity<WalletMember>(
             r => r.HasOne(rm => rm.Wallet).WithMany(u => u.WalletMembers).HasForeignKey(rm => rm.UserId),
             r => r.HasOne(rm => rm.User).WithMany(u => u.WalletMembers).HasForeignKey(rm => rm.WalletId),
             rm =>
             {
                 rm.HasKey(t => new { t.UserId, t.WalletId });
             }
             );
        }
    }
}
