namespace MoneyKeeper.Models
{
    public class SaveCode
    {
        public Users user { get; set; }
        public string otp { get; set; }
        public SaveCode()
        {
            user = null;
            otp = string.Empty;
        }
    }
}
