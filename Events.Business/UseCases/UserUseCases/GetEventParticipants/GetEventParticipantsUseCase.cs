using AutoMapper;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.ParticipantDtos;
using Events.Domain.Entities;
using Events.Domain.Models.User;

namespace Events.Business.UseCases.UserUseCases.GetEventParticipants;

public class GetEventParticipantsUseCase : IGetEventParticipantsUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetEventParticipantsUseCase(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<UserModel>> GetEventParticipants(EventParticipantsDto dto)
    {
        LiveEvent? liveEvent = await _unitOfWork.LiveEventRepository.GetAsync((x) => x.Id == dto.EventId);

        if (liveEvent == null)
            return [];

        return (await _unitOfWork.UserRepository
                .GetByPredicateAsync((x) => x.Events.Select(x => x.Id).Contains(liveEvent.Id)))
            .Select(x => _mapper.Map<UserModel>(x));
    }
}