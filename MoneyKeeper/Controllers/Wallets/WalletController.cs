using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Attributes;
using MoneyKeeper.Data;
using MoneyKeeper.Dtos;
using MoneyKeeper.DTOs;
using MoneyKeeper.Error;
using MoneyKeeper.Models;
using MoneyKeeper.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using static MoneyKeeper.Common.Enum;

namespace MoneyKeeper.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    [Protect]
    public class WalletController : Controller
    {
        public IWalletService _walletService { get; set; }
        public IMapper _mapper { get; set; }

        public WalletController(IWalletService walletSer, IMapper mapper)
        {
            _walletService = walletSer;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetWallets()
        {
            var userId = (int)(HttpContext.Items["UserId"] as int?)!;
            var wallets = await _walletService.GetWallets(userId);
            var walletsRes = _mapper.Map<IEnumerable<WalletDto>>(wallets);
            return Ok(new ApiResponse<IEnumerable<WalletDto>>(walletsRes, "Get my wallets successfully!"));


        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDto createWalletDto)
        {
            var userId = (int)(HttpContext.Items["UserId"] as int?)!;
            var wallet = await _walletService.CreateWallet(userId, createWalletDto);
            var walletDto = _mapper.Map<WalletDto>(wallet);

            return Ok(new ApiResponse<WalletDto>(walletDto, "Create wallet successfully!"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWallet(int id, [FromBody] UpdateWalletDto updateWallet)
        {
            var userId = (int)(HttpContext.Items["UserId"] as int?)!;

            await _walletService.UpdateWallet(id, userId, updateWallet);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallet(int id)
        {
            var userId = (int)(HttpContext.Items["UserId"] as int?)!;

            await _walletService.DeleteWallet(id, userId);

            return NoContent();
        }

        [HttpGet("{id}/members")]
        [Produces(typeof(ApiResponse<WalletMemberDto>))]
        public async Task<IActionResult> GetMembersOfWallet(int id)
        {
            var userId = (int)(HttpContext.Items["UserId"] as int?)!;

            var members = await _walletService.GetUsersInWallet(id, userId);
            return Ok(new ApiResponse<object>(members, "Get Members of wallet"));
        }

        //[HttpPost("{id}/members")]
        //[Produces(typeof(NoContentResult))]
        //public async Task<IActionResult> AddOrRemoveMember(int id, [FromBody] int memberId, [FromQuery, Required] WalletAction action)
        //{
        //    var userId = (int)(HttpContext.Items["UserId"] as int?)!;
        //    if (action == WalletAction.Invite)
        //    {
        //        await _walletService.AddMemberToWallet(userId, id, memberId);
        //    }
        //    else
        //    {
        //        await _walletService.RemoveMemberFromWallet(userId, id, memberId);
        //    }
        //    return NoContent();
        //}

    }

}
