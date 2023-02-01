using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyKeeper.Models
{
    [Table("Budget")]
    public class Budget:BaseEntity
    {
        public Budget()
        {
        }
        public int Id { get; set; }
        public int SpentAmount { get; set; }
        public int LimitAmount { get; set; }
        [NotMapped]
        public int RemainingAmount
        {
            get
            {
                return LimitAmount > SpentAmount ? LimitAmount - LimitAmount : 0;
            }
        }
        public int Month { get; set; }
        public int Year { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int WalletId { get; set; }
        public Wallet? Wallet { get; set; }
        public int CreatorId { get; set; }
        public User? Creator { get; set; }
    }

    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.HasIndex(b => new { b.Month, b.Year });
        }
    }
}

