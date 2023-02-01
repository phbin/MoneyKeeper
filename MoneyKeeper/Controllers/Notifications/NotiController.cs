using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Attributes;
using MoneyKeeper.Data;
using MoneyKeeper.Dtos;
using MoneyKeeper.Models;
using MoneyKeeper.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyKeeper.Controllers
{
    [ApiController]
    [Protect]
    [Route("api/notifications")]
    public class NotiController : ControllerBase
    {
        private readonly INotiService _notiService;
        public IMapper _mapper { get; set; }

        public NotiController(IMapper mapper, INotiService notiService)
        {
            _notiService = notiService;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces(typeof(ApiResponse<IEnumerable<NotificationDto>>))]
        public async Task<IActionResult> GetNotis()
        {
            var userId = HttpContext.Items["UserId"] as int?;
            var notis = await _notiService.GetNotifications((int)userId!);
            var notiDtos = _mapper.Map<IEnumerable<NotificationDto>>(notis);
            return Ok(new ApiResponse<object>(notiDtos, "Get notifications"));
        }
    }
}