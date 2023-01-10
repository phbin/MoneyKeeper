using MoneyKeeper.Models;
using System;

namespace MochiApi.Models
{
    public class Transaction
    {
        public Transaction()
        {
            Note = String.Empty;
        }
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }
        public int CreatorId { get; set; }
        public User? Creator { get; set; }
        public int WalletId { get; set; }
        public Wallet? Wallet { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime CreatedAt{ get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
    }
}
