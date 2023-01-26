using AutoMapper;
using Microsoft.Extensions.Logging;
using MoneyKeeper.Dtos;
using MoneyKeeper.Models;

namespace MoneyKeeper.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<LoginUserDto, User>();
        }

    }
}
