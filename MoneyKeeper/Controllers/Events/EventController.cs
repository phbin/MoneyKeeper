using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Attributes;
using MoneyKeeper.Data;
using MoneyKeeper.Dtos;
using MoneyKeeper.Models;
using MoneyKeeper.Services.Event;
using MoneyKeeper.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MoneyKeeper.Controllers.Events
{
    [ApiController]
    [Route("api/events")]
    [Protect]
    public class EventController : Controller
    {
        public IWalletService _walletService { get; set; }
        public IEventService _eventService { get; set; }
        public IMapper _mapper { get; set; }
        public DataContext _context { get; set; }
        public EventController(IMapper mapper, IWalletService walletService, DataContext context, IEventService eventService)
        {
            _mapper = mapper;
            _walletService = walletService;
            _context = context;
            _eventService = eventService;
        }
        [HttpGet]
        [Produces(typeof(ApiResponse<EventDto>))]
        public async Task<IActionResult> GetEvents([FromQuery] int? walletId)
        {
            var userId = HttpContext.Items["UserId"] as int?;

            IEnumerable<Event> events;
            if (walletId.HasValue)
            {
                events = await _eventService.GetEventsOfWallet((int)userId!, (int)walletId);
            }
            else
            {
                events = await _eventService.GetEvents((int)userId!);
            }
            var eventDtos = _mapper.Map<IEnumerable<EventDto>>(events);
            return Ok(new ApiResponse<object>(eventDtos, "Get my events successfully!"));
        }

        [HttpPost]
        [Produces(typeof(ApiResponse<EventDto>))]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto createDto)
        {
            var userId = HttpContext.Items["UserId"] as int?;

            var @event = await _eventService.CreateEvent((int)userId!, createDto);

            var @eventRes = _mapper.Map<EventDto>(@event);
            return new CreatedResult("", new ApiResponse<EventDto>(@eventRes, "Create successfully"));
        }

        [HttpPut("{id}")]
        [Produces(typeof(NoContentResult))]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto updateDto)
        {
            var userId = Convert.ToInt32(HttpContext.Items["UserId"] as int?);

            await _eventService.UpdateEvent(userId, id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Produces(typeof(NoContentResult))]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var userId = HttpContext.Items["UserId"] as int?;

            await _eventService.DeleteEvent((int)userId!, id);
            return NoContent();
        }

        [HttpGet("{id}/toggle-finished")]
        [Produces(typeof(NoContentResult))]
        public async Task<IActionResult> ToggleFinish(int id)
        {
            var userId = HttpContext.Items["UserId"] as int?;

            await _eventService.ToggleEventFinish((int)userId!, id);
            return NoContent();
        }
    }
}
