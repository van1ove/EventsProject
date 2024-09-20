using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.ParticipantDtos;
using Events.Domain.Entities;

namespace Events.Business.UseCases.UserUseCases.UnsubscribeEvent
{
    public class UnsubscribeEventUseCase : IUnsubscribeEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnsubscribeEventUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UnsubscribeEvent(RemoveParticipantDto dto)
        {
            Domain.Entities.User? user = await _unitOfWork.UserRepository.GetAsync((user) => user.Id == dto.UserId);

            if (user == null)
                throw new NullReferenceException("User not found");

            LiveEvent? liveEvent = await _unitOfWork.LiveEventRepository.GetAsync((liveEvent) => liveEvent.Id == dto.EventId);

            if (liveEvent == null)
                throw new NullReferenceException("Live event not found");

            user.Events.Remove(liveEvent);

            await _unitOfWork.UserRepository.UpdateAsync(user, (x) => x.Id == user.Id);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
