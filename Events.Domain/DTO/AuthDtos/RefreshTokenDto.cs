namespace Events.Domain.DTO.AuthDtos
{
    public class RefreshTokenDto
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
