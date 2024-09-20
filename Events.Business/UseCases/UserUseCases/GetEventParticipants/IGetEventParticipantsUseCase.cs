using Events.Domain.DTO.ParticipantDtos;
using Events.Domain.Models.User;

namespace Events.Business.UseCases.UserUseCases.GetEventParticipants;

public interface IGetEventParticipantsUseCase
{
    public Task<IEnumerable<UserModel>> GetEventParticipants(EventParticipantsDto dto);
}