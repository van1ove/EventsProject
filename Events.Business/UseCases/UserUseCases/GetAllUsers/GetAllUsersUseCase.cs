using AutoMapper;
using Events.DataAccess.UnitOfWork;
using Events.Domain.Models.User;

namespace Events.Business.UseCases.UserUseCases.GetAllUsers;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersUseCase(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<UserModel>> GetAllUsers()
    {
        var response = (await _unitOfWork.UserRepository.GetAllAsync()).Select(x => _mapper.Map<UserModel>(x));
        return response;
    }
}