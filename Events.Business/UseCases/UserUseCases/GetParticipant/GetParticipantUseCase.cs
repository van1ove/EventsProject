using AutoMapper;
using Events.DataAccess.UnitOfWork;
using Events.Domain.Entities;
using Events.Domain.Models.User;

namespace Events.Business.UseCases.UserUseCases.GetParticipant;

public class GetParticipantUseCase : IGetParticipantUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<UserModel?> GetParticipantById(Guid id)
    {
        User? entity = await _unitOfWork.UserRepository.GetAsync(x => x.Id == id);

        if (entity == null)
            throw new NullReferenceException("Event participant not found");

        return _mapper.Map<UserModel>(entity);
    }
}