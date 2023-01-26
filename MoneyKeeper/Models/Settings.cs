using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static MoneyKeeper.Common.Enum;

namespace MoneyKeeper.Models
{
    public class Settings
    {
        public Settings()
        {
            Language = "vn";
        }
        public int Id { get; set; }
        public string Language { get; set; }
        public Mode Mode { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }

    public class SettingsConfiguration : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.HasOne(w => w.User).WithOne(u => u.Settings).HasForeignKey<Settings>(u => u.UserId);
        }
    }
}
