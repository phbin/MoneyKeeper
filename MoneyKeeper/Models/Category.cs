using MoneyKeeper.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static MoneyKeeper.Common.Enum;

namespace MoneyKeeper.Models
{
    [Table("Category")]
    public class Category
    {

        public Category()
        {
            Name = String.Empty;
            Icon = null;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Icon { get; set; }
        public CategoryType Type { get; set; }
        public int WalletId{get; set;}
        public Wallet? Wallet{get; set;}
        public int CreatorId{get; set;}
        public User? Creator{get; set;}
    }
}
