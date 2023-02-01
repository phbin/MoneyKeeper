using System.Collections.Generic;
using static MoneyKeeper.Common.Enum;

namespace MoneyKeeper.Dtos
{
    public class UpdateWalletDto
    {
        public UpdateWalletDto()
        {
            Name = string.Empty;
            MemberIds = new List<int>();
            Icon = string.Empty;
        }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public int? Balance { get; set; }
        public WalletType? Type { get; set; }
        public ICollection<int>? MemberIds { get; set; }
    }
}
