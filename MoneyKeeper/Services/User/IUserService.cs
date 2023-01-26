using MoneyKeeper.Models;
using System.Threading.Tasks;

namespace MoneyKeeper.Services
{
    public interface IUserService
    {
        Task<User?> GetById(int id);
    }
}
