using MoneyKeeper.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static MochiApi.Common.Enum;

namespace MochiApi.Models
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            Name = String.Empty;
            Icon = String.Empty;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public CategoryType Type { get; set; }
        public int WalletId{get; set;}
        public Wallet? Wallet{get; set;}
        public int CreatorId{get; set;}
        public User? Creator{get; set;}
    }
}
