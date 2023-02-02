using System.ComponentModel.DataAnnotations;
using System;

namespace MoneyKeeper.Dtos
{
    public class UpdateEventDto
    {
        public UpdateEventDto()
        {
            Name = null!;
            Icon = null!;
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Icon { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
