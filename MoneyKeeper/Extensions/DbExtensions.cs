using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MoneyKeeper.Models;
using System;

namespace MoneyKeeper.Extensions
{
    public static class DbExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new List<User>
            {
                new User{ Id = 1, Email = "test@gmail.com", Password = "123123123" },
                new User{ Id = 2, Email = "test2@gmail.com", Password = "123123123"},
                new User{ Id = 3, Email = "test3@gmail.com", Password = "123123123"},
                new User{ Id = 4, Email = "test4@gmail.com", Password = "123123123"},
            });

            var date = new DateTime(2022, 12, 29);
            modelBuilder.Entity<Wallet>().HasData(new List<Wallet>
            {
                new Wallet{ Id = 1, Type = Common.Enum.WalletType.Personal, IsDefault = true, Balance = 100000, Name = "Ví"},
                new Wallet{ Id = 2, Type = Common.Enum.WalletType.Personal, IsDefault = true, Balance = 200000, Name = "Ví"},
                new Wallet{ Id = 3, Type = Common.Enum.WalletType.Personal, IsDefault = true, Balance = 300000, Name = "Ví"},
                new Wallet{ Id = 4, Type = Common.Enum.WalletType.Personal, IsDefault = true, Balance = 400000, Name = "Ví"},
            });

            modelBuilder.Entity<WalletMember>().HasData(
                new List<WalletMember>
                {
                    new WalletMember
                    {
                        Role = Common.Enum.MemberRole.Admin,
                        JoinAt  = date,
                        UserId = 1,
                        WalletId = 1
                    },
                    new WalletMember
                    {
                        Role = Common.Enum.MemberRole.Admin,
                        JoinAt  = date,
                        UserId = 2,
                        WalletId = 2
                    },
                    new WalletMember
                    {
                        Role = Common.Enum.MemberRole.Admin,
                        JoinAt  = date,
                        UserId = 3,
                        WalletId = 3
                    },
                    new WalletMember
                    {
                        Role = Common.Enum.MemberRole.Admin,
                        JoinAt  = date,
                        UserId = 4,
                        WalletId = 4
                    },
                }
                );

        }
    }
}
