using Events.Domain.Models.User;

namespace Events.Domain.DTO.AuthDtos
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserModel User { get; set; }
    }
}
