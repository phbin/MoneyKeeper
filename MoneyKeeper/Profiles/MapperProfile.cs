using AutoMapper;
using Microsoft.Extensions.Logging;
using MoneyKeeper.Data.Users;
using MoneyKeeper.Models;

namespace MoneyKeeper.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Data.Users.Users, Models.Users>();
            CreateMap<Models.Users, Data.Users.Users>();
        }

    }
}
