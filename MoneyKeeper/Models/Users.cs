using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyKeeper.Models
{
    public class Users
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
