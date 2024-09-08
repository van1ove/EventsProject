namespace Events.Domain.DTO.Participant
{
    public class CreateParticipantRequestDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateOnly BirthDate { get; set; }

        public DateOnly? RegistrationDate { get; set; }

        public Guid? LiveEventId { get; set; } 

        public string Email { get; set; }
    }
}
