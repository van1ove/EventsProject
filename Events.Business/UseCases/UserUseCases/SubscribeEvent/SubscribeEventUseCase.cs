using Events.Business.Utility;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.ParticipantDtos;
using Events.Domain.Entities;

namespace Events.Business.UseCases.UserUseCases.SubscribeEvent
{
    public class SubscribeEventUseCase : ISubscribeEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscribeEventUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SubscribeEvent(AddParticipantDto dto)
        {
            Domain.Entities.User? user = await _unitOfWork.UserRepository.GetAsync((user) => user.Id == dto.UserId);

            if (user == null)
                throw new NullReferenceException("User not found");

            LiveEvent? liveEvent = await _unitOfWork.LiveEventRepository.GetAsync((liveEvent) => liveEvent.Id == dto.EventId);

            if (liveEvent == null)
                throw new NullReferenceException("Live event not found");

            if (user.Events.Select(x => x.Id).Contains(liveEvent.Id))
                throw new DuplicatedObjectException("User is already event participant");

            user.Events.Add(liveEvent);

            await _unitOfWork.UserRepository.UpdateAsync(user, x => x.Id == user.Id);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
