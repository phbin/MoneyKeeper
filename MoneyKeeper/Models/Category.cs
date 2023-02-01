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
            Icon = String.Empty;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public CategoryType Type { get; set; }
        public CategoryGroup Group { get; set; }
        public int? WalletId{get; set;}
        public Wallet? Wallet{get; set;}
    }
}
