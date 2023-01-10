using Microsoft.EntityFrameworkCore;
using MochiApi.Models;

namespace MoneyKeeper.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //this.ChangeTracker.LazyLoadingEnabled = false;
        }

        #region DbSet
        public DbSet<User> Users => Set<User>();
        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<Budget> Budgets => Set<Budget>();
        public DbSet<Event> Events => Set<Event>();
        #endregion
    }
}
