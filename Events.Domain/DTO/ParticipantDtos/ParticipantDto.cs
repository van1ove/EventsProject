using System.Diagnostics.CodeAnalysis;

namespace Events.Domain.DTO.ParticipantDtos
{
    public class ParticipantDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateOnly BirthDate { get; set; }

        [AllowNull]
        public DateOnly RegistrationDate { get; set; }

        [AllowNull]
        public Guid LiveEventId { get; set; }

        public string Email { get; set; }
    }
}
