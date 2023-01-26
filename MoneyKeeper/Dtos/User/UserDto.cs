using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public UserDto()
        {
            Email = string.Empty;
            Token = string.Empty;
        }
    }
}
