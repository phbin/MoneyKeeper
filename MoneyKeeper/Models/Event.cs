using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyKeeper.Models
{
    [Table("Event")]
    public class Event:BaseEntity
    {
        public Event()
        {
            Name = String.Empty;
            Icon = String.Empty;
            Transactions = new List<Transaction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public long SpentAmount { get; set; }
        public DateTime? EndDate{ get; set; }
        public int CreatorId { get; set; }
        public User? Creator { get; set; }
        public int? WalletId { get; set; }
        public Wallet? Wallet { get; set; }
        public bool IsFinished { get; set; }
        public ICollection<Transaction> Transactions{ get; set; }
    }
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
        }
    }
}
