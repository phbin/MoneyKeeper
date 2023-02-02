using System.ComponentModel.DataAnnotations;
using System;

namespace MoneyKeeper.Dtos
{
    public class CreateEventDto
    {
        public CreateEventDto()
        {
            Name = null!;
            Icon = null!;
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Icon { get; set; }
        public DateTime? EndDate { get; set; }
        public int? WalletId { get; set; }
    }
}
