namespace Events.Domain.DTO.AuthDtos
{
    public class RegisterRequestDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateOnly Birthdate { get; set; }
    }
}
