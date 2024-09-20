using AutoMapper;
using Events.Business.Utility;
using Events.DataAccess.Services.TokenService;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.AuthDtos;
using Events.Domain.Entities;

namespace Events.Business.UseCases.AuthUseCases.Register;

public class RegisterUseCase : IRegisterUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    
    public RegisterUseCase(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenService = tokenService;
    }
    
    public async Task Register(RegisterRequestDto dto)
    {
        var existUser = await _unitOfWork.UserRepository.GetAsync((user) => user.Email == dto.Email);

        if (existUser != null)
            throw new DuplicatedObjectException("Such user exists");

        User user = CreateUser(dto);

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveAsync();
    }
    
    private User CreateUser(RegisterRequestDto dto)
    {
        User user = _mapper.Map<User>(dto);
        user.RefreshToken = _tokenService.GenerateRefreshToken();
        return user;
    }
}