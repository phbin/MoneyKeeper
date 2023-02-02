using System.Collections.Generic;
using System;

namespace MoneyKeeper.Dtos.Transaction
{
    public class TransactionGroupDateDto
    {
        public TransactionGroupDateDto()
        {
            Transactions = new List<TransactionDto>();
        }
        public DateTime Date { get; set; }
        public long Revenue { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}
