namespace MoneyKeeper.Helper
{
    public class NotiTemplate
    {
        public static string GetRemindBudgetExceedLimit(string budgetName, int month, int year)
        {
            return $"Ngân sách {budgetName} đã vượt quá giá trị tối đa của tháng {month}-{year}";
        }
        public static string GetInvitationContent(string walletName)
        {
            return $"Bạn vừa được mời tham gia vào ví {walletName}";
        }
    }
}
