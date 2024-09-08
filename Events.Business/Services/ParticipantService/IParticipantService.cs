using Events.Domain.DTO.Participant;
using Events.Domain.DTO.ParticipantDtos;

namespace Events.Business.Services.ParticipantService
{
    public interface IParticipantService
    {

        Task<IEnumerable<ParticipantDto>> GetallParticipants();

        Task<ParticipantDto?> GetParticipantById(Guid id);
    }
}
