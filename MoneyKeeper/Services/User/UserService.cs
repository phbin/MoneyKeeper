using MoneyKeeper.Models;
using System.Threading.Tasks;

namespace MoneyKeeper.Services
{
    public class UserService : IUserService
    {
        private DataContext _context { get; set; }
        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
