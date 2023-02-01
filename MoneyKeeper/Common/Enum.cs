using System.Text.Json.Serialization;

namespace MoneyKeeper.Common
{
    public static class Enum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum CategoryType
        {
            Income,
            Expense
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum CategoryGroup
        {
            Income,
            RequiredExpense,
            NecessaryExpense,
            Entertainment,
            InvestingOrDebt,
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum WalletType
        {
            Personal,
            Group
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum MemberRole
        {
            Admin,
            Member
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Mode
        {
            Light,
            Dark
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum NotificationType
        {
            BudgetExceed,
            Reminder,
            JoinWalletInvitation
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum InvitationStatus
        {
            New,
            Accepted,
            Declined
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum InvitationAction
        {
            Accept,
            Decline
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum MemberStatus
        {
            Accepted,
            Declined,
            Pending
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum WalletAction
        {
            Invite,
            Remove
        }
    }
}