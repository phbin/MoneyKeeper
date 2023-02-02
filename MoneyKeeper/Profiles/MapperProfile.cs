using AutoMapper;
using Microsoft.Extensions.Logging;
using MoneyKeeper.Dtos;
using MoneyKeeper.Dtos.Budget;
using MoneyKeeper.Dtos.Category;
using MoneyKeeper.Dtos.Transaction;
using MoneyKeeper.Dtos.User;
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
            CreateMap<User, BasicUserDto>(); 
            
            CreateMap<CreateCategoryDto, Category>();          
            CreateMap<UpdateCategoryDto, Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
            CreateMap<Category, CategoryDto>();

            CreateMap<Wallet, WalletDto>();
            CreateMap<CreateWalletDto, Wallet>();
            CreateMap<UpdateWalletDto, Wallet>();
            CreateMap<WalletMember, WalletMemberDto>();

            CreateMap<Transaction, TransactionDto>();
            //.ForMember(t => t.Pa);
            CreateMap<CreateTransactionDto, Transaction>();
            CreateMap<UpdateTransactionDto, Transaction>();

            CreateMap<Budget, BudgetDto>();
            CreateMap<CreateBudgetDto, Budget>();
            CreateMap<UpdateBudgetDto, Budget>();

            CreateMap<CreateNotificationDto, Notification>();
            CreateMap<Notification, NotificationDto>();

            CreateMap<Event, EventDto>();
            CreateMap<CreateEventDto, Event>();
            CreateMap<UpdateEventDto, Event>();
        }

    }
}
