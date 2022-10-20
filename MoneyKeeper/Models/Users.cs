using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyKeeper.Models
{
    [Table("User")]
    public class Users
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string email { get; set; }
        public string password { get; set; }
    }
}
