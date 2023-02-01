using MoneyKeeper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyKeeper.Services
{
    public interface IUserService
    {
        Task<User?> GetById(int id);
        Task<IEnumerable<User>> SearchByEmail(string Email);
    }
}
