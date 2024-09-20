using Events.Domain.Models.User;

namespace Events.Business.UseCases.UserUseCases.GetParticipant;

public interface IGetParticipantUseCase
{
    public Task<UserModel?> GetParticipantById(Guid id);
}