using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyKeeper.Models
{
    public class Transaction
    {
        public Transaction()
        {
            Note = String.Empty;
            ParticipantIds = String.Empty;
            Participants = new List<User>();
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
        public int? EventId { get; set; }
        public Event? Event { get; set; }
        [Required]
        public DateTime CreatedAt{ get; set; }
        public string? Image { get; set; }
        public string ParticipantIds { get; set; }
        [NotMapped]
        public List<User> Participants { get; set; }
    }
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("UTC_TIMESTAMP()");
        }
    }
}
