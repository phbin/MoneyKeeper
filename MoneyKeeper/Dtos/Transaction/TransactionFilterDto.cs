using System;

namespace MoneyKeeper.Dtos.Transaction
{
    public class TransactionFilterDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CategoryId { get; set; }
        public int? EventId { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
