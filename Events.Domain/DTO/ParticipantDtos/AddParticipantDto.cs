namespace Events.Domain.DTO.ParticipantDtos
{
    public class AddParticipantDto
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}
