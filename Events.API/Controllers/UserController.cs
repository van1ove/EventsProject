using Events.Business.Services.UserService;
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
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<UserModel> request = await _userService.GetAllUsers();
            return Ok(request);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            UserModel? response = await _userService.GetParticipantById(id);

            if(response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        [Route("add-to-event")]
        public async Task<IActionResult> AddUserToEvent([FromBody] AddParticipantDto dto)
        {
            return Ok(await _userService.AddParticipantToEvent(dto));
        }

        [HttpPost]
        [Route("remove-from-event")]
        public async Task<IActionResult> RemoveUserFromEvent([FromBody] RemoveParticipantDto dto)
        {
            return Ok(await _userService.RemoveParticipantFromEvent(dto));
        }

        [HttpPost]
        [Route("event-participants")]
        public async Task<IActionResult> GetEventParticipants([FromBody] EventParticipantsDto dto)
        {
            return Ok(await _userService.GetEventParticipants(dto));
        }

    }
}
