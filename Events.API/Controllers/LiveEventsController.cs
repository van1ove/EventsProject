using Microsoft.AspNetCore.Mvc;
using Events.Business.UseCases.LiveEventUseCases.CreateLiveEvent;
using Events.Business.UseCases.LiveEventUseCases.DeleteLiveEvent;
using Events.Business.UseCases.LiveEventUseCases.GetAllLiveEvents;
using Events.Business.UseCases.LiveEventUseCases.GetLiveEvent;
using Events.Business.UseCases.LiveEventUseCases.UpdateLiveEvent;
using Events.Domain.DTO.LiveEventDtos;
using Microsoft.AspNetCore.Authorization;

namespace Events_Web_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LiveEventsController : ControllerBase
    {
        private readonly ICreateLiveEventUseCase _createLiveEventUseCase;
        private readonly IGetAllLiveEventsUseCase _getAllLiveEventsUseCase;
        private readonly IGetLiveEventUseCase _getLiveEventUseCase;
        private readonly IUpdateLiveEventUseCase _updateLiveEventUseCase;
        private readonly IDeleteLiveEventUseCase _deleteLiveEventUseCase;
            
        public LiveEventsController(ICreateLiveEventUseCase createLiveEventUseCase,
            IGetAllLiveEventsUseCase getAllLiveEventsUseCase,
            IGetLiveEventUseCase getLiveEventUseCase,
            IUpdateLiveEventUseCase updateLiveEventUseCase,
            IDeleteLiveEventUseCase deleteLiveEventUseCase)
        {
            _createLiveEventUseCase = createLiveEventUseCase;
            _getAllLiveEventsUseCase = getAllLiveEventsUseCase;
            _getLiveEventUseCase = getLiveEventUseCase;
            _updateLiveEventUseCase = updateLiveEventUseCase;
            _deleteLiveEventUseCase = deleteLiveEventUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLiveEvent([FromForm] CreateLiveEventRequestDto dto)
        {
            var responce = await _createLiveEventUseCase.CreateLiveEvent(dto);
            return Ok(responce);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLiveEvents()
        {
            var responce = await _getAllLiveEventsUseCase.GetAllLiveEvents();
            return Ok(responce);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetLiveEventById([FromRoute] Guid id)
        {
            var responce = await _getLiveEventUseCase.GetLiveEventById(id);
            return Ok(responce);
        }

        [HttpGet]
        [Route("{title}")]
        public async Task<IActionResult> GetLiveEventByTitle([FromRoute] string title)
        {
            var responce = await _getLiveEventUseCase.GetLiveEventByTitle(title);
            return Ok(responce);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateLiveEventById([FromRoute] Guid id, UpdateLiveEventRequestDto dto)
        {
            var responce = await _updateLiveEventUseCase.UpdateLiveEventById(id, dto);
            return Ok(responce);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteLiveEventById([FromRoute] Guid id)
        {
            var response = await _deleteLiveEventUseCase.DeleteLiveEventById(id); 
            return Ok(response);
        }

        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetByFilter([FromBody] CriteriaDto dto)
        {
            var response = await _getLiveEventUseCase.GetLiveEventByPredicate(dto);
            return Ok(response);
        }
    }
}
