using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace MoneyKeeper.Dtos.Transaction
{
    public class UpdateTransactionDto
    {
        public UpdateTransactionDto()
        {
            Note = String.Empty;
            ParticipantIds = new List<int>();
        }
        public int Amount { get; set; }
        [Required]
        public string Note { get; set; }
        public int CategoryId { get; set; }
        public int? EventId { get; set; }
        public string? Image { get; set; }
        [Required]
        public DateTime? CreatedAt { get; set; }
        public List<int> ParticipantIds { get; set; }

        public DateTime CreateAtValue
        {
            get
            {
                if (!CreatedAt.HasValue)
                {
                    return DateTime.UtcNow;
                }
                return (DateTime)CreatedAt!;
            }
        }
    }
}
