using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MoneyKeeper.Attributes;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneyKeeper.Hubs
{
    [Authorize]
    public class NotiHub : Hub
    {
        private IMapper _mapper { get; set; }
        public NotiHub(IMapper mapper)
        {
            _mapper = mapper;
        }
        [Protect]
        public async Task Ping(string test)
        {
            try
            {
                var userId = Context.UserIdentifier;
                System.Diagnostics.Debug.WriteLine("Check user send message: " + userId);

                await Clients.Users(userId!).SendAsync("Pong", "Server reply " + userId);
            }
            catch (Exception e)
            {
                throw new HubException(e.Message);
            }
        }

        //public async Task SendMessage(CreateMessageDto message)
        //{
        //    try
        //    {
        //        var userId = Context.UserIdentifier;
        //        System.Diagnostics.Debug.WriteLine("Check user send message: " + userId);
        //        message.SenderId = userId!;
        //        var newMessage = await _unitOfWork.Messages.CreateNewMessage(message);
        //        var membersId = await _unitOfWork.Rooms.GetMembersInRoom(newMessage.RoomId);

        //        var messageDto = _mapper.Map<MessageDto>(newMessage);
        //        await Clients.Users(membersId).SendAsync("ReceiveMessage", messageDto);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new HubException(e.Message);
        //    }
        //}

        //public async Task SendMessageStr(string messageJson)
        //{
        //    try
        //    {

        //        CreateMessageDto? message =
        //           JsonSerializer.Deserialize<CreateMessageDto>(messageJson);

        //        if (message == null)
        //        {
        //            throw new Exception("Wrong format!");
        //        }

        //        var userId = Context.UserIdentifier;
        //        System.Diagnostics.Debug.WriteLine("Check user send message: " + userId);
        //        message.SenderId = userId!;
        //        var newMessage = await _unitOfWork.Messages.CreateNewMessage(message);
        //        var membersId = await _unitOfWork.Rooms.GetMembersInRoom(newMessage.RoomId);

        //        var messageDto = _mapper.Map<MessageDto>(newMessage);
        //        await Clients.Users(membersId).SendAsync("ReceiveMessage", messageDto);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new HubException(e.Message);
        //    }
        //}

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
