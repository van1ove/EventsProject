using AutoMapper;
using Events.Business.EnumUtility;
using Events.Business.Utility;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.AuthDtos;
using Events.Domain.Entities;
using Events.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
namespace Events.Business.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _mapper = mapper;
        }

        public async Task<(OperationResult, AuthResponse?)> Login(LoginDto dto)
        {
            var user = await _unitOfWork.UserRepository.GetAsync((user) => user.Email == dto.Email);

            if (user == null)
                return (OperationResult.Fail, null);

            if (user.Password != dto.Password.GetHash())
                return (OperationResult.Fail, null);

            var accessToken = TokenUtils.GenerateAccessToken(
                user, 
                _config["Jwt:Key"], 
                _config["Jwt:Audience"], 
                _config["Jwt:Issuer"]);

            var refreshToken = TokenUtils.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            await _unitOfWork.UserRepository.UpdateAsync(user, (user) => user.Email == user.Email);
            await _unitOfWork.SaveAsync();

            var tokenResponse = new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User = _mapper.Map<UserModel>(user)
            };

            return (OperationResult.Success, tokenResponse);
        }

        public async Task<(OperationResult, AuthResponse?)> RefreshToken(RefreshTokenDto dto)
        {
            var user = await _unitOfWork.UserRepository.GetAsync((user) => user.Id == dto.UserId);

            if (user == null) 
                return (OperationResult.Fail, null);

            var storedRefreshToken = user.RefreshToken;

            if (storedRefreshToken != dto.RefreshToken)
                return (OperationResult.Fail, null);

            var newAccessToken = TokenUtils.GenerateAccessToken(
                user, 
                _config["Jwt:Key"], 
                _config["Jwt:Audience"], 
                _config["Jwt:Issuer"]);

            var tokenResponse = new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = dto.RefreshToken
            };

            return (OperationResult.Success, tokenResponse);
        }

        public async Task<(OperationResult, List<string>)> Register(RegisterRequestDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Password = dto.Password.GetHash(),
                Birthdate = dto.Birthdate,
                Events = [],
                Role = "User",
                RegistrationDate = DateTime.UtcNow.Date.ToDateOnly(),
                RefreshToken = TokenUtils.GenerateRefreshToken()
            };

            var existUser = await _unitOfWork.UserRepository.GetAsync((user) => user.Email == dto.Email);

            if (existUser != null)
            {
                return (OperationResult.Fail, new List<string> { $"User already exists with email = {dto.Email}" });
            }

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return (OperationResult.Success, new List<string> { });
        }

        private (OperationResult, List<string>) CheckErrors(IEnumerable<IdentityError> identityErrors)
        {
            List<string> errors = new List<string>();

            foreach (var error in identityErrors)
            {
                errors.Add(error.Description);
            }
            return (OperationResult.Fail, errors);
        }
    }
}
