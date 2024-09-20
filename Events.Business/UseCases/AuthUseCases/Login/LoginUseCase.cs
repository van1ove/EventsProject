using AutoMapper;
using Events.Business.Utility;
using Events.DataAccess.Services.TokenService;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.AuthDtos;
using Events.Domain.Models.User;
using Microsoft.Extensions.Configuration;

namespace Events.Business.UseCases.AuthUseCases.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public LoginUseCase(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _config = config;
        _mapper = mapper;
        _tokenService = tokenService;
    }
    
    public async Task<AuthResponse?> Login(LoginDto dto)
    {
        var user = await _unitOfWork.UserRepository.GetAsync((user) => user.Email == dto.Email);

        if (user == null)
            throw new NullReferenceException("User not found");

        if (user.Password != dto.Password.GetHash())
            throw new InvalidPasswordException("Wrong password");

        var accessToken = _tokenService.GenerateAccessToken(
            user, 
            _config["Jwt:Key"], 
            _config["Jwt:Audience"], 
            _config["Jwt:Issuer"]);

        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;

        await _unitOfWork.UserRepository.UpdateAsync(user, user => user.Email == user.Email);
        await _unitOfWork.SaveAsync();

        var tokenResponse = new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = _mapper.Map<UserModel>(user)
        };

        return tokenResponse;
    }
}