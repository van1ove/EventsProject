using Events.Business.UseCases.UserUseCases.GetAllUsers;
using Events.Business.UseCases.UserUseCases.GetEventParticipants;
using Events.Business.UseCases.UserUseCases.GetParticipant;
using Events.Business.UseCases.UserUseCases.SubscribeEvent;
using Events.Business.UseCases.UserUseCases.UnsubscribeEvent;
using Events.Domain.DTO.ParticipantDtos;
using Events.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGetParticipantUseCase _getParticipantUseCase;
        private readonly IGetAllUsersUseCase _getAllUsersUseCase;
        private readonly IGetEventParticipantsUseCase _getEventParticipantsUseCase;
        private readonly ISubscribeEventUseCase _subscribeEventUseCase;
        private readonly IUnsubscribeEventUseCase _unsubscribeEventUseCase;

        public UserController(IGetParticipantUseCase getParticipantUseCase,
            IGetAllUsersUseCase getAllUsersUseCase,
            IGetEventParticipantsUseCase getEventParticipantsUseCase,
            ISubscribeEventUseCase subscribeEventUseCase,
            IUnsubscribeEventUseCase unsubscribeEventUseCase)
        {
            _getParticipantUseCase = getParticipantUseCase;
            _getAllUsersUseCase = getAllUsersUseCase;
            _getEventParticipantsUseCase = getEventParticipantsUseCase;
            _subscribeEventUseCase = subscribeEventUseCase;
            _unsubscribeEventUseCase = unsubscribeEventUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<UserModel> response = await _getAllUsersUseCase.GetAllUsers();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            UserModel? response = await _getParticipantUseCase.GetParticipantById(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("add-to-event")]
        public async Task<IActionResult> AddUserToEvent([FromBody] AddParticipantDto dto)
        {
            var response = await _subscribeEventUseCase.SubscribeEvent(dto);
            return Ok(response);
        }

        [HttpPost]
        [Route("remove-from-event")]
        public async Task<IActionResult> RemoveUserFromEvent([FromBody] RemoveParticipantDto dto)
        {
            var response = await _unsubscribeEventUseCase.UnsubscribeEvent(dto);
            return Ok(response);
        }

        [HttpPost]
        [Route("event-participants")]
        public async Task<IActionResult> GetEventParticipants([FromBody] EventParticipantsDto dto)
        {
            var response = await _getEventParticipantsUseCase.GetEventParticipants(dto);
            return Ok(response);
        }

    }
}
