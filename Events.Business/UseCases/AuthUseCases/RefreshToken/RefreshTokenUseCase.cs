using AutoMapper;
using Events.Business.Utility;
using Events.DataAccess.Services.TokenService;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.AuthDtos;
using Microsoft.Extensions.Configuration;

namespace Events.Business.UseCases.AuthUseCases.RefreshToken;

public class RefreshTokenUseCase : IRefreshTokenUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;
    private readonly ITokenService _tokenService;

    public RefreshTokenUseCase(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper,
        ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _config = config;
        _tokenService = tokenService;
    }
    
    public async Task<AuthResponse?> RefreshToken(RefreshTokenDto dto)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(user => user.Id == dto.UserId);

        if (user == null)
            throw new NullReferenceException("User not found");

        var storedRefreshToken = user.RefreshToken;

        if (storedRefreshToken != dto.RefreshToken)
            throw new InvalidTokenException("Invalid token");

        var newAccessToken = _tokenService.GenerateAccessToken(
            user, 
            _config["Jwt:Key"], 
            _config["Jwt:Audience"], 
            _config["Jwt:Issuer"]);

        var tokenResponse = new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = dto.RefreshToken
        };

        return tokenResponse;
    }
}