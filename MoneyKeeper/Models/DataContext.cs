using Microsoft.EntityFrameworkCore;

namespace MoneyKeeper.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //this.ChangeTracker.LazyLoadingEnabled = false;
        }

        #region DbSet
       public DbSet<Users> Users => Set<Users>();

        #endregion
    }
}
