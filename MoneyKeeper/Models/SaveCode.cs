namespace MoneyKeeper.Models
{
    public class SaveCode
    {
        public User user { get; set; }
        public string otp { get; set; }
        public SaveCode()
        {
            user = null;
            otp = string.Empty;
        }
    }
}
