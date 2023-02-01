namespace MoneyKeeper.Dtos.User
{
    public class BasicUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public BasicUserDto()
        {
            Email = string.Empty;
        }
    }
}
