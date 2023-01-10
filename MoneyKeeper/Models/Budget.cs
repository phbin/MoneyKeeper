using MoneyKeeper.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MochiApi.Models
{
    [Table("Budget")]
    public class Budget
    {
        public Budget()
        {
            Name = string.Empty;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int SpentAmount{ get; set; }
        public int LimitAmount{ get; set; }
        public int Month{ get; set; }
        public int Year{ get; set; }
        public int CategoryId{ get; set; }
        public Category? Category{ get; set; }
        public int WalletId { get; set; }
        public Wallet? Wallet { get; set; }
        public int CreatorId{ get; set; }
        public User? Creator { get; set; }
    }
}
