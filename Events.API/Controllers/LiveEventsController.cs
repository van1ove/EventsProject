using Microsoft.AspNetCore.Mvc;
using Events.Business.Services.LiveEventService;
using Events.Domain.DTO.LiveEventDtos;
using Microsoft.AspNetCore.Authorization;

namespace Events_Web_Application.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LiveEventsController : ControllerBase
    {
        private readonly ILiveEventService _liveEventService;
            
        public LiveEventsController(ILiveEventService liveEventService)
        {
            _liveEventService = liveEventService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLiveEvent([FromForm] CreateLiveEventRequestDto dto)
        {
            var responce = await _liveEventService.CreateLiveEvent(dto);
            return Ok(responce);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLiveEvents()
        {
            var responce = await _liveEventService.GetAllLiveEvents();
            return Ok(responce);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetLiveEventById([FromRoute] Guid id)
        {
            var responce = await _liveEventService.GetLiveEventById(id);
            return CheckResponse(responce);
        }

        [HttpGet]
        [Route("{title}")]
        public async Task<IActionResult> GetLiveEventByTitle([FromRoute] string title)
        {
            var responce = await _liveEventService.GetLiveEventByTitle(title);
            return CheckResponse(responce);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateLiveEventById([FromRoute] Guid id, UpdateLiveEventRequestDto dto)
        {
            var responce = await _liveEventService.UpdateLiveEventById(id, dto);
            return CheckResponse(responce);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteLiveEventById([FromRoute] Guid id)
        {
            var responce = await _liveEventService.DeleteLiveEventById(id); 
            return CheckResponse(responce);
        }

        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetByFilter([FromBody] CriteriaDto dto)
        {
            return Ok(await _liveEventService.GetLiveEventByPredicate(dto));
        }

        private IActionResult CheckResponse(LiveEventDto? response) 
        {
            if (response is null)
                return NotFound();

            return Ok(response); 
        }
    }
}
