using Events.Domain.Entities;

namespace Events.DataAccess.Services.TokenService
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, string secret, string audience, string issuer);

        string GenerateRefreshToken();
    }
}
