using Events.Domain.DTO.ParticipantDtos;
using Events.Domain.Models.User;

namespace Events.Business.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel?> GetParticipantById(Guid id);
        Task<IEnumerable<UserModel>> GetEventParticipants(EventParticipantsDto dto);
        Task<bool> AddParticipantToEvent(AddParticipantDto dto);
        Task<bool> RemoveParticipantFromEvent(RemoveParticipantDto dto);
    }
}
