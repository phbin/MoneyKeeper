using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Data;
using MoneyKeeper.Models;
using static MoneyKeeper.Common.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using MoneyKeeper.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Dtos.User;

namespace MoneyKeeper.Controllers.Users
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        public DataContext _context { get; set; }
        public IMapper _mapper { get; set; }
        public AccountController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPut("info")]
        [Produces(typeof(IEnumerable<InvitationDto>))]
        public async Task<IActionResult> UpdateInfo([FromBody] UpdateUserDto updateUserDto)
        {
            var userId = HttpContext.Items["UserId"] as int?;
            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (user == null) return NotFound();
            user.Avatar = updateUserDto.Avatar;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("invitations")]
        [Produces(typeof(IEnumerable<InvitationDto>))]
        public async Task<IActionResult> GetInvitations()
        {
            var userId = HttpContext.Items["UserId"] as int?;
            var invitations = await _context.Invitations.Where(u => u.UserId == userId).ToListAsync();

            var invitationDtos = _mapper.Map<IEnumerable<InvitationDto>>(invitations);
            return Ok(new ApiResponse<IEnumerable<InvitationDto>>(invitationDtos, "Get invitations"));
        }

        [HttpGet("invitations/{id}/action")]
        public async Task<IActionResult> DoActionOnInvitation(int id, [Required][FromQuery] InvitationAction action)
        {
            var userId = HttpContext.Items["UserId"] as int?;
            var invitation = await _context.Invitations.Where(u => u.Id == id && u.UserId == userId).FirstOrDefaultAsync();

            if (invitation == null)
            {
                throw new ApiException("Invalid invitation", 400);
            }

            if (invitation.Status != InvitationStatus.New)
            {
                throw new ApiException("Invitation in this status can not be changed", 400);
            }

            if (invitation.ExpirationDate <= DateTime.Now)
            {
                throw new ApiException("This invitation is expired", 400);
            }

            var memberWallet = await _context.WalletMembers.Where(wM => wM.WalletId == invitation.WalletId && wM.UserId == userId).FirstOrDefaultAsync();
            if (memberWallet == null)
            {
                throw new ApiException("User is not invited in Wallet", 400);
            }

            if (action == InvitationAction.Accept)
            {
                memberWallet.Status = MemberStatus.Accepted;
                invitation.Status = InvitationStatus.Accepted;
            }
            else
            {
                memberWallet.Status = MemberStatus.Declined;
                invitation.Status = InvitationStatus.Declined;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
