using System.Collections.Generic;
using static MoneyKeeper.Common.Enum;

namespace MoneyKeeper.Dtos
{
    public class WalletDto
    {
        public WalletDto()
        {
            Name = string.Empty;
            Icon = string.Empty;
            Members = new List<UserDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Balance { get; set; }
        public bool IsDefault { get; set; }
        public WalletType Type { get; set; }
        public ICollection<UserDto> Members { get; set; }
        public ICollection<WalletMemberDto> WalletMembers
        { get; set; }

    }
}
