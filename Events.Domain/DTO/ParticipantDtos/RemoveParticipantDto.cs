namespace Events.Domain.DTO.ParticipantDtos
{
    public class RemoveParticipantDto
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}
