using System;

namespace MoneyKeeper.Dtos.Statistic
{
    public class DailyReport
    {
        public DateTime Date { get; set; }
        public long Income { get; set; }
        public long Expense { get; set; }
    }
}
