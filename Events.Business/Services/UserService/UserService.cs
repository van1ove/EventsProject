using AutoMapper;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.ParticipantDtos;
using Events.Domain.Entities;
using Events.Domain.Models.User;

namespace Events.Business.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
   
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddParticipantToEvent(AddParticipantDto dto)
        {
            User? user = await _unitOfWork.UserRepository.GetAsync((user) => user.Id == dto.UserId);

            if (user == null)
                return false;

            LiveEvent? liveEvent = await _unitOfWork.LiveEventRepository.GetAsync((liveEvent) => liveEvent.Id == dto.EventId);

            if (liveEvent == null || user.Events.Select(x => x.Id).Contains(liveEvent.Id))
                return false;

            user.Events.Add(liveEvent);

            await _unitOfWork.UserRepository.UpdateAsync(user, (x) => x.Id == user.Id);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return (await _unitOfWork.UserRepository.GetAllAsync()).Select(x => _mapper.Map<UserModel>(x));
        }

        public async Task<IEnumerable<UserModel>> GetEventParticipants(EventParticipantsDto dto)
        {
            LiveEvent? liveEvent = await _unitOfWork.LiveEventRepository.GetAsync((x) => x.Id == dto.EventId);

            if (liveEvent == null)
            {
                return [];
            }

            return (await _unitOfWork.UserRepository
                .GetByPredicateAsync((x) => x.Events.Select(x => x.Id).Contains(liveEvent.Id)))
                .Select(x => _mapper.Map<UserModel>(x));
        }

        public async Task<UserModel?> GetParticipantById(Guid id)
        {
            User? entity = await _unitOfWork.UserRepository.GetAsync(x => x.Id == id);

            if (entity == null)
                return null;

            return _mapper.Map<UserModel>(entity);
        }

        public async Task<bool> RemoveParticipantFromEvent(RemoveParticipantDto dto)
        {
            User? user = await _unitOfWork.UserRepository.GetAsync((user) => user.Id == dto.UserId);

            if (user == null)
                return false;

            LiveEvent? liveEvent = await _unitOfWork.LiveEventRepository.GetAsync((liveEvent) => liveEvent.Id == dto.EventId);

            if (liveEvent == null)
                return false;

            user.Events.Remove(liveEvent);

            await _unitOfWork.UserRepository.UpdateAsync(user, (x) => x.Id == user.Id);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
