using AutoMapper;
using Events.Domain.Entities;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.LiveEventDtos;
using Events.Business.Utility;
using System.Linq.Expressions;

namespace Events.Business.Services.LiveEventService
{
    public class LiveEventsService : ILiveEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LiveEventsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LiveEventDto> CreateLiveEvent(CreateLiveEventRequestDto dto)
        {
            LiveEvent liveEvent = _mapper.Map<LiveEvent>(dto);
            await _unitOfWork.LiveEventRepository.AddAsync(liveEvent);
            await _unitOfWork.SaveAsync();
            LiveEventDto response = _mapper.Map<LiveEventDto>(liveEvent);
            return response;
        }

        public async Task<IEnumerable<LiveEventDto>> GetAllLiveEvents()
        {
            IEnumerable<LiveEvent> liveEvents = await _unitOfWork.LiveEventRepository.GetAllAsync();

            List<LiveEventDto> response = new List<LiveEventDto>();
            foreach (LiveEvent liveEvent in liveEvents)
            {
                response.Add(_mapper.Map<LiveEventDto>(liveEvent));
            }

            return response;
        }

        public async Task<LiveEventDto?> GetLiveEventById(Guid id)
        {
            LiveEvent? liveEvent = await _unitOfWork.LiveEventRepository.GetAsync(liveEvent => liveEvent.Id == id);
            return CheckLiveEventValue(liveEvent);
        }

        public async Task<LiveEventDto?> GetLiveEventByTitle(string title)
        {
            LiveEvent? liveEvent = await _unitOfWork.LiveEventRepository.GetAsync(liveEvent => liveEvent.Title.ToLower() == title.ToLower());
            return CheckLiveEventValue(liveEvent);
        }

        public async Task<LiveEventDto?> UpdateLiveEventById(Guid id, UpdateLiveEventRequestDto dto)
        {
            LiveEvent? existingLiveEvent = await _unitOfWork.LiveEventRepository.GetAsync(x => x.Id == id);

            if (existingLiveEvent == null)
            {
                return null;
            }

            CopyFiledsfromDto(ref existingLiveEvent, dto);
            await _unitOfWork.LiveEventRepository.UpdateAsync(existingLiveEvent, x => x.Id == existingLiveEvent.Id);
            await _unitOfWork.SaveAsync();
            return CheckLiveEventValue(existingLiveEvent);
        }

        public async Task<LiveEventDto?> DeleteLiveEventById(Guid id)
        {
            LiveEvent? liveEvent = await _unitOfWork.LiveEventRepository.DeleteAsync(liveEvent => liveEvent.Id == id);
            await _unitOfWork.SaveAsync();
            return CheckLiveEventValue(liveEvent);
        }


        private LiveEventDto? CheckLiveEventValue(LiveEvent? liveEvent)
        {
            if (liveEvent is null)
                return null;

            LiveEventDto responce = _mapper.Map<LiveEventDto>(liveEvent);
            return responce;
        }

        private static void CopyFiledsfromDto(ref LiveEvent? existingEvent, UpdateLiveEventRequestDto dto)
        {
            existingEvent.Date = dto.Date;
            existingEvent.Category = dto.Category;
            existingEvent.Description = dto.Description;
            existingEvent.Title = dto.Title;
            existingEvent.ImageUrl = dto.ImageUrl;
            existingEvent.Place = dto.Place;
            existingEvent.MaxParticipants = dto.MaxParticipants;
            existingEvent.ImageUrl = dto.ImageUrl;
            existingEvent.Time = dto.Time;
        }

        public async Task<IEnumerable<LiveEventDto>> GetLiveEventByPredicate(CriteriaDto dto)
        {
            Expression<Func<LiveEvent, bool>> predicate = dto.ToPredicate();

            IEnumerable<LiveEvent> entities =  await _unitOfWork.LiveEventRepository.GetByPredicateAsync(predicate);

            List<LiveEventDto> dtos = [];

            foreach (var entity in entities)
            {
                dtos.Add(_mapper.Map<LiveEventDto>(entity));
            }

            return dtos;
        }
    }
}
